using Azure.Messaging.ServiceBus;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Interfaces;
using Microsoft.Extensions.Configuration;

namespace FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Infra.Gateways
{
    public class AzureServiceBusGateway : IMessagerGateway
    {
        private readonly string _connectionString;

        public AzureServiceBusGateway(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString(Constants.AZ_SERVICEBUS_CONN_NAME) ?? "";
        }

        public async Task SendMessageAsync(string queueName, string messageBody)
        {
            var client = new ServiceBusClient(_connectionString);
            var sender = client.CreateSender(queueName);

            await using (sender)
            {
                await sender.SendMessageAsync(new ServiceBusMessage(messageBody));
            }
        }

        public async Task<string[]> ReceiveMessagesAsync(string queueName)
        {
            var result = new List<string>();

            var client = new ServiceBusClient(_connectionString);
            var receiver = client.CreateReceiver(queueName);

            await using (receiver)
            {
                while (true)
                {
                    var message = await receiver.ReceiveMessageAsync();
                    if (message != null)
                    {
                        result.Add(message.Body.ToString());
                        Console.WriteLine($"Received: {message.Body}");
                        await receiver.CompleteMessageAsync(message);
                    }
                    else
                        break;
                }
            }

            return result.ToArray();
        }
    }
}
