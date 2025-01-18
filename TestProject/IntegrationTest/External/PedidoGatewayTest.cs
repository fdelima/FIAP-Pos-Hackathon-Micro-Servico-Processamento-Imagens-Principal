using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Entities;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Extensions;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Interfaces;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.ValuesObject;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Infra.Gateways;
using System.Linq.Expressions;
using TestProject.Infra;
using TestProject.MockData;

namespace TestProject.IntegrationTest.External
{
    /// <summary>
    /// Classe de teste.
    /// </summary>
    public partial class PedidoGatewayTest : IClassFixture<IntegrationTestsBase>
    {
        internal readonly MongoTestFixture _mongoTestFixture;

        /// <summary>
        /// Construtor da classe de teste.
        /// </summary>
        public PedidoGatewayTest(IntegrationTestsBase data)
        {
            _mongoTestFixture = data._mongoTestFixture;
        }

        /// <summary>
        /// Testa a inserção com dados válidos
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Inclusao, true, 3)]
        public async void InserirComDadosValidos(Guid idPedido, Guid idDispositivo, Guid idCliente,
            DateTime data, string status, DateTime dataStatusPedido,
            string statusPagamento, DateTime dataStatusPagamento)
        {
            ///Arrange     
            var pedido = new Pedido
            {
                IdPedido = idPedido,
                IdDispositivo = idDispositivo,
                IdCliente = idCliente,
                Data = data,
                Status = status,
                DataStatusPedido = dataStatusPedido,
                StatusPagamento = statusPagamento,
                DataStatusPagamento = dataStatusPagamento

            };

            //Act
            var _pedidoGateway = new BaseGateway<Pedido>(_mongoTestFixture.GetDbContext());
            var result = await _pedidoGateway.InsertAsync(pedido);

            //Assert
            try
            {
                await _pedidoGateway.CommitAsync();
                Assert.True(true);
            }
            catch (InvalidOperationException)
            {
                Assert.True(false);
            }
        }

        /// <summary>
        /// Testa a alteração com dados válidos
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Alteracao, true, 3)]
        public async void AlterarComDadosValidos(Guid idPedido, Guid idDispositivo, Guid idCliente,
            DateTime data, string status, DateTime dataStatusPedido,
            string statusPagamento, DateTime dataStatusPagamento)
        {
            ///Arrange            
            var pedido = new Pedido
            {
                IdPedido = idPedido,
                IdDispositivo = idDispositivo,
                IdCliente = idCliente,
                Data = data,
                Status = status,
                DataStatusPedido = dataStatusPedido,
                StatusPagamento = statusPagamento,
                DataStatusPagamento = dataStatusPagamento

            };

            var _pedidoGateway = new BaseGateway<Pedido>(_mongoTestFixture.GetDbContext());
            var result = await _pedidoGateway.InsertAsync(pedido);
            await _pedidoGateway.CommitAsync();

            //Alterando
            pedido.StatusPagamento = enmPedidoStatusPagamento.PROCESSANDO.ToString();

            var dbEntity = await _pedidoGateway.FindByIdAsync(idPedido);

            //Act
            await _pedidoGateway.UpdateAsync(dbEntity, pedido);
            await _pedidoGateway.UpdateAsync(pedido);

            try
            {
                await _pedidoGateway.CommitAsync();
                Assert.True(true);
            }
            catch (InvalidOperationException)
            {
                Assert.True(false);
            }
            catch (Exception)
            {
                Assert.True(false);
            }
        }

        /// <summary>
        /// Testa a deletar por id
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Alteracao, true, 1)]
        public async void DeletarPedido(Guid idPedido, Guid idDispositivo, Guid idCliente,
            DateTime data, string status, DateTime dataStatusPedido,
            string statusPagamento, DateTime dataStatusPagamento)
        {
            ///Arrange            
            var pedido = new Pedido
            {
                IdPedido = idPedido,
                IdDispositivo = idDispositivo,
                IdCliente = idCliente,
                Data = data,
                Status = status,
                DataStatusPedido = dataStatusPedido,
                StatusPagamento = statusPagamento,
                DataStatusPagamento = dataStatusPagamento

            };


            var _pedidoGateway = new BaseGateway<Pedido>(_mongoTestFixture.GetDbContext());
            await _pedidoGateway.InsertAsync(pedido);
            await _pedidoGateway.CommitAsync();

            //Act
            await _pedidoGateway.DeleteAsync(idPedido);

            //Assert
            try
            {
                await _pedidoGateway.CommitAsync();
                Assert.True(true);
            }
            catch (InvalidOperationException)
            {
                Assert.True(false);
            }
            catch (Exception)
            {
                Assert.True(false);
            }
        }

        /// <summary>
        /// Testa a consulta por id
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Alteracao, true, 1)]
        public async void ConsultarPedidoPorId(Guid idPedido, Guid idDispositivo, Guid idCliente,
            DateTime data, string status, DateTime dataStatusPedido,
            string statusPagamento, DateTime dataStatusPagamento)
        {
            ///Arrange            
            var pedido = new Pedido
            {
                IdPedido = idPedido,
                IdDispositivo = idDispositivo,
                IdCliente = idCliente,
                Data = data,
                Status = status,
                DataStatusPedido = dataStatusPedido,
                StatusPagamento = statusPagamento,
                DataStatusPagamento = dataStatusPagamento

            };

            var _pedidoGateway = new BaseGateway<Pedido>(_mongoTestFixture.GetDbContext());
            await _pedidoGateway.InsertAsync(pedido);
            await _pedidoGateway.CommitAsync();

            //Act
            var result = await _pedidoGateway.FindByIdAsync(idPedido);

            //Assert
            Assert.True(result != null);
        }

        /// <summary>
        /// Testa a consulta Valida
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Consulta, true, 3)]
        public async Task ConsultarPedido(IPagingQueryParam filter, Expression<Func<Pedido, object>> sortProp, IEnumerable<Pedido> pedidos)
        {
            ///Arrange
            var _pedidoGateway = new BaseGateway<Pedido>(_mongoTestFixture.GetDbContext());

            foreach (var pedido in pedidos)
            {
                await _pedidoGateway.InsertAsync(pedido);
                await _pedidoGateway.CommitAsync();
            }

            //Act
            var result = await _pedidoGateway.GetItemsAsync(filter, sortProp);

            //Assert
            Assert.True(result.Content.Any());
        }

        /// <summary>
        /// Testa a consulta com condição de pesquisa
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Consulta, true, 3)]
        public async Task ConsultarPedidoComCondicao(IPagingQueryParam filter, Expression<Func<Pedido, object>> sortProp, IEnumerable<Pedido> pedidos)
        {
            ///Arrange
            var _pedidoGateway = new BaseGateway<Pedido>(_mongoTestFixture.GetDbContext());
            foreach (var pedido in pedidos)
                pedido.StatusPagamento = enmPedidoStatusPagamento.APROVADO.ToString();

            foreach (var pedido in pedidos)
            {
                await _pedidoGateway.InsertAsync(pedido);
                await _pedidoGateway.CommitAsync();
            }

            var param = new PagingQueryParam<Pedido>() { CurrentPage = 1, Take = 10, ObjFilter = pedidos.ElementAt(0) };

            //Act
            var result = await _pedidoGateway.GetItemsAsync(filter, param.ConsultRule(), sortProp);

            //Assert
            Assert.True(result.Content.Any());
        }

        /// <summary>
        /// Testa a consulta sem condição de pesquisa
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Consulta, true, 3)]
        public async Task ConsultarPedidoSemCondicao(IPagingQueryParam filter, Expression<Func<Pedido, object>> sortProp, IEnumerable<Pedido> pedidos)
        {
            ///Arrange
            var _pedidoGateway = new BaseGateway<Pedido>(_mongoTestFixture.GetDbContext());
            foreach (var pedido in pedidos)
                pedido.StatusPagamento = enmPedidoStatusPagamento.APROVADO.ToString();

            foreach (var pedido in pedidos)
            {
                await _pedidoGateway.InsertAsync(pedido);
                await _pedidoGateway.CommitAsync();
            }

            //Act
            var result = await _pedidoGateway.GetItemsAsync(filter, sortProp);

            //Assert
            Assert.True(result.Content.Any());
        }


        /// <summary>
        /// Testa metodo base de lista pedidos
        /// </summary>
        [Fact]
        public async Task Listar()
        {
            ///Arrange
            var _pedidoGateway = new BaseGateway<Pedido>(_mongoTestFixture.GetDbContext());

            //Act
            var result = await _pedidoGateway.GetItemsAsync();

            //Assert
            Assert.True(result.Content.Any());
        }

        #region [ Xunit MemberData ]

        /// <summary>
        /// Mock de dados
        /// </summary>
        public static IEnumerable<object[]> ObterDados(enmTipo tipo, bool dadosValidos, int quantidade)
        {
            switch (tipo)
            {
                case enmTipo.Inclusao:
                case enmTipo.Alteracao:
                    if (dadosValidos)
                        return PedidoMock.ObterDadosValidos(quantidade);
                    else
                        return PedidoMock.ObterDadosInvalidos(quantidade);
                case enmTipo.Consulta:
                    if (dadosValidos)
                        return PedidoMock.ObterDadosConsultaValidos(quantidade);
                    else
                        return PedidoMock.ObterDadosConsultaInValidos(quantidade);
                default:
                    return null;
            }
        }

        #endregion

    }
}
