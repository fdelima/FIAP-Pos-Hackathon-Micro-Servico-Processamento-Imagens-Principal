using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Entities;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Interfaces;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Models;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Models.MercadoPago;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Api.Controllers
{
    //TODO: Controller :: 1 - Duplicar esta controller de exemplo e trocar o nome da entidade.
    /// <summary>
    /// Controller dos Pedidos cadastrados
    /// </summary>
    [Route("api/[Controller]")]
    public class PagamentoController : ApiController
    {
        private readonly IPagamentoController _controller;

        /// <summary>
        /// Construtor do controller dos Pedidos cadastrados
        /// </summary>
        public PagamentoController(IPagamentoController controller)
        {
            _controller = controller;
        }

        /// <summary>
        /// Consulta o pagamento do pedido.
        /// </summary>
        /// <param name="id">Identificador do Pedido.</param>
        /// <returns>Retorna o result do Pedido cadastrado.</returns>
        /// <response code="200">Pedido encontrado.</response>
        /// <response code="400">Pedido não encontrado.</response>
        [HttpGet("Consultar/Pedido/{id}")]
        [ProducesResponseType(typeof(ModelResult), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ModelResult), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> ConsultarPagamentoAsync(Guid id)
        {
            return ExecuteCommand(await _controller.ConsultarPagamentoAsync(id));
        }

        /// <summary>
        ///  Notificação de pedido aguardando pagamento.
        /// </summary>
        [HttpPost("Pedido")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> ReceberPedido(Pedido notificacao)
        {
            return ExecuteCommand(await _controller.ReceberPedido(notificacao));
        }

        /* [ Fazer caso de tempo ]*/

        /// <summary>
        ///  Mercado pago recebimento de notificação webhook.
        ///  https://www.mercadopago.com.br/developers/pt/docs/your-integrations/notifications/webhooks#editor_13
        /// </summary>
        [HttpPost("MercadoPagoWebhoock")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> MercadoPagoWebhoock(MercadoPagoWebhoockModel notificacao)
        {
            var mercadoPagoWebhoock = (MercadoPagoWebhoock)notificacao;
            return ExecuteCommand(await _controller.MercadoPagoWebhoock(mercadoPagoWebhoock, Guid.Parse(notificacao.Data.Id), Request.Headers));
        }
    }
}