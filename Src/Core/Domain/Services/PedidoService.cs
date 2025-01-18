using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Entities;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Interfaces;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Messages;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Models;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.ValuesObject;
using FluentValidation;

namespace FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Services
{
    public class PedidoService : BaseService<Pedido>, IPedidoService
    {
        protected readonly IGateways<Notificacao> _notificacaoGateway;
        protected readonly IGateways<MercadoPagoWebhoock> _mercadoPagoWebhoockGateway;

        /// <summary>
        /// Lógica de negócio referentes ao pedido.
        /// </summary>
        /// <param name="gateway">Gateway de pedido a ser injetado durante a execução</param>
        /// <param name="validator">abstração do validador a ser injetado durante a execução</param>
        /// <param name="notificacaoGateway">Gateway de notificação a ser injetado durante a execução</param>
        /// <param name="MercadoPagoWebhoockGateway">Gateway de MercadoPagoWebhoock a ser injetado durante a execução</param>
        public PedidoService(IGateways<Pedido> gateway,
            IValidator<Pedido> validator,
            IGateways<Notificacao> notificacaoGateway,
            IGateways<MercadoPagoWebhoock> mercadoPagoWebhoockGateway)
            : base(gateway, validator)
        {
            _notificacaoGateway = notificacaoGateway;
            _mercadoPagoWebhoockGateway = mercadoPagoWebhoockGateway;
        }


        /// <summary>
        /// Regra para para consultar o pagamento de um pedido.
        /// </summary>
        public async Task<ModelResult> ConsultarPagamentoAsync(Guid id)
        {
            var result = await _gateway.FindByIdAsync(id);

            if (result == null)
                return ModelResultFactory.NotFoundResult<Pedido>();

            return ModelResultFactory.SucessResult(result.StatusPagamento);
        }

        /// <summary>
        ///  Regra de Webhook para notificação de pagamento.
        /// </summary>
        public async Task<ModelResult> ReceberPedido(Pedido notificacao, string[]? businessRules = null)
        {
            ModelResult ValidatorResult = new ModelResult();

            if (businessRules != null)
                ValidatorResult.AddError(businessRules);

            if (!ValidatorResult.IsValid)
                return ValidatorResult;

            var entity = await _gateway.FindByIdAsync(notificacao.IdPedido);

            if (entity == null)
            {
                await _gateway.InsertAsync(notificacao);
                await _gateway.CommitAsync();
                return ModelResultFactory.InsertSucessResult<Pedido>(notificacao);
            }
            else
            {
                return ModelResultFactory.DuplicatedResult<Pedido>();
            }
        }

        public async Task<ModelResult> MercadoPagoWebhoock(MercadoPagoWebhoock notificacao, Guid idPedido, string[]? businessRules = null)
        {
            ModelResult ValidatorResult = new ModelResult();

            if (businessRules != null)
                ValidatorResult.AddError(businessRules);

            if (!ValidatorResult.IsValid)
                return ValidatorResult;

            var entity = await _gateway.FindByIdAsync(idPedido);

            if (entity != null)
            {
                entity.StatusPagamento = enmPedidoStatusPagamento.APROVADO.ToString();
                entity.DataStatusPagamento = DateTime.Now;
                await _gateway.UpdateAsync(entity);
                await _gateway.CommitAsync();
            }
            else
            {
                var notFound = new Notificacao
                {
                    Data = DateTime.Now,
                    Mensagem = $"{idPedido}: {BusinessMessages.NotFoundError<Pedido>()}"
                };
                await _notificacaoGateway.InsertAsync(notFound);
                await _gateway.CommitAsync();
            }

            await _mercadoPagoWebhoockGateway.InsertAsync(notificacao);
            await _mercadoPagoWebhoockGateway.CommitAsync();
            return ModelResultFactory.InsertSucessResult<MercadoPagoWebhoock>(notificacao);
        }
    }
}