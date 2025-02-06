
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Models;

namespace FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Interfaces
{
    public interface IMessagerGateway
    {
        Task<MessageModel?> ReceiveMessageAsync();
        Task SendMessageAsync(string messageBody);
        Task DeleteMessageAsync(MessageModel message);
    }
}