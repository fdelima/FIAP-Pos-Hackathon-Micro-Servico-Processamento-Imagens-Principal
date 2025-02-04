using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Application.Controllers;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Application.UseCases.Notificacao.Commands;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Entities;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Extensions;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Interfaces;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Models;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Validator;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using System.Linq.Expressions;
using TestProject.MockData;

namespace TestProject.UnitTest.Aplication
{
    public class NotificacaoControllerTest
    {
        private readonly IConfiguration _configuration;
        private readonly IMediator _mediator;
        private readonly IValidator<Notificacao> _validator;

        /// <summary>
        /// Construtor da classe de teste.
        /// </summary>
        public NotificacaoControllerTest()
        {
            _configuration = Substitute.For<IConfiguration>();
            _mediator = Substitute.For<IMediator>();
            _validator = new NotificacaoValidator();
        }

        /// <summary>
        /// Testa a inserção com dados válidos
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Inclusao, true, 3)]
        public async Task InserirComDadosValidos(string usuario, string mensagem)
        {
            ///Arrange
            var notificacao = new Notificacao
            {
                Usuario = usuario,
                Mensagem = mensagem
            };

            notificacao.Usuario = usuario;
            var aplicationController = new NotificacaoController(_mediator, _validator);

            //Mockando retorno do mediator.
            _mediator.Send(Arg.Any<NotificacaoPostCommand>())
                .Returns(Task.FromResult(ModelResultFactory.SucessResult()));

            //Act
            var result = await aplicationController.PostAsync(notificacao);

            //Assert
            Assert.True(result.IsValid);
        }

        /// <summary>
        /// Testa a inserção com dados inválidos
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Inclusao, false, 3)]
        public async Task InserirComDadosInvalidos(string usuario, string mensagem)
        {
            ///Arrange
            var notificacao = new Notificacao
            {
                Usuario = usuario,
                Mensagem = mensagem
            };
            var aplicationController = new NotificacaoController(_mediator, _validator);

            //Act
            var result = await aplicationController.PostAsync(notificacao);

            //Assert
            Assert.False(result.IsValid);

        }

        /// <summary>
        /// Testa a alteração com dados válidos
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Alteracao, true, 3)]
        public async Task AlterarComDadosValidos(Guid idNotificacao, string usuario, string mensagem)
        {
            ///Arrange
            var notificacao = new Notificacao
            {
                IdNotificacao = idNotificacao,
                Usuario = usuario,
                Mensagem = mensagem
            };
            var aplicationController = new NotificacaoController(_mediator, _validator);

            //Mockando retorno do mediator.
            _mediator.Send(Arg.Any<NotificacaoPutCommand>())
                .Returns(Task.FromResult(ModelResultFactory.SucessResult()));

            //Act
            var result = await aplicationController.PutAsync(idNotificacao, notificacao);

            //Assert
            Assert.True(result.IsValid);
        }

        /// <summary>
        /// Testa a alteração com dados inválidos
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Alteracao, false, 3)]
        public async Task AlterarComDadosInvalidos(Guid idNotificacao, string usuario, string mensagem)
        {
            ///Arrange
            var notificacao = new Notificacao
            {
                IdNotificacao = idNotificacao,
                Usuario = usuario,
                Mensagem = mensagem
            };
            var aplicationController = new NotificacaoController(_mediator, _validator);

            //Act
            var result = await aplicationController.PutAsync(idNotificacao, notificacao);

            //Assert
            Assert.False(result.IsValid);
        }

        /// <summary>
        /// Testa a deletar
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.ConsultaPorId, true, 3)]
        public async Task DeletarNotificacao(Guid idNotificacao)
        {
            ///Arrange
            var aplicationController = new NotificacaoController(_mediator, _validator);

            //Mockando retorno do mediator.
            _mediator.Send(Arg.Any<NotificacaoDeleteCommand>())
                .Returns(Task.FromResult(ModelResultFactory.SucessResult()));

            //Act
            var result = await aplicationController.DeleteAsync(idNotificacao);

            //Assert
            Assert.True(result.IsValid);
        }

        /// <summary>
        /// Testa a consulta por id
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.ConsultaPorId, true, 3)]
        public async Task ConsultarNotificacaoPorId(Guid idNotificacao)
        {
            ///Arrange
            var aplicationController = new NotificacaoController(_mediator, _validator);

            //Mockando retorno do mediator.
            _mediator.Send(Arg.Any<NotificacaoFindByIdCommand>())
                .Returns(Task.FromResult(ModelResultFactory.SucessResult()));

            //Act
            var result = await aplicationController.FindByIdAsync(idNotificacao);

            //Assert
            Assert.True(result.IsValid);
        }

        /// <summary>
        /// Testa a consulta com condição de pesquisa
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Consulta, true, 3)]
        public async Task ConsultarNotificacaoComCondicao(IPagingQueryParam filter, Expression<Func<Notificacao, object>> sortProp, IEnumerable<Notificacao> notificacaos)
        {
            ///Arrange
            var aplicationController = new NotificacaoController(_mediator, _validator);
            var param = new PagingQueryParam<Notificacao>() { CurrentPage = 1, Take = 10 };

            //Mockando retorno do mediator.
            _mediator.Send(Arg.Any<NotificacaoGetItemsCommand>())
                .Returns(Task.FromResult(new PagingQueryResult<Notificacao>(new List<Notificacao>(notificacaos), 1, 1)));

            //Act
            var result = await aplicationController.ConsultItemsAsync(filter, param.ConsultRule(), sortProp);

            //Assert
            Assert.True(result.Content.Any());
        }

        /// <summary>
        /// Testa a consulta sem condição de pesquisa
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Consulta, true, 3)]
        public async Task ConsultarNotificacaoSemCondicao(IPagingQueryParam filter, Expression<Func<Notificacao, object>> sortProp, IEnumerable<Notificacao> notificacaos)
        {
            ///Arrange
            var aplicationController = new NotificacaoController(_mediator, _validator);
            var param = new PagingQueryParam<Notificacao>() { CurrentPage = 1, Take = 10 };

            //Mockando retorno do mediator.
            _mediator.Send(Arg.Any<NotificacaoGetItemsCommand>())
                .Returns(Task.FromResult(new PagingQueryResult<Notificacao>(new List<Notificacao>(notificacaos), 1, 1)));

            //Act
            var result = await aplicationController.GetItemsAsync(filter, sortProp);

            //Assert
            Assert.True(result.Content.Any());
        }

        /// <summary>
        /// Testa a consulta com condição de pesquisa Inválida
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Consulta, false, 10)]
        public async Task ConsultarNotificacaoComCondicaoInvalidos(IPagingQueryParam filter, Expression<Func<Notificacao, object>> sortProp, IEnumerable<Notificacao> notificacaos)
        {
            ///Arrange

            filter = null;
            var param = new PagingQueryParam<Notificacao>() { CurrentPage = 1, Take = 10 };
            var aplicationController = new NotificacaoController(_mediator, _validator);

            //Act
            try
            {
                var result = await aplicationController.ConsultItemsAsync(filter, param.ConsultRule(), sortProp);
            }
            catch (Exception ex)
            {
                //Assert
                Assert.True(ex.GetType().Equals(typeof(InvalidOperationException)));
            }
        }

        /// <summary>
        /// Testa a consulta sem condição de pesquisa Inválida
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Consulta, false, 10)]
        public async Task ConsultarNotificacaoSemCondicaoInvalidos(IPagingQueryParam filter, Expression<Func<Notificacao, object>> sortProp, IEnumerable<Notificacao> notificacaos)
        {
            ///Arrange

            filter = null;
            var aplicationController = new NotificacaoController(_mediator, _validator);

            //Act
            try
            {
                var result = await aplicationController.GetItemsAsync(filter, sortProp);
            }
            catch (Exception ex)
            {
                //Assert
                Assert.True(ex.GetType().Equals(typeof(InvalidOperationException)));
            }
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
                    if (dadosValidos)
                        return NotificacaoMock.ObterDadosValidos(quantidade);
                    else
                        return NotificacaoMock.ObterDadosInvalidos(quantidade);
                case enmTipo.Alteracao:
                    if (dadosValidos)
                        return NotificacaoMock.ObterDadosValidos(quantidade)
                            .Select(i => new object[] { Guid.NewGuid() }.Concat(i).ToArray());
                    else
                        return NotificacaoMock.ObterDadosInvalidos(quantidade)
                            .Select(i => new object[] { Guid.NewGuid() }.Concat(i).ToArray());
                case enmTipo.Consulta:
                    if (dadosValidos)
                        return NotificacaoMock.ObterDadosConsultaValidos(quantidade);
                    else
                        return NotificacaoMock.ObterDadosConsultaInValidos(quantidade);
                case enmTipo.ConsultaPorId:
                    if (dadosValidos)
                        return NotificacaoMock.ObterDadosConsultaPorIdValidos(quantidade);
                    else
                        return NotificacaoMock.ObterDadosConsultaPorIdInvalidos(quantidade);
                default:
                    return null;
            }
        }

        #endregion

    }
}
