using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Entities;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Extensions;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Interfaces;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Models;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Models.MercadoPago;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Services;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Validator;
using FluentValidation;
using NSubstitute;
using System.Linq.Expressions;
using TestProject.MockData;

namespace TestProject.UnitTest.Domain
{
    /// <summary>
    /// Classe de teste.
    /// </summary>
    public partial class PedidoServiceTest
    {
        private readonly IGateways<Pedido> _gatewayPedidoMock;
        private readonly IValidator<Pedido> _validator;
        private readonly IGateways<Notificacao> _notificacaoGatewayMock;
        protected readonly IGateways<MercadoPagoWebhoock> _mercadoPagoWebhoockGatewayMock;

        /// <summary>
        /// Construtor da classe de teste.
        /// </summary>
        public PedidoServiceTest()
        {
            _validator = new PedidoValidator();
            _gatewayPedidoMock = Substitute.For<IGateways<Pedido>>();
            _notificacaoGatewayMock = Substitute.For<IGateways<Notificacao>>();
            _mercadoPagoWebhoockGatewayMock = Substitute.For<IGateways<MercadoPagoWebhoock>>();
        }

        /// <summary>
        /// Testa a inserção com dados válidos
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Inclusao, true, 3)]
        public async Task InserirComDadosValidos(Guid idPedido, Guid idDispositivo, Guid idCliente,
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

            var domainService = new PedidoService(_gatewayPedidoMock, _validator, _notificacaoGatewayMock, _mercadoPagoWebhoockGatewayMock);

            //Act
            var result = await domainService.InsertAsync(pedido);

            //Assert
            Assert.True(result.IsValid);
        }

        /// <summary>
        /// Testa a inserção com dados válidos
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Inclusao, true, 3)]
        public async Task ReceberPedido(Guid idPedido, Guid idDispositivo, Guid idCliente,
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

            var domainService = new PedidoService(_gatewayPedidoMock, _validator, _notificacaoGatewayMock, _mercadoPagoWebhoockGatewayMock);

            //Act
            var result = await domainService.ReceberPedido(pedido);

            //Assert
            Assert.True(result.IsValid);
        }

        /// <summary>
        /// Testa a inserção com dados inválidos
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Inclusao, false, 3)]
        public async Task InserirComDadosInvalidos(Guid idPedido, Guid idDispositivo, Guid idCliente,
            DateTime data, string status, DateTime dataStatusPedido,
            string statusPagamento, DateTime dataStatusPagamento)
        {
            ///Arrange            
            var pedido = new Pedido
            {
                IdDispositivo = idDispositivo,
                IdCliente = idCliente,
                Data = data,
                Status = status,
                DataStatusPedido = dataStatusPedido,
                StatusPagamento = statusPagamento,
                DataStatusPagamento = dataStatusPagamento

            };

            var domainService = new PedidoService(_gatewayPedidoMock, _validator, _notificacaoGatewayMock, _mercadoPagoWebhoockGatewayMock);

            //Act
            var result = await domainService.InsertAsync(pedido);

            //Assert
            Assert.False(result.IsValid);

        }

        /// <summary>
        /// Testa a alteração com dados válidos
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Alteracao, true, 3)]
        public async Task AlterarComDadosValidos(Guid idPedido, Guid idDispositivo, Guid idCliente,
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

            var domainService = new PedidoService(_gatewayPedidoMock, _validator, _notificacaoGatewayMock, _mercadoPagoWebhoockGatewayMock);

            //Mockando retorno do metodo interno do UpdateAsync
            _gatewayPedidoMock.FindByIdAsync(idPedido)
                .Returns(new ValueTask<Pedido>(pedido));

            //Mockando retorno do metodo interno do UpdateAsync
            _gatewayPedidoMock.UpdateAsync(Arg.Any<Pedido>())
                .Returns(Task.FromResult(pedido));

            //Act
            var result = await domainService.UpdateAsync(pedido);

            //Assert
            Assert.True(result.IsValid);
        }

        /// <summary>
        /// Testa a alteração com dados inválidos
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Alteracao, false, 3)]
        public async Task AlterarComDadosInvalidos(Guid idPedido, Guid idDispositivo, Guid idCliente,
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

            var domainService = new PedidoService(_gatewayPedidoMock, _validator, _notificacaoGatewayMock, _mercadoPagoWebhoockGatewayMock);

            //Mockando retorno do metodo interno do UpdateAsync
            _gatewayPedidoMock.FindByIdAsync(idPedido)
                .Returns(new ValueTask<Pedido>(pedido));

            //Act
            var result = await domainService.UpdateAsync(pedido);

            //Assert
            Assert.False(result.IsValid);
        }


        /// <summary>
        /// Testa a consulta por id
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Alteracao, true, 3)]
        public async Task DeletarPedido(Guid idPedido, Guid idDispositivo, Guid idCliente,
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

            var domainService = new PedidoService(_gatewayPedidoMock, _validator, _notificacaoGatewayMock, _mercadoPagoWebhoockGatewayMock);

            //Mockando retorno do metodo interno do FindByIdAsync
            _gatewayPedidoMock.FindByIdAsync(idPedido)
                .Returns(new ValueTask<Pedido>(pedido));

            _gatewayPedidoMock.DeleteAsync(idPedido)
                .Returns(Task.FromResult(ModelResultFactory.SucessResult()));

            //Act
            var result = await domainService.DeleteAsync(idPedido);

            //Assert
            Assert.True(result.IsValid);
        }

        /// <summary>
        /// Testa a consulta por id
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Alteracao, true, 3)]
        public async Task ConsultarPedidoPorIdComDadosValidos(Guid idPedido, Guid idDispositivo, Guid idCliente,
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

            var domainService = new PedidoService(_gatewayPedidoMock, _validator, _notificacaoGatewayMock, _mercadoPagoWebhoockGatewayMock);

            //Mockando retorno do metodo interno do FindByIdAsync
            _gatewayPedidoMock.FindByIdAsync(idPedido)
                .Returns(new ValueTask<Pedido>(pedido));

            //Act
            var result = await domainService.FindByIdAsync(idPedido);

            //Assert
            Assert.True(result.IsValid);
        }


        /// <summary>
        /// Testa a consulta de pagamento do pedido
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Alteracao, true, 3)]
        public async Task ConsultarPagamento(Guid idPedido, Guid idDispositivo, Guid idCliente,
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

            var domainService = new PedidoService(_gatewayPedidoMock, _validator, _notificacaoGatewayMock, _mercadoPagoWebhoockGatewayMock);

            //Mockando retorno do metodo interno do FindByIdAsync
            _gatewayPedidoMock.FindByIdAsync(idPedido)
                .Returns(new ValueTask<Pedido>(pedido));

            //Act
            var result = await domainService.ConsultarPagamentoAsync(idPedido);

            //Assert
            Assert.True(result.IsValid);
        }


        /// <summary>
        /// Testa a consulta de pagamento do pedido
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Alteracao, true, 3)]
        public async Task MercadoPagoWebhook(Guid idPedido, Guid idDispositivo, Guid idCliente,
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

            var notificacao = new MercadoPagoWebhoockModel
            {
                Id = 1,
                Action = "RECEBIMENTO VIA CARTÃO DE CRÉDITO",
                ApiVersion = "1.0",
                Data = new Data { Id = pedido.IdPedido.ToString() },
                DateCreated = DateTime.Now,
                LiveMode = true,
                Type = "RECEBIMENTO",
                UserId = 1
            };

            var domainService = new PedidoService(_gatewayPedidoMock, _validator, _notificacaoGatewayMock, _mercadoPagoWebhoockGatewayMock);

            //Mockando retorno do metodo interno do FindByIdAsync
            _gatewayPedidoMock.FindByIdAsync(idPedido)
                .Returns(new ValueTask<Pedido>(pedido));

            //Act
            var result = await domainService.MercadoPagoWebhoock((MercadoPagoWebhoock)notificacao, idPedido);

            //Assert
            Assert.True(result.IsValid);
        }

        /// <summary>
        /// Testa a consulta por id
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Alteracao, true, 3)]
        public async Task ConsultarPedidoPorIdComDadosInvalidos(Guid idPedido, Guid idDispositivo, Guid idCliente,
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

            var domainService = new PedidoService(_gatewayPedidoMock, _validator, _notificacaoGatewayMock, _mercadoPagoWebhoockGatewayMock);

            //Act
            var result = await domainService.FindByIdAsync(idPedido);

            //Assert
            Assert.False(result.IsValid);
        }

        /// <summary>
        /// Testa a consulta Valida
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Consulta, true, 3)]
        public async Task ConsultarPedido(IPagingQueryParam filter, Expression<Func<Pedido, object>> sortProp, IEnumerable<Pedido> Pedidos)
        {
            ///Arrange
            var domainService = new PedidoService(_gatewayPedidoMock, _validator, _notificacaoGatewayMock, _mercadoPagoWebhoockGatewayMock);

            //Mockando retorno do metodo interno do GetItemsAsync
            _gatewayPedidoMock.GetItemsAsync(Arg.Any<PagingQueryParam<Pedido>>(),
                Arg.Any<Expression<Func<Pedido, object>>>())
                .Returns(new ValueTask<PagingQueryResult<Pedido>>(new PagingQueryResult<Pedido>(new List<Pedido>(Pedidos))));


            //Act
            var result = await domainService.GetItemsAsync(filter, sortProp);

            //Assert
            Assert.True(result.Content.Any());
        }

        /// <summary>
        /// Testa a consulta com condição de pesquisa
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Consulta, true, 3)]
        public async Task ConsultarPedidoComCondicao(IPagingQueryParam filter, Expression<Func<Pedido, object>> sortProp, IEnumerable<Pedido> Pedidos)
        {
            ///Arrange
            var param = new PagingQueryParam<Pedido>() { CurrentPage = 1, Take = 10 };

            //Mockando retorno do metodo interno do GetItemsAsync
            _gatewayPedidoMock.GetItemsAsync(Arg.Any<PagingQueryParam<Pedido>>(),
                Arg.Any<Expression<Func<Pedido, bool>>>(),
                Arg.Any<Expression<Func<Pedido, object>>>())
                .Returns(new ValueTask<PagingQueryResult<Pedido>>(new PagingQueryResult<Pedido>(new List<Pedido>(Pedidos))));

            //Act
            var result = await _gatewayPedidoMock.GetItemsAsync(filter, param.ConsultRule(), sortProp);

            //Assert
            Assert.True(result.Content.Any());
        }

        /// <summary>
        /// Testa a consulta sem condição de pesquisa
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Consulta, true, 3)]
        public async Task ConsultarPedidoSemCondicao(IPagingQueryParam filter, Expression<Func<Pedido, object>> sortProp, IEnumerable<Pedido> Pedidos)
        {
            ///Arrange

            //Mockando retorno do metodo interno do GetItemsAsync
            _gatewayPedidoMock.GetItemsAsync(filter, sortProp)
                .Returns(new ValueTask<PagingQueryResult<Pedido>>(new PagingQueryResult<Pedido>(new List<Pedido>(Pedidos))));

            //Act
            var result = await _gatewayPedidoMock.GetItemsAsync(filter, sortProp);

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
