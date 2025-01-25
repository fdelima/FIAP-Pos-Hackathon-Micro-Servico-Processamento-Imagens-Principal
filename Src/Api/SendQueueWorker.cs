using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Interfaces;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Models;
using MongoDB.Bson.IO;
using System.Text.Json;

namespace FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Api
{
    /// <summary>
    /// Serviço responsável por enviar os arquivos recebidos para fila de processamento
    /// Padrão de projeto Transactional outbox
    /// Saga Coreografada
    /// <see cref="https://microservices.io/patterns/data/transactional-outbox.html"/>
    /// <see cref="https://learn.microsoft.com/pt-br/azure/architecture/patterns/saga#choreography"/>
    /// </summary>
    public class SendQueueWorker : BackgroundService
    {
        IProcessamentoImagemController _processamentoImagemController;

        public SendQueueWorker(IProcessamentoImagemController processamentoImagemController)
        {
            _processamentoImagemController = processamentoImagemController;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            ModelResult result;

            while (!stoppingToken.IsCancellationRequested)
            {
                Console.WriteLine("Worker Send Service executando...");
                result = await _processamentoImagemController.SendMessageToQueueAsync(Constants.MESSAGER_QUEUE_TO_PROCESS_NAME);
                Console.Write(JsonSerializer.Serialize(result));

                Console.WriteLine("Worker Send Service aguardando...");
                await Task.Delay(TimeSpan.FromSeconds(60), stoppingToken);

                Console.WriteLine("Worker Receiver Service executando...");
                await _processamentoImagemController.ReceiverMessageInQueueAsync(Constants.MESSAGER_QUEUE_PROCESSED_NAME);
                Console.Write(JsonSerializer.Serialize(result));

                Console.WriteLine("Worker Receiver Service aguardando...");
                await Task.Delay(TimeSpan.FromSeconds(60), stoppingToken);
            }
        }
    }

}
