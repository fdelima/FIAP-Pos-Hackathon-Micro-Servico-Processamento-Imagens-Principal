using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Entities;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Models;
using Newtonsoft.Json;
using System.Net.Http.Json;
using TestProject.Infra;
using Xunit.Gherkin.Quick;

namespace TestProject.ComponenteTest
{
    /// <summary>
    /// Classe de teste.
    /// </summary>
    [FeatureFile("./BDD/Features/ControlarNotificacaos.feature")]
    public class NotificacaoControllerTest : Feature, IClassFixture<BaseTests>
    {
        private readonly ApiTestFixture _apiTest;
        private ModelResult expectedResult;
        Notificacao _notificacao;

        /// <summary>
        /// Construtor da classe de teste.
        /// </summary>
        public NotificacaoControllerTest(BaseTests data)
        {
            _apiTest = data._apiTest;
        }
        private class ActionResult
        {
            public List<string> Messages { get; set; }
            public List<string> Errors { get; set; }
            public Notificacao Model { get; set; }
            public bool IsValid { get; set; }
        }

        [Given(@"Recebendo uma notificacao do processamento de imagem vamos preparar o notificacao")]
        public void PrepararNotificacao()
        {
            _notificacao = new Notificacao
            {
                Usuario = "Usuário",
                Mensagem = "Mensagem de teste",
            };
        }

        [And(@"Adicionar o notificacao")]
        public async Task AdicionarNotificacao()
        {
            expectedResult = ModelResultFactory.InsertSucessResult<Notificacao>(_notificacao);

            var client = _apiTest.GetClient();
            HttpResponseMessage response = await client.PostAsJsonAsync(
                "api/notificacao", _notificacao);

            var responseContent = await response.Content.ReadAsStringAsync();
            var actualResult = JsonConvert.DeserializeObject<ActionResult>(responseContent);

            _notificacao = actualResult.Model;

            Assert.Equal(expectedResult.IsValid, actualResult.IsValid);
            Assert.Equal(expectedResult.Messages, actualResult.Messages);
            Assert.Equal(expectedResult.Errors, actualResult.Errors);

            Assert.True(true);
        }

        [And(@"Encontrar o notificacao")]
        public async Task EncontrarNotificacao()
        {
            expectedResult = ModelResultFactory.SucessResult(_notificacao);

            var client = _apiTest.GetClient();
            HttpResponseMessage response = await client.GetAsync(
                $"api/notificacao/{_notificacao.IdNotificacao}");

            var responseContent = await response.Content.ReadAsStringAsync();
            var actualResult = JsonConvert.DeserializeObject<ActionResult>(responseContent);
            _notificacao = actualResult.Model;

            Assert.Equal(expectedResult.IsValid, actualResult.IsValid);
            Assert.Equal(expectedResult.Messages, actualResult.Messages);
            Assert.Equal(expectedResult.Errors, actualResult.Errors);
        }

        [And(@"Alterar o notificacao")]
        public async Task AlterarNotificacao()
        {
            expectedResult = ModelResultFactory.UpdateSucessResult<Notificacao>(_notificacao);

            var client = _apiTest.GetClient();
            HttpResponseMessage response = await client.PutAsJsonAsync(
                $"api/notificacao/{_notificacao.IdNotificacao}", _notificacao);

            var responseContent = await response.Content.ReadAsStringAsync();
            var actualResult = JsonConvert.DeserializeObject<ActionResult>(responseContent);
            _notificacao = actualResult.Model;

            Assert.Equal(expectedResult.IsValid, actualResult.IsValid);
            Assert.Equal(expectedResult.Messages, actualResult.Messages);
            Assert.Equal(expectedResult.Errors, actualResult.Errors);
        }

        [When(@"Consultar o notificacao")]
        public async Task ConsultarNotificacao()
        {
            var client = _apiTest.GetClient();
            HttpResponseMessage response = await client.PostAsJsonAsync(
                $"api/notificacao/consult", new PagingQueryParam<Notificacao> { ObjFilter = _notificacao });

            var responseContent = await response.Content.ReadAsStringAsync();
            dynamic actualResult = JsonConvert.DeserializeObject(responseContent);

            Assert.True(actualResult.content != null);
        }

        [Then(@"posso deletar o notificacao")]
        public async Task DeletarNotificacao()
        {
            expectedResult = ModelResultFactory.DeleteSucessResult<Notificacao>();

            var client = _apiTest.GetClient();
            HttpResponseMessage response = await client.DeleteAsync(
                $"api/notificacao/{_notificacao.IdNotificacao}");

            var responseContent = await response.Content.ReadAsStringAsync();
            var actualResult = JsonConvert.DeserializeObject<ActionResult>(responseContent);
            _notificacao = null;

            Assert.Equal(expectedResult.IsValid, actualResult.IsValid);
            Assert.Equal(expectedResult.Messages, actualResult.Messages);
            Assert.Equal(expectedResult.Errors, actualResult.Errors);
        }
    }
}
