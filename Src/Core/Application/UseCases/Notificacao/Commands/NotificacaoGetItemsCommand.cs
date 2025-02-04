using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Interfaces;
using MediatR;
using System.Linq.Expressions;

namespace FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Application.UseCases.Notificacao.Commands
{
    public class NotificacaoGetItemsCommand : IRequest<PagingQueryResult<Domain.Entities.Notificacao>>
    {
        public NotificacaoGetItemsCommand(IPagingQueryParam filter, Expression<Func<Domain.Entities.Notificacao, object>> sortProp)
        {
            Filter = filter;
            SortProp = sortProp;
        }

        public NotificacaoGetItemsCommand(IPagingQueryParam filter,
            Expression<Func<Domain.Entities.Notificacao, bool>> expression, Expression<Func<Domain.Entities.Notificacao, object>> sortProp)
            : this(filter, sortProp)
        {
            Expression = expression;
        }

        public IPagingQueryParam Filter { get; }
        public Expression<Func<Domain.Entities.Notificacao, bool>> Expression { get; }

        public Expression<Func<Domain.Entities.Notificacao, object>> SortProp { get; }
    }
}