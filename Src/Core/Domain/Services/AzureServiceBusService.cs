using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Interfaces;

namespace FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Services
{
    public class AzureServiceBusService : IMessagerService
    {
        IMessagerGateway _messagerGateway;

        public AzureServiceBusService(IMessagerGateway messagerGateway)
        {
            _messagerGateway = messagerGateway;
        }
        public Task<string[]> ReceiveMessagesAsync(string queueName)
        {
            return _messagerGateway.ReceiveMessagesAsync(queueName);
        }

        public Task SendMessageAsync(string queueName, string messageBody)
        {
            return _messagerGateway.SendMessageAsync(queueName, messageBody);
        }
    }
}
