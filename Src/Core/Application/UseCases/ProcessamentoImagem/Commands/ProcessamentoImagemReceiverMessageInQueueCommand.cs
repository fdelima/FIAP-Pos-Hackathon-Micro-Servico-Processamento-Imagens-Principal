using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Models;
using MediatR;

namespace FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Application.UseCases.ProcessamentoImagem.Commands
{
    public class ProcessamentoImagemReceiverMessageInQueueCommand : IRequest<ModelResult>
    {
        public ProcessamentoImagemReceiverMessageInQueueCommand(string queueName)
        {
            QueueName = queueName;
        }

        public string QueueName { get; private set; }
    }
}