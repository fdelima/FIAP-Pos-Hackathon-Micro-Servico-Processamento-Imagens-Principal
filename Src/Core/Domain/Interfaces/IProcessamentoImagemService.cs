using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Entities;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Models;

namespace FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Interfaces
{
    /// <summary>
    /// Interface regulamentando os métodos que precisam ser impementados pelos serviços
    /// </summary>
    public interface IProcessamentoImagemService : IService<ProcessamentoImagem>
    {
        /// <summary>
        /// Lê as mensagens dos arquivos processados.
        /// </summary>
        Task<ModelResult> ReceiverMessageInQueueAsync();

        /// <summary>
        /// Envia as mensagens dos arquivos recebidos para a fila.
        /// </summary>
        Task<ModelResult> SendMessageToQueueAsync();
    }
}
