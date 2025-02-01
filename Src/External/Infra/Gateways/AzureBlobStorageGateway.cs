using Azure.Storage.Blobs;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Interfaces;
using Microsoft.Extensions.Configuration;

namespace FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Infra.Gateways
{
    public class AzureBlobStorageGateway : IStorageGateway
    {
        private readonly string _connectionString;

        public AzureBlobStorageGateway(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString(Constants.AZ_STORAGE_CONN_NAME) ?? "";
        }

        public async Task UploadFileAsync(string containerName, string fileName, Stream fileStream)
        {
            var blobServiceClient = new BlobServiceClient(_connectionString);
            var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            await containerClient.CreateIfNotExistsAsync();

            var blobClient = containerClient.GetBlobClient(fileName);
            fileStream.Position = 0;
            await blobClient.UploadAsync(fileStream, true);
        }

        public async Task DownloadFileAsync(string containerName, string fileName, string localFilePath)
        {
            var blobServiceClient = new BlobServiceClient(_connectionString);
            var containerClient = blobServiceClient.GetBlobContainerClient(containerName);

            var blobClient = containerClient.GetBlobClient(fileName);
            await blobClient.DownloadToAsync(localFilePath);
        }

        public async Task DeleteFileAsync(string containerName, string fileName)
        {
            // Cria um cliente para interagir com o serviço de blobs
            BlobServiceClient blobServiceClient = new BlobServiceClient(_connectionString);

            // Obtém o cliente do contêiner
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);

            // Obtém o cliente do blob
            BlobClient blobClient = containerClient.GetBlobClient(fileName);

            // Exclui o blob
            await blobClient.DeleteAsync();
        }
    }
}
