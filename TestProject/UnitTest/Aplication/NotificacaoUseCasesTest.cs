using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Application.UseCases.Notificacao.Commands;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Application.UseCases.Notificacao.Handlers;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Entities;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Extensions;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Interfaces;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Models;
using NSubstitute;
using System.Linq.Expressions;
using TestProject.MockData;

namespace TestProject.UnitTest.Aplication
{
    public partial class NotificacaoUseCasesTest
    {
        private readonly IService<Notificacao> _service;

        /// <summary>
        /// Construtor da classe de teste.
        /// </summary>
        public NotificacaoUseCasesTest()
        {
            _service = Substitute.For<IService<Notificacao>>();
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
            var command = new NotificacaoPostCommand(notificacao);

            //Mockando retorno do serviço de domínio.
            _service.InsertAsync(notificacao)
                .Returns(Task.FromResult(ModelResultFactory.SucessResult(notificacao)));

            //Act
            var handler = new NotificacaoPostHandler(_service);
            var result = await handler.Handle(command, CancellationToken.None);

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
            var command = new NotificacaoPostCommand(notificacao);

            //Mockando retorno do serviço de domínio.
            _service.InsertAsync(notificacao)
                .Returns(Task.FromResult(ModelResultFactory.NotFoundResult<Notificacao>()));

            //Act
            var handler = new NotificacaoPostHandler(_service);
            var result = await handler.Handle(command, CancellationToken.None);

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
            notificacao.Usuario = usuario;

            var command = new NotificacaoPutCommand(idNotificacao, notificacao);

            //Mockando retorno do serviço de domínio.
            _service.UpdateAsync(notificacao)
                .Returns(Task.FromResult(ModelResultFactory.SucessResult()));

            //Act
            var handler = new NotificacaoPutHandler(_service);
            var result = await handler.Handle(command, CancellationToken.None);

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

            var command = new NotificacaoPutCommand(idNotificacao, notificacao);

            //Mockando retorno do serviço de domínio.
            _service.UpdateAsync(notificacao)
                .Returns(Task.FromResult(ModelResultFactory.NotFoundResult<Notificacao>()));

            //Act
            var handler = new NotificacaoPutHandler(_service);
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.False(result.IsValid);
        }

        /// <summary>
        /// Testa a consulta por id
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Alteracao, true, 3)]
        public async Task DeletarNotificacao(Guid idNotificacao, string usuario, string mensagem)
        {
            ///Arrange
            var notificacao = new Notificacao
            {
                IdNotificacao = idNotificacao,
                Usuario = usuario,
                Mensagem = mensagem
            };

            var command = new NotificacaoDeleteCommand(idNotificacao);

            //Mockando retorno do serviço de domínio.
            _service.FindByIdAsync(idNotificacao)
                .Returns(Task.FromResult(ModelResultFactory.SucessResult(notificacao)));

            _service.DeleteAsync(idNotificacao)
                .Returns(Task.FromResult(ModelResultFactory.SucessResult(notificacao)));

            //Act
            var handler = new NotificacaoDeleteHandler(_service);
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.True(result.IsValid);
        }

        /// <summary>
        /// Testa a consulta por id
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Alteracao, true, 3)]
        public async Task ConsultarNotificacaoPorId(Guid idNotificacao, string usuario, string mensagem)
        {
            ///Arrange
            var notificacao = new Notificacao
            {
                IdNotificacao = idNotificacao,
                Usuario = usuario,
                Mensagem = mensagem
            };

            var command = new NotificacaoFindByIdCommand(idNotificacao);

            //Mockando retorno do serviço de domínio.
            _service.FindByIdAsync(idNotificacao)
                .Returns(Task.FromResult(ModelResultFactory.SucessResult(notificacao)));

            //Act
            var handler = new NotificacaoFindByIdHandler(_service);
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.True(result.IsValid);
        }

        /// <summary>
        /// Testa a consulta com condição de pesquisa
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Consulta, true, 3)]
        public async Task ConsultarNotificacaoComCondicao(IPagingQueryParam filter, Expression<Func<Notificacao, object>> sortProp, IEnumerable<Notificacao> notificacoes)
        {
            //Arrange
            var param = new PagingQueryParam<Notificacao>() { CurrentPage = 1, Take = 10 };
            var command = new NotificacaoGetItemsCommand(filter, param.ConsultRule(), sortProp);

            //Mockando retorno do serviço de domínio.
            _service.GetItemsAsync(Arg.Any<PagingQueryParam<Notificacao>>(),
                Arg.Any<Expression<Func<Notificacao, bool>>>(),
                Arg.Any<Expression<Func<Notificacao, object>>>())
                .Returns(new ValueTask<PagingQueryResult<Notificacao>>(new PagingQueryResult<Notificacao>(new List<Notificacao>(notificacoes))));

            //Act
            var handler = new NotificacaoGetItemsHandler(_service);
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.True(result.Content.Any());
        }


        /// <summary>
        /// Testa a consulta sem condição de pesquisa
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Consulta, true, 3)]
        public async Task ConsultarNotificacaoSemCondicao(IPagingQueryParam filter, Expression<Func<Notificacao, object>> sortProp, IEnumerable<Notificacao> notificacoes)
        {
            //Arrange
            var command = new NotificacaoGetItemsCommand(filter, sortProp);

            //Mockando retorno do serviço de domínio.
            _service.GetItemsAsync(filter, sortProp)
                .Returns(new ValueTask<PagingQueryResult<Notificacao>>(new PagingQueryResult<Notificacao>(new List<Notificacao>(notificacoes))));

            //Act
            var handler = new NotificacaoGetItemsHandler(_service);
            var result = await handler.Handle(command, CancellationToken.None);

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
                default:
                    return null;
            }
        }


        #endregion
    }
}
