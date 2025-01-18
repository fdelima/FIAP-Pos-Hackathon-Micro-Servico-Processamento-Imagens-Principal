using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Entities;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Models;
using MediatR;

namespace FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Application.UseCases.MercadoPago.Commands
{
    public class MercadoPagoWebhoockCommand : IRequest<ModelResult>
    {
        public MercadoPagoWebhoockCommand(MercadoPagoWebhoock entity,
            Guid idPedido,
            string microServicoPedidoBaseAdress,
            string[]? businessRules = null)
        {
            Entity = entity;
            IdPedido = idPedido;
            MicroServicoPedidoBaseAdress = microServicoPedidoBaseAdress;
            BusinessRules = businessRules;
        }

        public MercadoPagoWebhoock Entity { get; private set; }
        public Guid IdPedido { get; private set; }
        public string MicroServicoPedidoBaseAdress { get; private set; }
        public string[]? BusinessRules { get; private set; }
    }
}