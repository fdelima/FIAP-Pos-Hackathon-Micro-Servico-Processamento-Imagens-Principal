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
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                // Código do seu Worker Service
                Console.WriteLine("Worker Service executando...");
                await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
            }
        }
    }

}
