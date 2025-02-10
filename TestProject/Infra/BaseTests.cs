namespace TestProject.Infra
{
    public class BaseTests : IDisposable
    {
        private readonly AzuriteTestFixture _azuriteTestFixture;
        private readonly MongoTestFixture _mongoComponentTestFixture;
        internal readonly MongoTestFixture _mongoIntegrationTestFixture;
        internal readonly ApiTestFixture _apiTest;
        private static int _tests = 0;

        public BaseTests()
        {
            _tests += 1;

            _apiTest = new ApiTestFixture();

            _azuriteTestFixture = new AzuriteTestFixture(
                databaseContainerName: "azurite-processamento-imagens-principal-component-test");

            _mongoComponentTestFixture = new MongoTestFixture(
                databaseContainerName: "mongodb-processamento-imagens-principal-component-test",
                port: "27019");

            _mongoIntegrationTestFixture = new MongoTestFixture(
                databaseContainerName: "mongodb-processamento-imagens-principal-integration-test",
                port: "27021");

            Thread.Sleep(15000);

        }

        public void Dispose()
        {
            _tests -= 1;
            if (_tests == 0)
            {
                _azuriteTestFixture.Dispose();
                _mongoComponentTestFixture.Dispose();
                _mongoIntegrationTestFixture.Dispose();
                _apiTest.Dispose();
            }
        }
    }
}
