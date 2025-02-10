using Microsoft.EntityFrameworkCore;

namespace TestProject.Infra
{
    public class MongoTestFixture : IDisposable
    {
        private const string _imageName = "mongo:latest";
        private const string _dataBaseName = "hackathon-microservico-processamento-imagens-principal-grupo-71";
        string _port = string.Empty; string _databaseContainerName = string.Empty;

        public MongoTestFixture(string databaseContainerName, string port)
        {
            _databaseContainerName = databaseContainerName;
            _port = port;

            if (DockerManager.UseDocker())
            {
                if (!DockerManager.ContainerIsRunning(databaseContainerName))
                {
                    DockerManager.PullImageIfDoesNotExists(_imageName);
                    DockerManager.KillContainer(databaseContainerName);
                    DockerManager.KillVolume(databaseContainerName);

                    DockerManager.CreateNetWork();

                    DockerManager.RunContainerIfIsNotRunning(databaseContainerName,
                        $"run --name {databaseContainerName} " +
                        $"-p {port}:27017 " +
                        $"--network {DockerManager.NETWORK} " +
                        $"-d {_imageName}");

                    Thread.Sleep(3000);
                }
            }
        }

        public FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Infra.Context GetDbContext()
        {
            string connectionString = $"mongodb://localhost:{_port}";

            var options = new DbContextOptionsBuilder<FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Infra.Context>()
                                .UseMongoDB(connectionString, _dataBaseName).Options;

            return new FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Infra.Context(options);
        }

        public void Dispose()
        {
            if (DockerManager.UseDocker())
            {
                DockerManager.KillContainer(_databaseContainerName);
                DockerManager.KillVolume(_databaseContainerName);
            }
            GC.SuppressFinalize(this);
        }
    }
}
