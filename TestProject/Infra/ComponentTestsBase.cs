﻿namespace TestProject.Infra
{
    public class ComponentTestsBase : IDisposable
    {
        private readonly AzuriteTestFixture _azuriteTestFixture;
        private readonly MongoTestFixture _mongoTestFixture;
        internal readonly ApiTestFixture _apiTest;
        private static int _tests = 0;

        public ComponentTestsBase()
        {
            _tests += 1;
            
            _apiTest = new ApiTestFixture();
            Thread.Sleep(25000);

            _azuriteTestFixture = new AzuriteTestFixture(
                databaseContainerName: "azurite-processamento-imagens-principal-component-test");
            Thread.Sleep(25000);

            _mongoTestFixture = new MongoTestFixture(
                databaseContainerName: "mongodb-processamento-imagens-principal-component-test", port: "27021");
            Thread.Sleep(25000);

        }

        public void Dispose()
        {
            _tests -= 1;
            if (_tests == 0)
            {
                _azuriteTestFixture.Dispose();
                _mongoTestFixture.Dispose();
                _apiTest.Dispose();
            }
        }
    }
}
