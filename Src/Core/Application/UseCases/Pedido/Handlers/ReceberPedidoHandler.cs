using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Application.UseCases.Pedido.Commands;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Interfaces;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Models;
using MediatR;

namespace FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Application.UseCases.Pedido.Handlers
{
    public class ReceberPedidoHandler : IRequestHandler<ReceberPedidoCommand, ModelResult>
    {
        private readonly IPedidoService _service;

        public ReceberPedidoHandler(IPedidoService service)
        {
            _service = service;
        }

        public async Task<ModelResult> Handle(ReceberPedidoCommand command, CancellationToken cancellationToken = default)
        {
            return await _service.ReceberPedido(command.Entity, command.BusinessRules);
        }
    }
}
