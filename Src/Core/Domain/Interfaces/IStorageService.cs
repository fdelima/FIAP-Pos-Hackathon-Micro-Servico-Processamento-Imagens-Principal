
namespace FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Interfaces
{
    public interface IStorageService
    {
        Task DeleteFileAsync(string containerName, string fileName);
        Task DownloadFileAsync(string containerName, string fileName, Stream destination);
        Task UploadFileAsync(string containerName, string fileName, Stream fileStream);
    }
}