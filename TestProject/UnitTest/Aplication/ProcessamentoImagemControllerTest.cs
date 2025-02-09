using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Interfaces;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Models;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain;
using FluentValidation;
using MediatR;
using NSubstitute;
using System.Linq.Expressions;
using TestProject.MockData;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Entities;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Validator;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Application.Controllers;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Application.UseCases.ProcessamentoImagem.Commands;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Extensions;
using System.ComponentModel.DataAnnotations;

namespace TestProject.UnitTest.Aplication
{
    /// <summary>
    /// Classe de teste.
    /// </summary>
    public partial class ProcessamentoImagemControllerTest
    {
        private readonly IMediator _mediator;
        private readonly IStorageService _storageService;
        private readonly IValidator<ProcessamentoImagem> _validator;
        private readonly IValidator<ProcessamentoImagemUploadModel> _uploadValidator;


        /// <summary>
        /// Construtor da classe de teste.
        /// </summary>
        public ProcessamentoImagemControllerTest()
        {
            _mediator = Substitute.For<IMediator>();
            _validator = new ProcessamentoImagemValidator();
            _storageService = Substitute.For<IStorageService>();
            _uploadValidator = new ProcessamentoImagemUploadModelValidator();
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
                IdProcessamentoImagem = Guid.NewGuid(),
                Data = data,
                Usuario = usuario,
                DataEnvio = dataEnvio,
                NomeArquivo = nomeArquivo,
                TamanhoArquivo = tamanhoArquivo,
                NomeArquivoZipDownload = nomeArquivoZipDownload
            };

            var aplicationController = new ProcessamentoImagemController(_mediator, _validator, _storageService, _uploadValidator);

            //Mockando retorno do mediator.
            _mediator.Send(Arg.Any<ProcessamentoImagemPostCommand>())
                .Returns(Task.FromResult(ModelResultFactory.SucessResult()));

            //Act
            var result = await aplicationController.PostAsync(processamentoImagem);

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
                IdProcessamentoImagem = Guid.NewGuid(),
                Data = data,
                Usuario = usuario,
                DataEnvio = dataEnvio,
                NomeArquivo = nomeArquivo,
                TamanhoArquivo = tamanhoArquivo,
                NomeArquivoZipDownload = nomeArquivoZipDownload
            };

            var aplicationController = new ProcessamentoImagemController(_mediator, _validator, _storageService, _uploadValidator);

            //Act
            var result = await aplicationController.PostAsync(processamentoImagem);

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

            var aplicationController = new ProcessamentoImagemController(_mediator, _validator, _storageService, _uploadValidator);

            //Mockando retorno do mediator.
            _mediator.Send(Arg.Any<ProcessamentoImagemPutCommand>())
                .Returns(Task.FromResult(ModelResultFactory.SucessResult()));

            //Act
            var result = await aplicationController.PutAsync(idProcessamentoImagem, processamentoImagem);

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

            var aplicationController = new ProcessamentoImagemController(_mediator, _validator, _storageService, _uploadValidator);

            //Act
            var result = await aplicationController.PutAsync(idProcessamentoImagem, processamentoImagem);

