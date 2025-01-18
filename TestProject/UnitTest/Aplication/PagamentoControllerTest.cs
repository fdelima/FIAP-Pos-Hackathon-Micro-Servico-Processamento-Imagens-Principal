using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Application.Controllers;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Application.UseCases.MercadoPago.Commands;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Application.UseCases.Pedido.Commands;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Entities;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Models;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Models.MercadoPago;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Validator;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using NSubstitute;
using TestProject.MockData;

namespace TestProject.UnitTest.Aplication
{
    /// <summary>
    /// Classe de teste.
    /// </summary>
    public partial class PagamentoControllerTest
    {
        private readonly IConfiguration _configuration;
        private readonly IMediator _mediator;
        private readonly IValidator<Pedido> _validator;

        /// <summary>
        /// Construtor da classe de teste.
        /// </summary>
        public PagamentoControllerTest()
        {
            _configuration = Substitute.For<IConfiguration>();
            _mediator = Substitute.For<IMediator>();
            _validator = new PedidoValidator();
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

            var aplicationController = new PagamentoController(_configuration, _mediator, _validator);

            //Mockando retorno do mediator.
            _mediator.Send(Arg.Any<ReceberPedidoCommand>())
                .Returns(Task.FromResult(ModelResultFactory.SucessResult()));

            //Act
            var result = await aplicationController.ReceberPedido(pedido);

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

            var aplicationController = new PagamentoController(_configuration, _mediator, _validator);

            //Act
            var result = await aplicationController.ReceberPedido(pedido);

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

            var aplicationController = new PagamentoController(_configuration, _mediator, _validator);

            //Mockando retorno do mediator.
            _mediator.Send(Arg.Any<ReceberPedidoCommand>())
                .Returns(Task.FromResult(ModelResultFactory.SucessResult()));

            //Act
            var result = await aplicationController.ReceberPedido(pedido);

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

            var aplicationController = new PagamentoController(_configuration, _mediator, _validator);

            //Act
            var result = await aplicationController.ReceberPedido(pedido);

            //Assert
            Assert.False(result.IsValid);
        }


        /// <summary>
        /// Testa a consulta por id
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.ConsultaPorId, true, 3)]
        public async Task ConsultarPedidoPorId(Guid idPedido)
        {
            ///Arrange
            var aplicationController = new PagamentoController(_configuration, _mediator, _validator);

            //Mockando retorno do mediator.
            _mediator.Send(Arg.Any<PedidoConsultarPagamentoCommand>())
                .Returns(Task.FromResult(ModelResultFactory.SucessResult()));

            //Act
            var result = await aplicationController.ConsultarPagamentoAsync(idPedido);

            //Assert
            Assert.True(result.IsValid);
        }

        /// <summary>
        /// Testa o recebimento do webhook do mercado pago
        /// </summary>
        [Fact]
        public async Task ReceberPagamentoViaWebhookValido()
        {
            ///Arrange            

            IHeaderDictionary headers = new HeaderDictionary(new Dictionary<String, StringValues>
            {
                { "client_id", ""}
            });
            var idPedido = Guid.NewGuid();

            var webhook = new MercadoPagoWebhoockModel
            {
                Id = 1,
                Action = "RECEBIMENTO VIA CARTÃO DE CRÉDITO",
                ApiVersion = "1.0",
                Data = new Data { Id = idPedido.ToString() },
                DateCreated = DateTime.Now,
                LiveMode = true,
                Type = "RECEBIMENTO",
                UserId = 1
            };

            var aplicationController = new PagamentoController(_configuration, _mediator, _validator);

            //Mockando retorno do mediator.
            _mediator.Send(Arg.Any<MercadoPagoWebhoockCommand>())
                .Returns(Task.FromResult(ModelResultFactory.SucessResult()));

            //Act
            var result = await aplicationController.MercadoPagoWebhoock((MercadoPagoWebhoock)webhook, idPedido, headers);

            //Assert
            Assert.True(result.IsValid);
        }

        /// <summary>
        /// Testa o recebimento do webhook do mercado pago
        /// </summary>
        [Fact]
        public async Task ReceberPagamentoViaWebhookInValido()
        {
            ///Arrange            

            IHeaderDictionary headers = new HeaderDictionary(new Dictionary<String, StringValues>
            {
                { "client_id", "xxxx"}
            });
            var idPedido = Guid.NewGuid();

            var webhook = new MercadoPagoWebhoockModel
            {
                Id = 1,
                Action = "RECEBIMENTO VIA CARTÃO DE CRÉDITO",
                ApiVersion = "1.0",
                Data = new Data { Id = idPedido.ToString() },
                DateCreated = DateTime.Now,
                LiveMode = true,
                Type = "RECEBIMENTO",
                UserId = 1
            };

            var aplicationController = new PagamentoController(_configuration, _mediator, _validator);

            //Mockando retorno do mediator.
            _mediator.Send(Arg.Any<MercadoPagoWebhoockCommand>())
                .Returns(Task.FromResult(ModelResultFactory.Error("Consumidor não autorizado e/ou inválido!")));

            //Act
            var result = await aplicationController.MercadoPagoWebhoock((MercadoPagoWebhoock)webhook, idPedido, headers);

            //Assert
            Assert.False(result.IsValid);
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
