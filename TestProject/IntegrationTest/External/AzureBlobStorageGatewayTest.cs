﻿using Azure.Storage.Blobs;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Interfaces;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Infra.Gateways;
using Microsoft.Extensions.Configuration;
using TestProject.Infra;

namespace TestProject.IntegrationTest.External
{
    public class AzureBlobStorageGatewayTest : IClassFixture<BaseTests>
    {
        private readonly IStorageGateway _storageGateway;
        private readonly string _containerName;
        private readonly string _fileName;
        private readonly string _localFilePath;
        private readonly Stream _fileStream;

        public AzureBlobStorageGatewayTest(BaseTests data)
        {
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    { $"ConnectionStrings:{Constants.AZ_STORAGE_CONN_NAME}", "UseDevelopmentStorage=true" },
                })
                .Build();

            _storageGateway = new AzureBlobStorageGateway(configuration);

            _containerName = "test-container";
            _fileName = "test-file.txt";
            _localFilePath = Path.GetTempFileName();
            _fileStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes("Test file content"));
        }

        [Fact]
        public async Task UploadFileAsync_ShouldUploadFileSuccessfully()
        {
            // Act
            await _storageGateway.UploadFileAsync(_containerName, _fileName, _fileStream);

            // Assert
            Assert.True(await FileExistsInStorageAsync(_containerName, _fileName));
        }

        [Fact]
        public async Task DownloadFileAsync_ShouldDownloadFileSuccessfully()
        {
            // Arrange
            await _storageGateway.UploadFileAsync(_containerName, _fileName, _fileStream);

            // Act
            var destination = new MemoryStream();
            await _storageGateway.DownloadFileAsync(_containerName, _fileName, destination);

            // Assert
            Assert.True(destination != null);
        }

        [Fact]
        public async Task DeleteFileAsync_ShouldDeleteFileSuccessfully()
        {
            // Arrange
            await _storageGateway.UploadFileAsync(_containerName, _fileName, _fileStream);

            // Act
            await _storageGateway.DeleteFileAsync(_containerName, _fileName);

            // Assert
            Assert.False(await FileExistsInStorageAsync(_containerName, _fileName));
        }

        private async Task<bool> FileExistsInStorageAsync(string containerName, string fileName)
        {
            var blobServiceClient = new BlobServiceClient("UseDevelopmentStorage=true");
            var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            var blobClient = containerClient.GetBlobClient(fileName);

            return await blobClient.ExistsAsync();
        }
    }
}