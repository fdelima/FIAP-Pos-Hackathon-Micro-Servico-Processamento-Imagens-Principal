using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Models;
using MediatR;

namespace FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Application.UseCases.ProcessamentoImagem.Commands
{
    public class ProcessamentoImagemSendMessageToQueueCommand : IRequest<ModelResult>
    {
        public ProcessamentoImagemSendMessageToQueueCommand(string queueName)
        {
            QueueName = queueName;
        }

        public string QueueName { get; private set; }
    }
}