using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Application.UseCases.ProcessamentoImagem.Commands;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Interfaces;
using MediatR;

namespace FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Application.UseCases.ProcessamentoImagem.Handlers
{
    public class ProcessamentoImagemGetItemsHandler : IRequestHandler<ProcessamentoImagemGetItemsCommand, PagingQueryResult<Domain.Entities.ProcessamentoImagem>>
    {
        private readonly IProcessamentoImagemService _service;

        public ProcessamentoImagemGetItemsHandler(IProcessamentoImagemService service)
        {
            _service = service;
        }

        public async Task<PagingQueryResult<Domain.Entities.ProcessamentoImagem>> Handle(ProcessamentoImagemGetItemsCommand command, CancellationToken cancellationToken = default)
        {
            if (command.Expression == null)
                return await _service.GetItemsAsync(command.Filter, command.SortProp);
            else
                return await _service.GetItemsAsync(command.Filter, command.Expression, command.SortProp);
        }
    }
}
