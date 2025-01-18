using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Application.UseCases.MercadoPago.Commands;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Application.UseCases.Pedido.Commands;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Entities;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Interfaces;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Models;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Application.Controllers
{
    /// <summary>
    /// Regras da aplicação referente ao pedido
    /// </summary>
    public class PagamentoController : IPagamentoController
    {
        private readonly IConfiguration _configuration;
        private readonly IMediator _mediator;
        private readonly IValidator<Pedido> _validator;

        public PagamentoController(IConfiguration configuration, IMediator mediator,
            IValidator<Pedido> validator)
        {
            _configuration = configuration;
            _mediator = mediator;
            _validator = validator;
        }

        /// <summary>
        /// Valida a entidade
        /// </summary>
        /// <param name="entity">Entidade</param>
        public async Task<ModelResult> ValidateAsync(Pedido entity)
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
        /// Consulta o pagamento de um pedido.
        /// </summary> 
        public async Task<ModelResult> ConsultarPagamentoAsync(Guid id)
        {
            PedidoConsultarPagamentoCommand command = new(id);
            return await _mediator.Send(command);
        }


        /// <summary>
        ///  Notificação de pedido aguardando pagamento.
        /// </summary>
        public async Task<ModelResult> ReceberPedido(Pedido notificacao)
        {
            if (notificacao == null) throw new InvalidOperationException($"Necessário informar o pedido");

            ModelResult ValidatorResult = new ModelResult(notificacao);

            FluentValidation.Results.ValidationResult validations = _validator.Validate(notificacao);
            if (!validations.IsValid)
            {
                ValidatorResult.AddValidations(validations);
                return ValidatorResult;
            }

            if (ValidatorResult.IsValid)
            {
                ReceberPedidoCommand command = new(notificacao);
                return await _mediator.Send(command);
            }

            return ValidatorResult;
        }

        /// <summary>
        ///  Mercado pago recebimento de notificação webhook.
        ///  https://www.mercadopago.com.br/developers/pt/docs/your-integrations/notifications/webhooks#editor_13
        /// </summary>
        public async Task<ModelResult> MercadoPagoWebhoock(MercadoPagoWebhoock notificacao, Guid idPedido, IHeaderDictionary headers)
        {
            if (notificacao == null) throw new InvalidOperationException($"Necessário informar a notificacao");

            var warnings = new List<string>();
            if (!headers.ContainsKey("client_id"))
                warnings.Add("Consumidor não autorizado e/ou inválido!");
            else if (!headers["client_id"].Equals(_configuration["WebhookClientAutorized"]))
                warnings.Add("Consumidor não autorizado e/ou inválido!");

            MercadoPagoWebhoockCommand command =
                new(notificacao, idPedido, _configuration["micro-servico-pedido-baseadress"] ?? "", warnings.ToArray());

            return await _mediator.Send(command);
        }
    }
}
