using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Entities;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Interfaces;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Models;
using FluentValidation;
using System.Linq.Expressions;

namespace FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Services
{
    public class ProcessamentoImagemService : BaseService<ProcessamentoImagem>
    {
        protected readonly IGateways<Notificacao> _notificacaoGateway;

        /// <summary>
        /// Lógica de negócio referentes ao pedido.
        /// </summary>
        /// <param name="gateway">Gateway de pedido a ser injetado durante a execução</param>
        /// <param name="validator">abstração do validador a ser injetado durante a execução</param>
        /// <param name="notificacaoGateway">Gateway de notificação a ser injetado durante a execução</param>
        /// <param name="MercadoPagoWebhoockGateway">Gateway de MercadoPagoWebhoock a ser injetado durante a execução</param>
        public ProcessamentoImagemService(IGateways<ProcessamentoImagem> gateway,
            IValidator<ProcessamentoImagem> validator,
            IGateways<Notificacao> notificacaoGateway)
            : base(gateway, validator)
        {
            _notificacaoGateway = notificacaoGateway;
        }

        /// <summary>
        /// Regras para inserção do processamento de imagem
        /// </summary>
        /// <param name="entity">Entidade</param>
        /// <param name="ValidatorResult">Validações já realizadas a serem adicionadas ao contexto</param>
        public override async Task<ModelResult> InsertAsync(ProcessamentoImagem entity, string[]? businessRules = null)
        {
            entity.IdProcessamentoImagem = entity.IdProcessamentoImagem.Equals(default) ? Guid.NewGuid() : entity.IdProcessamentoImagem;
            entity.Data = DateTime.Now;

            ModelResult ValidatorResult = await ValidateAsync(entity);

            Expression<Func<IDomainEntity, bool>> duplicatedExpression = entity.InsertDuplicatedRule();

            if (ValidatorResult.IsValid)
            {
                if (duplicatedExpression != null)
                {
                    bool duplicado = await _gateway.Any(duplicatedExpression);

                    if (duplicado)
                        ValidatorResult.Add(ModelResultFactory.DuplicatedResult<ProcessamentoImagem>());
                }

                if (businessRules != null)
                    ValidatorResult.AddError(businessRules);

                if (!ValidatorResult.IsValid)
                    return ValidatorResult;

                await _gateway.InsertAsync(entity);
                await _gateway.InsertAsync(new Notificacao
                {
                    Mensagem = "Arquivo recebido com sucesso!",
                    Usuario = entity.Usuario
                });
                await _gateway.CommitAsync();
                return ModelResultFactory.InsertSucessResult<ProcessamentoImagem>(entity);
            }

            return ValidatorResult;
        }
    }
}