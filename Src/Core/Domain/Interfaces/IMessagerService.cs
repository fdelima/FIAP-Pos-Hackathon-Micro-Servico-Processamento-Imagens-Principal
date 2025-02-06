
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Models;

namespace FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Interfaces
{
    public interface IMessagerService
    {
        Task DeleteMessageAsync(MessageModel message);
        Task<MessageModel?> ReceiveMessageAsync();
        Task SendMessageAsync(string messageBody);
    }
}