﻿using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Interfaces;

namespace FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Services
{
    public class AzureBlobStorageService : IStorageService
    {
        IStorageGateway _gateway;

        public AzureBlobStorageService(IStorageGateway gateway)
        {
            _gateway = gateway;
        }

        public Task DeleteFileAsync(string containerName, string fileName)
        {
            return _gateway.DeleteFileAsync(containerName, fileName);
        }

        public Task DownloadFileAsync(string containerName, string fileName, Stream destination)
        {
            return _gateway.DownloadFileAsync(containerName, fileName, destination);
        }

        public Task UploadFileAsync(string containerName, string fileName, Stream fileStream)
        {
            return _gateway.UploadFileAsync(containerName, fileName, fileStream);
        }
    }
}
