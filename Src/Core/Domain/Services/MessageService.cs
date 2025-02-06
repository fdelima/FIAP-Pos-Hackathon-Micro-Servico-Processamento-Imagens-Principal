using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Interfaces;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Models;

namespace FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Services
{
    public class MessageService : IMessagerService
    {
        IMessagerGateway _messagerGateway;

        public MessageService(IMessagerGateway messagerGateway)
        {
            _messagerGateway = messagerGateway;
        }
        public Task DeleteMessageAsync(MessageModel message)
        {
            return _messagerGateway.DeleteMessageAsync(message);
        }

        public Task<MessageModel?> ReceiveMessageAsync()
        {
            return _messagerGateway.ReceiveMessageAsync();
        }

        public Task SendMessageAsync(string messageBody)
        {
            return _messagerGateway.SendMessageAsync(messageBody);
        }
    }
}