            //Assert
            Assert.False(result.IsValid);
        }

        /// <summary>
        /// Testa a deletar
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.ConsultaPorId, true, 3)]
        public async Task DeletarProcessamentoImagem(Guid idProcessamentoImagem)
        {
            ///Arrange
            var aplicationController = new ProcessamentoImagemController(_mediator, _validator, _storageService, _uploadValidator);

            //Mockando retorno do mediator.
            _mediator.Send(Arg.Any<ProcessamentoImagemDeleteCommand>())
                .Returns(Task.FromResult(ModelResultFactory.SucessResult()));

            //Act
            var result = await aplicationController.DeleteAsync(idProcessamentoImagem);

            //Assert
            Assert.True(result.IsValid);
        }

        /// <summary>
        /// Testa a consulta por id
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.ConsultaPorId, true, 3)]
        public async Task ConsultarProcessamentoImagemPorId(Guid idProcessamentoImagem)
        {
            ///Arrange
            var aplicationController = new ProcessamentoImagemController(_mediator, _validator, _storageService, _uploadValidator);

            //Mockando retorno do mediator.
            _mediator.Send(Arg.Any<ProcessamentoImagemFindByIdCommand>())
                .Returns(Task.FromResult(ModelResultFactory.SucessResult()));

            //Act
            var result = await aplicationController.FindByIdAsync(idProcessamentoImagem);

            //Assert
            Assert.True(result.IsValid);
        }

        /// <summary>
        /// Testa a consulta com condição de pesquisa
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Consulta, true, 3)]
        public async Task ConsultarProcessamentoImagemComCondicao(IPagingQueryParam filter, Expression<Func<ProcessamentoImagem, object>> sortProp, IEnumerable<ProcessamentoImagem> clientes)
        {
            ///Arrange
            var aplicationController = new ProcessamentoImagemController(_mediator, _validator, _storageService, _uploadValidator);
            var param = new PagingQueryParam<ProcessamentoImagem>() { CurrentPage = 1, Take = 10 };

            //Mockando retorno do mediator.
            _mediator.Send(Arg.Any<ProcessamentoImagemGetItemsCommand>())
                .Returns(Task.FromResult(new PagingQueryResult<ProcessamentoImagem>(new List<ProcessamentoImagem>(clientes), 1, 1)));

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
        public async Task ConsultarProcessamentoImagemSemCondicao(IPagingQueryParam filter, Expression<Func<ProcessamentoImagem, object>> sortProp, IEnumerable<ProcessamentoImagem> clientes)
        {
            ///Arrange
            var aplicationController = new ProcessamentoImagemController(_mediator, _validator, _storageService, _uploadValidator);
            var param = new PagingQueryParam<ProcessamentoImagem>() { CurrentPage = 1, Take = 10 };

            //Mockando retorno do mediator.
            _mediator.Send(Arg.Any<ProcessamentoImagemGetItemsCommand>())
                .Returns(Task.FromResult(new PagingQueryResult<ProcessamentoImagem>(new List<ProcessamentoImagem>(clientes), 1, 1)));

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
        public async Task ConsultarProcessamentoImagemComCondicaoInvalidos(IPagingQueryParam filter, Expression<Func<ProcessamentoImagem, object>> sortProp, IEnumerable<ProcessamentoImagem> processamentoImagens)
        {
            ///Arrange

            filter = null;
            var param = new PagingQueryParam<ProcessamentoImagem>() { CurrentPage = 1, Take = 10 };
            var aplicationController = new ProcessamentoImagemController(_mediator, _validator, _storageService, _uploadValidator);

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
        public async Task ConsultarProcessamentoImagemSemCondicaoInvalidos(IPagingQueryParam filter, Expression<Func<ProcessamentoImagem, object>> sortProp, IEnumerable<ProcessamentoImagem> processamentoImagens)
        {
            ///Arrange

            filter = null;
            var aplicationController = new ProcessamentoImagemController(_mediator, _validator, _storageService, _uploadValidator);

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

        [Fact]
        public async Task SendMessageToQueueAsync()
        {
            ///Arrange
            var aplicationController = new ProcessamentoImagemController(_mediator, _validator, _storageService, _uploadValidator);
           
            //Mockando retorno do mediator.
            _mediator.Send(Arg.Any<ProcessamentoImagemSendMessageToQueueCommand>())
                .Returns(Task.FromResult(ModelResultFactory.SucessResult()));

            //Act
            var result = await aplicationController.SendMessageToQueueAsync();

            //Assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public async Task ReceiverMessageInQueueAsync()
        {
            ///Arrange
            var aplicationController = new ProcessamentoImagemController(_mediator, _validator, _storageService, _uploadValidator);
          
            //Mockando retorno do mediator.
            _mediator.Send(Arg.Any<ProcessamentoImagemReceiverMessageInQueueCommand>())
                .Returns(Task.FromResult(ModelResultFactory.SucessResult()));

            //Act
            var result = await aplicationController.ReceiverMessageInQueueAsync();

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
                case enmTipo.ConsultaPorId:
                    if (dadosValidos)
                        return ProcessamentoImagemMock.ObterDadosConsultaPorIdValidos(quantidade);
                    else
                        return ProcessamentoImagemMock.ObterDadosConsultaPorIdInvalidos(quantidade);
                default:
                    return null;
            }
        }

        #endregion

    }
}

