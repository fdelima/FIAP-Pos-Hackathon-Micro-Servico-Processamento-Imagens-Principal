using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Entities;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Models;

namespace FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Interfaces
{
    /// <summary>
    /// Interface regulamentando os métodos que precisam ser impementados pelos serviços da aplicação
    /// </summary>
    public interface IProcessamentoImagemController : IController<ProcessamentoImagem>
    {

        /// <summary>
        /// Insere o objeto
        /// </summary>
        /// <param name="entity">Objeto relacional do bd mapeado</param>
        Task<ModelResult> PostAsync(ProcessamentoImagemUploadModel entity);

        /// <summary>
        /// Lê as mensagens dos arquivos processados.
        /// </summary>
        Task<ModelResult> ReceiverMessageInQueueAsync();

        /// <summary>
        /// Envia as mensagens dos arquivos recebidos para a fila.
        /// </summary>
        Task<ModelResult> SendMessageToQueueAsync();

        /// <summary>
        /// Download do arquivo
        /// </summary>
        Task<ModelResult> DownloadAsync(Guid id);
        
        /// <summary>
        /// Status do processamento
        /// </summary>
        Task<ModelResult> StatusByIdAsync(Guid id);
    }
}
