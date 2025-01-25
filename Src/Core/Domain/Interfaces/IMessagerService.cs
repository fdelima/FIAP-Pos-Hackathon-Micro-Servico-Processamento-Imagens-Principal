
namespace FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Interfaces
{
    public interface IMessagerService
    {
        Task<string[]> ReceiveMessagesAsync(string queueName);
        Task SendMessageAsync(string queueName, string messageBody);
    }
}