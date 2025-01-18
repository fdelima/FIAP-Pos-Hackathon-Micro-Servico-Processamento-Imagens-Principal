using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Application.UseCases.MercadoPago.Commands;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Interfaces;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Models;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.ValuesObject;
using MediatR;
using System.Net.Http.Json;

namespace FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Application.UseCases.MercadoPago.Handlers
{
    public class MercadoPagoWebhoockHandler : IRequestHandler<MercadoPagoWebhoockCommand, ModelResult>
    {
        private readonly IPedidoService _service;

        public MercadoPagoWebhoockHandler(IPedidoService service)
        {
            _service = service;
        }

        public async Task<ModelResult> Handle(MercadoPagoWebhoockCommand command, CancellationToken cancellationToken = default)
        {
            var result = await _service.MercadoPagoWebhoock(command.Entity, command.IdPedido, command.BusinessRules);

            if (result.IsValid)
            {
                try
                {
                    var producaoClient = Util.GetClient(command.MicroServicoPedidoBaseAdress);

                    HttpResponseMessage response =
                     await producaoClient.PutAsJsonAsync($"api/Pedido/ReceberStatusPagamento?id={command.IdPedido}&statusPagamento={enmPedidoStatusPagamento.APROVADO}", command.Entity);

                    if (!response.IsSuccessStatusCode)
                        result.AddMessage("Não foi possível enviar status do pagamento do pedido.");
                }
                catch (Exception)
                {
                    result.AddMessage("Falha ao conectar ao pedido.");
                }
            }

            return result;
        }
    }
}
