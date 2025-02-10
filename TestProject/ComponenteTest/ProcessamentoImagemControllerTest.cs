using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Entities;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;
using TestProject.Infra;
using Xunit.Gherkin.Quick;

namespace TestProject.ComponenteTest
{
    [FeatureFile("./BDD/Features/ControlarProcessamentoImagens.feature")]
    public class ProcessamentoImagemControllerTest : Feature, IClassFixture<ComponentTestsBase>
    {
        private readonly ApiTestFixture _apiTest;
        private ModelResult expectedResult;
        ProcessamentoImagem _ProcessamentoImagem;
        ProcessamentoImagemUploadModel _ProcessamentoImagemUpload;

        /// <summary>
        /// Construtor da classe de teste.
        /// </summary>
        public ProcessamentoImagemControllerTest(ComponentTestsBase data)
        {
            _apiTest = data._apiTest;
        }
        private class ActionResult
        {
            public List<string> Messages { get; set; }
            public List<string> Errors { get; set; }
            public ProcessamentoImagem Model { get; set; }
            public bool IsValid { get; set; }
        }

        [Given(@"Recebendo um ProcessamentoImagem")]
        public void PrepararProcessamentoImagem()
        {
            _ProcessamentoImagem = new ProcessamentoImagem
            {
                Data = DateTime.Now,
                Usuario = "fiap@tech.com",
                DataEnvio = new DateTime(2024, 3, 1),
                NomeArquivo = "Nome Arquivo 1",
                TamanhoArquivo = 1000,
                NomeArquivoZipDownload = "Nome Arquivo Zip Download 1"
            };
        }

        [And(@"Adicionar o ProcessamentoImagem")]
        public async Task AdicionarProcessamentoImagem()
        {
            expectedResult = ModelResultFactory.InsertSucessResult<ProcessamentoImagem>(_ProcessamentoImagem);
            IFormFile formFile;
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes("conteudo")))
            {
                formFile = new FormFile(stream, 0, stream.Length, "FormFile", "arquivo.txt")
                {
                    Headers = new HeaderDictionary(),
                    ContentType = "text/plain"
                };
            }
            var client = _apiTest.GetClient();
            var conteudo = new MultipartFormDataContent();
            conteudo.Add(new ByteArrayContent(File.ReadAllBytes("video_test.mp4")), "FormFile", "video_test.mp4");

            HttpResponseMessage response = await client.PostAsync(
                "api/ProcessamentoImagem?Data=2025-02-09T05%3A15%3A40.964Z&Usuario=Lima", conteudo);

            var responseContent = await response.Content.ReadAsStringAsync();
            var actualResult = JsonConvert.DeserializeObject<ActionResult>(responseContent);
            Console.WriteLine(responseContent);
            _ProcessamentoImagem = actualResult.Model;

            Assert.Equal(expectedResult.IsValid, actualResult.IsValid);
            Assert.Equal(expectedResult.Messages, actualResult.Messages);
            Assert.Equal(expectedResult.Errors, actualResult.Errors);

            Assert.True(true);
        }

        [And(@"Encontrar o ProcessamentoImagem")]
        public async Task EncontrarProcessamentoImagem()
        {
            expectedResult = ModelResultFactory.SucessResult(_ProcessamentoImagem);

            var client = _apiTest.GetClient();
            HttpResponseMessage response = await client.GetAsync(
                $"api/ProcessamentoImagem/{_ProcessamentoImagem.IdProcessamentoImagem}");

            var responseContent = await response.Content.ReadAsStringAsync();
            var actualResult = JsonConvert.DeserializeObject<ActionResult>(responseContent);
            _ProcessamentoImagem = actualResult.Model;

            Assert.Equal(expectedResult.IsValid, actualResult.IsValid);
            Assert.Equal(expectedResult.Messages, actualResult.Messages);
            Assert.Equal(expectedResult.Errors, actualResult.Errors);
        }

        [And(@"Alterar o ProcessamentoImagem")]
        public async Task AlterarProcessamentoImagem()
        {
            expectedResult = ModelResultFactory.UpdateSucessResult<ProcessamentoImagem>(_ProcessamentoImagem);

            var client = _apiTest.GetClient();
            HttpResponseMessage response = await client.PutAsJsonAsync(
                $"api/ProcessamentoImagem/{_ProcessamentoImagem.IdProcessamentoImagem}", _ProcessamentoImagem);

            var responseContent = await response.Content.ReadAsStringAsync();
            var actualResult = JsonConvert.DeserializeObject<ActionResult>(responseContent);
            _ProcessamentoImagem = actualResult.Model;

            Assert.Equal(expectedResult.IsValid, actualResult.IsValid);
            Assert.Equal(expectedResult.Messages, actualResult.Messages);
            Assert.Equal(expectedResult.Errors, actualResult.Errors);
        }

        [When(@"Consultar o ProcessamentoImagem")]
        public async Task ConsultarProcessamentoImagem()
        {
            var client = _apiTest.GetClient();
            HttpResponseMessage response = await client.PostAsJsonAsync(
                $"api/ProcessamentoImagem/consult", new PagingQueryParam<ProcessamentoImagem> { ObjFilter = _ProcessamentoImagem });

            var responseContent = await response.Content.ReadAsStringAsync();
            dynamic actualResult = JsonConvert.DeserializeObject(responseContent);

            Assert.True(actualResult.content != null);
        }

        [Then(@"posso deletar o ProcessamentoImagem")]
        public async Task DeletarProcessamentoImagem()
        {
            expectedResult = ModelResultFactory.DeleteSucessResult<ProcessamentoImagem>();

            var client = _apiTest.GetClient();
            HttpResponseMessage response = await client.DeleteAsync(
                $"api/ProcessamentoImagem/{_ProcessamentoImagem.IdProcessamentoImagem}");

            var responseContent = await response.Content.ReadAsStringAsync();
            var actualResult = JsonConvert.DeserializeObject<ActionResult>(responseContent);
            _ProcessamentoImagem = null;

            Assert.Equal(expectedResult.IsValid, actualResult.IsValid);
            Assert.Equal(expectedResult.Messages, actualResult.Messages);
            Assert.Equal(expectedResult.Errors, actualResult.Errors);
        }
    }
}
