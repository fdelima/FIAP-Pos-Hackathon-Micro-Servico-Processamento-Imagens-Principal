using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Application.UseCases.ProcessamentoImagem.Commands;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Application.UseCases.ProcessamentoImagem.Handlers;
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
    public partial class ProcessamentoImagemUseCasesTest
    {
        private readonly IService<ProcessamentoImagem> _service;

        /// <summary>
        /// Construtor da classe de teste.
        /// </summary>
        public ProcessamentoImagemUseCasesTest()
        {
            _service = Substitute.For<IService<ProcessamentoImagem>>();
        }

        /// <summary>
        /// Testa a inserção com dados válidos
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Inclusao, true, 3)]
        public async Task InserirComDadosValidos(DateTime data, string usuario, DateTime dataEnvio, string nomeArquivo, string nomeArquivoZipDownload, long tamanhoArquivo)
        {
            ///Arrange
            var processamentoImagem = new ProcessamentoImagem
            { 
                Data = data,
                Usuario = usuario,
                DataEnvio = dataEnvio,
                NomeArquivo = nomeArquivo,
                TamanhoArquivo = tamanhoArquivo,
                NomeArquivoZipDownload = nomeArquivoZipDownload
            };

            var command = new ProcessamentoImagemPostCommand(processamentoImagem);

            //Mockando retorno do serviço de domínio.
            _service.InsertAsync(processamentoImagem)
                .Returns(Task.FromResult(ModelResultFactory.SucessResult(processamentoImagem)));

            //Act
            var handler = new ProcessamentoImagemPostHandler(_service);
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.True(result.IsValid);
        }

        /// <summary>
        /// Testa a inserção com dados inválidos
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Inclusao, false, 3)]
        public async Task InserirComDadosInvalidos(DateTime data, string usuario, DateTime dataEnvio, string nomeArquivo, string nomeArquivoZipDownload, long tamanhoArquivo)
        {
            ///Arrange
            var processamentoImagem = new ProcessamentoImagem
            {
                Data = data,
                Usuario = usuario,
                DataEnvio = dataEnvio,
                NomeArquivo = nomeArquivo,
                TamanhoArquivo = tamanhoArquivo,
                NomeArquivoZipDownload = nomeArquivoZipDownload
            };

            var command = new ProcessamentoImagemPostCommand(processamentoImagem);

            //Mockando retorno do serviço de domínio.
            _service.InsertAsync(processamentoImagem)
                .Returns(Task.FromResult(ModelResultFactory.NotFoundResult<ProcessamentoImagem>()));

            //Act
            var handler = new ProcessamentoImagemPostHandler(_service);
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.False(result.IsValid);
        }

        /// <summary>
        /// Testa a alteração com dados válidos
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Alteracao, true, 3)]
        public async Task AlterarComDadosValidos(Guid idProcessamentoImagem, DateTime data, string usuario, DateTime dataEnvio, string nomeArquivo, string nomeArquivoZipDownload, long tamanhoArquivo)
        {
            ///Arrange
            var processamentoImagem = new ProcessamentoImagem
            {
                IdProcessamentoImagem = idProcessamentoImagem,
                Data = data,
                Usuario = usuario,
                DataEnvio = dataEnvio,
                NomeArquivo = nomeArquivo,
                TamanhoArquivo = tamanhoArquivo,
                NomeArquivoZipDownload = nomeArquivoZipDownload
            };

            var command = new ProcessamentoImagemPutCommand(idProcessamentoImagem, processamentoImagem);

            //Mockando retorno do serviço de domínio.
            _service.UpdateAsync(processamentoImagem)
                .Returns(Task.FromResult(ModelResultFactory.SucessResult()));

            //Act
            var handler = new ProcessamentoImagemPutHandler(_service);
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.True(result.IsValid);
        }

        /// <summary>
        /// Testa a alteração com dados inválidos
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Alteracao, false, 3)]
        public async Task AlterarComDadosInvalidos(Guid idProcessamentoImagem, DateTime data, string usuario, DateTime dataEnvio, string nomeArquivo, string nomeArquivoZipDownload, long tamanhoArquivo)
        {
            ///Arrange
            var processamentoImagem = new ProcessamentoImagem
            {
                IdProcessamentoImagem = idProcessamentoImagem,
                Data = data,
                Usuario = usuario,
                DataEnvio = dataEnvio,
                NomeArquivo = nomeArquivo,
                TamanhoArquivo = tamanhoArquivo,
                NomeArquivoZipDownload = nomeArquivoZipDownload
            };

            var command = new ProcessamentoImagemPutCommand(idProcessamentoImagem, processamentoImagem);

            //Mockando retorno do serviço de domínio.
            _service.UpdateAsync(processamentoImagem)
                .Returns(Task.FromResult(ModelResultFactory.NotFoundResult<ProcessamentoImagem>()));

            //Act
            var handler = new ProcessamentoImagemPutHandler(_service);
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.False(result.IsValid);
        }

        /// <summary>
        /// Testa a consulta por id
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Alteracao, true, 3)]
        public async Task DeletarProcessamentoImagem(Guid idProcessamentoImagem, DateTime data, string usuario, DateTime dataEnvio, string nomeArquivo, string nomeArquivoZipDownload, long tamanhoArquivo)
        {
            ///Arrange
            var processamentoImagem = new ProcessamentoImagem
            {
                IdProcessamentoImagem = idProcessamentoImagem,
                Data = data,
                Usuario = usuario,
                DataEnvio = dataEnvio,
                NomeArquivo = nomeArquivo,
                TamanhoArquivo = tamanhoArquivo,
                NomeArquivoZipDownload = nomeArquivoZipDownload
            };

            var command = new ProcessamentoImagemDeleteCommand(idProcessamentoImagem);

            //Mockando retorno do serviço de domínio.
            _service.FindByIdAsync(idProcessamentoImagem)
                .Returns(Task.FromResult(ModelResultFactory.SucessResult(processamentoImagem)));

            _service.DeleteAsync(idProcessamentoImagem)
                .Returns(Task.FromResult(ModelResultFactory.SucessResult(processamentoImagem)));

            //Act
            var handler = new ProcessamentoImagemDeleteHandler(_service);
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.True(result.IsValid);
        }

        /// <summary>
        /// Testa a consulta por id
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Alteracao, true, 3)]
        public async Task ConsultarProcessamentoImagemPorId(Guid idProcessamentoImagem, DateTime data, string usuario, DateTime dataEnvio, string nomeArquivo, string nomeArquivoZipDownload, long tamanhoArquivo)
        {
            ///Arrange
            var processamentoImagem = new ProcessamentoImagem
            {
                IdProcessamentoImagem = idProcessamentoImagem,
                Data = data,
                Usuario = usuario,
                DataEnvio = dataEnvio,
                NomeArquivo = nomeArquivo,
                TamanhoArquivo = tamanhoArquivo,
                NomeArquivoZipDownload = nomeArquivoZipDownload
            };

            var command = new ProcessamentoImagemFindByIdCommand(idProcessamentoImagem);

            //Mockando retorno do serviço de domínio.
            _service.FindByIdAsync(idProcessamentoImagem)
                .Returns(Task.FromResult(ModelResultFactory.SucessResult(processamentoImagem)));

            //Act
            var handler = new ProcessamentoImagemFindByIdHandler(_service);
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.True(result.IsValid);
        }

        /// <summary>
        /// Testa a consulta com condição de pesquisa
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Consulta, true, 3)]
        public async Task ConsultarProcessamentoImagemComCondicao(IPagingQueryParam filter, Expression<Func<ProcessamentoImagem, object>> sortProp, IEnumerable<ProcessamentoImagem> notificacoes)
        {
            //Arrange
            var param = new PagingQueryParam<ProcessamentoImagem>() { CurrentPage = 1, Take = 10 };
            var command = new ProcessamentoImagemGetItemsCommand(filter, param.ConsultRule(), sortProp);

            //Mockando retorno do serviço de domínio.
            _service.GetItemsAsync(Arg.Any<PagingQueryParam<ProcessamentoImagem>>(),
                Arg.Any<Expression<Func<ProcessamentoImagem, bool>>>(),
                Arg.Any<Expression<Func<ProcessamentoImagem, object>>>())
                .Returns(new ValueTask<PagingQueryResult<ProcessamentoImagem>>(new PagingQueryResult<ProcessamentoImagem>(new List<ProcessamentoImagem>(notificacoes))));

            //Act
            var handler = new ProcessamentoImagemGetItemsHandler(_service);
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.True(result.Content.Any());
        }


        /// <summary>
        /// Testa a consulta sem condição de pesquisa
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Consulta, true, 3)]
        public async Task ConsultarProcessamentoImagemSemCondicao(IPagingQueryParam filter, Expression<Func<ProcessamentoImagem, object>> sortProp, IEnumerable<ProcessamentoImagem> notificacoes)
        {
            //Arrange
            var command = new ProcessamentoImagemGetItemsCommand(filter, sortProp);

            //Mockando retorno do serviço de domínio.
            _service.GetItemsAsync(filter, sortProp)
                .Returns(new ValueTask<PagingQueryResult<ProcessamentoImagem>>(new PagingQueryResult<ProcessamentoImagem>(new List<ProcessamentoImagem>(notificacoes))));

            //Act
            var handler = new ProcessamentoImagemGetItemsHandler(_service);
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
                        return ProcessamentoImagemMock.ObterDadosValidos(quantidade);
                    else
                        return ProcessamentoImagemMock.ObterDadosInvalidos(quantidade);
                case enmTipo.Alteracao:
                    if (dadosValidos)
                        return ProcessamentoImagemMock.ObterDadosValidos(quantidade)
                            .Select(i => new object[] { Guid.NewGuid() }.Concat(i).ToArray());
                    else
                        return ProcessamentoImagemMock.ObterDadosInvalidos(quantidade)
                            .Select(i => new object[] { Guid.NewGuid() }.Concat(i).ToArray());
                case enmTipo.Consulta:
                    if (dadosValidos)
                        return ProcessamentoImagemMock.ObterDadosConsultaValidos(quantidade);
                    else
                        return ProcessamentoImagemMock.ObterDadosConsultaInValidos(quantidade);
                default:
                    return null;
            }
        }


        #endregion
    }
}
