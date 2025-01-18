using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Entities;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Models;
using Microsoft.AspNetCore.Http;

namespace FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Interfaces
{
    public interface IPagamentoController
    {
        /// <summary>
        /// Valida o objeto
        /// </summary>
        /// <param name="entity">Objeto relacional do bd mapeado</param>
        Task<ModelResult> ValidateAsync(Pedido entity);

        /// <summary>
        /// Consulta o pagamento de um pedido.
        /// </summary> 
        Task<ModelResult> ConsultarPagamentoAsync(Guid id);


        /// <summary>
        ///  Notificação de pedido aguardando pagamento.
        /// </summary>
        Task<ModelResult> ReceberPedido(Pedido notificacao);

        /// <summary>
        ///  Mercado pago recebimento de notificação webhook.
        ///  https://www.mercadopago.com.br/developers/pt/docs/your-integrations/notifications/webhooks#editor_13
        /// </summary>
        Task<ModelResult> MercadoPagoWebhoock(MercadoPagoWebhoock notificacao, Guid idPedido, IHeaderDictionary headers);

    }
}
