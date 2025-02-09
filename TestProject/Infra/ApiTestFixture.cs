using System.Net.Http.Headers;

namespace TestProject.Infra
{
    public class ApiTestFixture : IDisposable
    {
        const string port = "5000";

        //api
        private const string ImageName = "fdelima/fiap-pos-hackathon-micro-servico-processamento-imagens-principal-gurpo-71-api:fase5";
        private const string DatabaseContainerName = "api-processamento-imagens-principal-test";
        private const string DataBaseName = "hackathon-microservico-processamento-imagens-principal-grupo-71";
        private HttpClient _client;

        public ApiTestFixture()
        {
            if (DockerManager.UseDocker())
            {
                if (!DockerManager.ContainerIsRunning(DatabaseContainerName))
                {
                    DockerManager.PullImageIfDoesNotExists(ImageName);
                    DockerManager.KillContainer(DatabaseContainerName);
                    DockerManager.KillVolume(DatabaseContainerName);

                    DockerManager.CreateNetWork();

                    DockerManager.RunContainerIfIsNotRunning(DatabaseContainerName,
                        $"run --name {DatabaseContainerName} " +
                        $"-e ASPNETCORE_ENVIRONMENT=Test " +
                        $"-p {port}:8080 " +
                        $"--network {DockerManager.NETWORK} " +
                        $"-d {ImageName}");

                    Thread.Sleep(3000);
                }
            }
        }

        public HttpClient GetClient()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri($"http://localhost:{port}/");
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            return _client;
        }

        public void Dispose()
        {
            if (DockerManager.UseDocker())
            {
                DockerManager.KillContainer(DatabaseContainerName);
                DockerManager.KillVolume(DatabaseContainerName);
            }
            GC.SuppressFinalize(this);
        }
    }
}
