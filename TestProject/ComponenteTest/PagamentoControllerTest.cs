using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Entities;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Models;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Models.MercadoPago;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.ValuesObject;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System.Net.Http.Json;
using TestProject.Infra;
using Xunit.Gherkin.Quick;

namespace TestProject.ComponenteTest
{
    /// <summary>
    /// Classe de teste.
    /// </summary>
    [FeatureFile("./BDD/Features/ControlarPagamentos.feature")]
    public class PagamentoControllerTest : Feature, IClassFixture<ComponentTestsBase>
    {
        private readonly ApiTestFixture _apiTest;
        Pedido _pedido;

        /// <summary>
        /// Construtor da classe de teste.
        /// </summary>
        public PagamentoControllerTest(ComponentTestsBase data)
        {
            _apiTest = data._apiTest;
        }
        private class ActionResult<T>
        {
            public List<string> Messages { get; set; }
            public List<string> Errors { get; set; }
            public T Model { get; set; }
            public bool IsValid { get; set; }
        }

        [Given(@"Preparando o processo de pagamento de um pedido pendente")]
        public void PrepararPedido()
        {
            _pedido = new Pedido
            {
                IdPedido = Guid.NewGuid(),
                IdCliente = Guid.NewGuid(),
                IdDispositivo = Guid.NewGuid(),
                Data = DateTime.Now,
                Status = enmPedidoStatus.RECEBIDO.ToString(),
                DataStatusPedido = DateTime.Now,
                StatusPagamento = enmPedidoStatusPagamento.PENDENTE.ToString(),
                DataStatusPagamento = DateTime.Now
            };
        }

        [And(@"Recebendo um pedido pendente de pagamento")]
        public async Task ReceberPedido()
        {
            var client = _apiTest.GetClient();
            HttpResponseMessage response = await client.PostAsJsonAsync(
                "api/Pagamento/Pedido", _pedido);

            var responseContent = await response.Content.ReadAsStringAsync();
            var actualResult = JsonConvert.DeserializeObject<ActionResult<Pedido>>(responseContent);

            _pedido = actualResult.Model;

            Assert.True(actualResult.IsValid);
        }

        [And(@"Consultar status de pagamento do pedido pendente")]
        public async Task ConusultarStatusPagamentoPendente()
        {
            var client = _apiTest.GetClient();
            HttpResponseMessage response = await client.GetAsync(
                $"api/Pagamento/Consultar/Pedido/{_pedido.IdPedido}");

            var responseContent = await response.Content.ReadAsStringAsync();
            var actualResult = JsonConvert.DeserializeObject<ActionResult<string>>(responseContent);
            var statusPagamento = actualResult.Model;

            Assert.Equal(enmPedidoStatusPagamento.PENDENTE.ToString(), statusPagamento);
            Assert.True(actualResult.IsValid);
        }

        [When(@"Receber informacoes do pagamento do pedido via webhook")]
        public async Task ReceberInformacoesPagamento()
        {
            IHeaderDictionary headers = new HeaderDictionary(new Dictionary<String, StringValues>
            {
                { "client_id", ""}
            });

            var webhook = new MercadoPagoWebhoockModel
            {
                Id = 1,
                Action = "RECEBIMENTO VIA CARTÃO DE CRÉDITO",
                ApiVersion = "1.0",
                Data = new Data { Id = _pedido.IdPedido.ToString() },
                DateCreated = DateTime.Now,
                LiveMode = true,
                Type = "RECEBIMENTO",
                UserId = 1
            };

            var client = _apiTest.GetClient();

            //ApiKey
            client.DefaultRequestHeaders.Add("client_id", "70F3C2F7-E239-4DFB-9482-46BCDB47F7B1");

            HttpResponseMessage response = await client.PostAsJsonAsync(
                "api/Pagamento/MercadoPagoWebhoock", webhook);

            var responseContent = await response.Content.ReadAsStringAsync();
            var actualResult = JsonConvert.DeserializeObject<ActionResult<MercadoPagoWebhoock>>(responseContent);

            Assert.True(actualResult.IsValid);
        }

        [Then(@"Consultar status de pagamento do pedido")]
        public async Task ConusultarStatusPagamento()
        {
            var client = _apiTest.GetClient();
            HttpResponseMessage response = await client.GetAsync(
                $"api/Pagamento/Consultar/Pedido/{_pedido.IdPedido}");

            var responseContent = await response.Content.ReadAsStringAsync();
            var actualResult = JsonConvert.DeserializeObject<ActionResult<string>>(responseContent);
            var statusPagamento = actualResult.Model;

            Assert.Equal(enmPedidoStatusPagamento.APROVADO.ToString(), statusPagamento);
            Assert.True(actualResult.IsValid);
        }
    }
}
