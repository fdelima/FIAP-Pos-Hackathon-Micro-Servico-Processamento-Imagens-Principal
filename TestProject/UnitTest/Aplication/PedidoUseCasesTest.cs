using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Application.UseCases.MercadoPago.Commands;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Application.UseCases.MercadoPago.Handlers;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Application.UseCases.Pedido.Commands;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Application.UseCases.Pedido.Handlers;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Entities;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Interfaces;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Models;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Models.MercadoPago;
using NSubstitute;
using TestProject.MockData;

namespace TestProject.UnitTest.Aplication
{
    /// <summary>
    /// Classe de teste.
    /// </summary>
    public partial class PedidoUseCasesTest
    {
        private readonly IPedidoService _service;
        private const string microServicoPedidoBaseAdress = "http://localhost:8080/";

        /// <summary>
        /// Construtor da classe de teste.
        /// </summary>
        public PedidoUseCasesTest()
        {
            _service = Substitute.For<IPedidoService>();
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

            var command = new ReceberPedidoCommand(pedido);

            //Mockando retorno do serviço de domínio.
            _service.ReceberPedido(pedido)
                .Returns(Task.FromResult(ModelResultFactory.SucessResult(pedido)));

            //Act
            var handler = new ReceberPedidoHandler(_service);
            var result = await handler.Handle(command, CancellationToken.None);

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
                IdPedido = idPedido,
                IdDispositivo = idDispositivo,
                IdCliente = idCliente,
                Data = data,
                Status = status,
                DataStatusPedido = dataStatusPedido,
                StatusPagamento = statusPagamento,
                DataStatusPagamento = dataStatusPagamento

            };

            var command = new ReceberPedidoCommand(pedido);

            //Mockando retorno do serviço de domínio.
            _service.ReceberPedido(pedido)
                .Returns(Task.FromResult(ModelResultFactory.DuplicatedResult<Pedido>()));

            //Act
            var handler = new ReceberPedidoHandler(_service);
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.False(result.IsValid);

        }

        /// <summary>
        /// Testa a consulta por id
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Alteracao, true, 3)]
        public async Task ConsultarPedidoPorId(Guid idPedido, Guid idDispositivo, Guid idCliente,
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

            var command = new PedidoConsultarPagamentoCommand(idPedido);

            //Mockando retorno do serviço de domínio.
            _service.ConsultarPagamentoAsync(idPedido)
                .Returns(Task.FromResult(ModelResultFactory.SucessResult(pedido)));

            //Act
            var handler = new PedidoConsultarPagamentoHandler(_service);
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.True(result.IsValid);
        }

        /// <summary>
        /// Testa recebimento do webhook de pagamento
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


            var command = new MercadoPagoWebhoockCommand((MercadoPagoWebhoock)notificacao, idPedido, microServicoPedidoBaseAdress);

            //Mockando retorno do serviço de domínio.
            _service.MercadoPagoWebhoock(Arg.Any<MercadoPagoWebhoock>(), Arg.Any<Guid>())
                .Returns(Task.FromResult(ModelResultFactory.InsertSucessResult<MercadoPagoWebhoock>((MercadoPagoWebhoock)notificacao)));

            //Act
            var handler = new MercadoPagoWebhoockHandler(_service);
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.True(result.IsValid);
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
                case enmTipo.ConsultaPorId:
                    if (dadosValidos)
                        return PedidoMock.ObterDadosConsultaPorIdValidos(quantidade);
                    else
                        return PedidoMock.ObterDadosConsultaPorIdInvalidos(quantidade);
                default:
                    return null;
            }
        }

        #endregion

    }
}
