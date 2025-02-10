using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Application.UseCases.Notificacao.Commands;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Interfaces;
using MediatR;

namespace FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Application.UseCases.Notificacao.Handlers
{
    public class NotificacaoGetItemsHandler : IRequestHandler<NotificacaoGetItemsCommand, PagingQueryResult<Domain.Entities.Notificacao>>
    {
        private readonly IService<Domain.Entities.Notificacao> _service;

        public NotificacaoGetItemsHandler(IService<Domain.Entities.Notificacao> service)
        {
            _service = service;
        }

        public async Task<PagingQueryResult<Domain.Entities.Notificacao>> Handle(NotificacaoGetItemsCommand command, CancellationToken cancellationToken = default)
        {
            if (command.Expression == null)
                return await _service.GetItemsAsync(command.Filter, command.SortProp);
            else
                return await _service.GetItemsAsync(command.Filter, command.Expression, command.SortProp);
        }
    }
}
