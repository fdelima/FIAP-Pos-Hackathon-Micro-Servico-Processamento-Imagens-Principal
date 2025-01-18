using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Application.UseCases.ProcessamentoImagem.Commands;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Interfaces;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Models;
using FluentValidation;
using MediatR;
using System.Linq.Expressions;

namespace FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Application.Controllers
{
    /// <summary>
    /// Regras da aplicação referente ao ProcessamentoImagem
    /// </summary>
    public class ProcessamentoImagemController : IController<Domain.Entities.ProcessamentoImagem>
    {
        private readonly IMediator _mediator;
        private readonly IValidator<Domain.Entities.ProcessamentoImagem> _validator;

        public ProcessamentoImagemController(IMediator mediator, IValidator<Domain.Entities.ProcessamentoImagem> validator)
        {
            _mediator = mediator;
            _validator = validator;
        }

        /// <summary>
        /// Valida a entidade
        /// </summary>
        /// <param name="entity">Entidade</param>
        public async Task<ModelResult> ValidateAsync(Domain.Entities.ProcessamentoImagem entity)
        {
            ModelResult ValidatorResult = new ModelResult(entity);

            FluentValidation.Results.ValidationResult validations = _validator.Validate(entity);
            if (!validations.IsValid)
            {
                ValidatorResult.AddValidations(validations);
                return ValidatorResult;
            }

            return await Task.FromResult(ValidatorResult);
        }

        /// <summary>
        /// Envia a entidade para inserção ao domínio
        /// </summary>
        /// <param name="entity">Entidade</param>
        public virtual async Task<ModelResult> PostAsync(Domain.Entities.ProcessamentoImagem entity)
        {
            if (entity == null) throw new InvalidOperationException($"Necessário informar os dados de processamento de imagem");

            ModelResult ValidatorResult = await ValidateAsync(entity);

            if (ValidatorResult.IsValid)
            {
                ProcessamentoImagemPostCommand command = new(entity);
                return await _mediator.Send(command);
            }

            return ValidatorResult;
        }

        /// <summary>
        /// Envia a entidade para atualização ao domínio
        /// </summary>
        /// <param name="entity">Entidade</param>
        /// <param name="duplicatedExpression">Expressão para verificação de duplicidade.</param>
        public virtual async Task<ModelResult> PutAsync(Guid id, Domain.Entities.ProcessamentoImagem entity)
        {
            if (entity == null) throw new InvalidOperationException($"Necessário informar os dados de processamento de imagem");

            ModelResult ValidatorResult = await ValidateAsync(entity);

            if (ValidatorResult.IsValid)
            {
                ProcessamentoImagemPutCommand command = new(id, entity);
                return await _mediator.Send(command);
            }

            return ValidatorResult;
        }

        /// <summary>
        /// Envia a entidade para deleção ao domínio
        /// </summary>
        /// <param name="entity">Entidade</param>
        public virtual async Task<ModelResult> DeleteAsync(Guid id)
        {
            ProcessamentoImagemDeleteCommand command = new(id);
            return await _mediator.Send(command);
        }

        /// <summary>
        /// Retorna a entidade
        /// </summary>
        /// <param name="entity">Entidade</param>
        public virtual async Task<ModelResult> FindByIdAsync(Guid id)
        {
            ProcessamentoImagemFindByIdCommand command = new(id);
            return await _mediator.Send(command);
        }


        /// <summary>
        /// Retorna as entidades
        /// </summary>
        /// <param name="filter">filtro a ser aplicado</param>
        public virtual async ValueTask<PagingQueryResult<Domain.Entities.ProcessamentoImagem>> GetItemsAsync(IPagingQueryParam filter, Expression<Func<Domain.Entities.ProcessamentoImagem, object>> sortProp)
        {
            if (filter == null) throw new InvalidOperationException("Necessário informar o filtro da consulta");

            ProcessamentoImagemGetItemsCommand command = new(filter, sortProp);
            return await _mediator.Send(command);
        }


        /// <summary>
        /// Retorna as entidades que atendem a expressão de filtro 
        /// </summary>
        /// <param name="expression">Condição que filtra os itens a serem retornados</param>
        /// <param name="filter">filtro a ser aplicado</param>
        public virtual async ValueTask<PagingQueryResult<Domain.Entities.ProcessamentoImagem>> ConsultItemsAsync(IPagingQueryParam filter, Expression<Func<Domain.Entities.ProcessamentoImagem, bool>> expression, Expression<Func<Domain.Entities.ProcessamentoImagem, object>> sortProp)
        {
            if (filter == null) throw new InvalidOperationException("Necessário informar o filtro da consulta");

            ProcessamentoImagemGetItemsCommand command = new(filter, expression, sortProp);
            return await _mediator.Send(command);
        }

    }
}
