using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Application.UseCases.Pedido.Commands;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Interfaces;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Models;
using MediatR;

namespace FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Application.UseCases.Pedido.Handlers
{
    public class PedidoConsultarPagamentoHandler : IRequestHandler<PedidoConsultarPagamentoCommand, ModelResult>
    {
        private readonly IPedidoService _service;

        public PedidoConsultarPagamentoHandler(IPedidoService service)
        {
            _service = service;
        }

        public async Task<ModelResult> Handle(PedidoConsultarPagamentoCommand command, CancellationToken cancellationToken = default)
        {
            return await _service.ConsultarPagamentoAsync(command.Id);
        }
    }
}
