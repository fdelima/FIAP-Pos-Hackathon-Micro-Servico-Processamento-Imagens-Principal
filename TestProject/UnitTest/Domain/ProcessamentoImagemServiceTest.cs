using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Interfaces;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Models;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain;
using FluentValidation;
using NSubstitute;
using System.Linq.Expressions;
using TestProject.MockData;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Entities;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Validator;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Services;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Application.UseCases.ProcessamentoImagem.Commands;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Extensions;
using Newtonsoft.Json;

namespace TestProject.UnitTest.Domain
{
    /// <summary>
    /// Classe de teste.
    /// </summary>
    public partial class ProcessamentoImagemServiceTest
    {
        private readonly IStorageService _storageService;
        private readonly IMessagerService _messagerService;
        private readonly IValidator<ProcessamentoImagem> _validator;
        private readonly IGateways<Notificacao> _notificacaoGatewayMock;
        private readonly IGateways<ProcessamentoImagem> _gatewayProcessamentoImagemMock;

        /// <summary>
        /// Construtor da classe de teste.
        /// </summary>
        public ProcessamentoImagemServiceTest()
        {
            _validator = new ProcessamentoImagemValidator();
            _storageService = Substitute.For<IStorageService>();
            _messagerService = Substitute.For<IMessagerService>();
            _notificacaoGatewayMock = Substitute.For<IGateways<Notificacao>>();
            _gatewayProcessamentoImagemMock = Substitute.For<IGateways<ProcessamentoImagem>>();
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

            var domainService = new ProcessamentoImagemService(_gatewayProcessamentoImagemMock, _validator, _notificacaoGatewayMock, _messagerService, _storageService);

            //Act
            var result = await domainService.InsertAsync(processamentoImagem);

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

            var domainService = new ProcessamentoImagemService(_gatewayProcessamentoImagemMock, _validator, _notificacaoGatewayMock, _messagerService, _storageService);

            //Act
            var result = await domainService.InsertAsync(processamentoImagem);

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

            var domainService = new ProcessamentoImagemService(_gatewayProcessamentoImagemMock, _validator, _notificacaoGatewayMock, _messagerService, _storageService);

            //Mockando retorno do metodo interno do UpdateAsync
            _gatewayProcessamentoImagemMock.UpdateAsync(Arg.Any<ProcessamentoImagem>())
                .Returns(Task.FromResult(processamentoImagem));

            //Act
            var result = await domainService.UpdateAsync(processamentoImagem);

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

            var domainService = new ProcessamentoImagemService(_gatewayProcessamentoImagemMock, _validator, _notificacaoGatewayMock, _messagerService, _storageService);

            //Act
            var result = await domainService.UpdateAsync(processamentoImagem);

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

            var domainService = new ProcessamentoImagemService(_gatewayProcessamentoImagemMock, _validator, _notificacaoGatewayMock, _messagerService, _storageService);

            //Mockando retorno do metodo interno do FindByIdAsync
            _gatewayProcessamentoImagemMock.FindByIdAsync(idProcessamentoImagem)
                .Returns(new ValueTask<ProcessamentoImagem>(processamentoImagem));

            _gatewayProcessamentoImagemMock.DeleteAsync(idProcessamentoImagem)
                .Returns(Task.FromResult(ModelResultFactory.SucessResult()));

            //Act
            var result = await domainService.DeleteAsync(idProcessamentoImagem);

            //Assert
            Assert.True(result.IsValid);
        }

        /// <summary>
        /// Testa a consulta por id
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Alteracao, true, 3)]
        public async Task ConsultarProcessamentoImagemPorIdComDadosValidos(Guid idProcessamentoImagem, DateTime data, string usuario, DateTime dataEnvio, string nomeArquivo, string nomeArquivoZipDownload, long tamanhoArquivo)
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

            var domainService = new ProcessamentoImagemService(_gatewayProcessamentoImagemMock, _validator, _notificacaoGatewayMock, _messagerService, _storageService);

            //Mockando retorno do metodo interno do FindByIdAsync
            _gatewayProcessamentoImagemMock.FindByIdAsync(idProcessamentoImagem)
                .Returns(new ValueTask<ProcessamentoImagem>(processamentoImagem));

            //Act
            var result = await domainService.FindByIdAsync(idProcessamentoImagem);

            //Assert
            Assert.True(result.IsValid);
        }

        /// <summary>
        /// Testa a consulta por id
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Alteracao, true, 3)]
        public async Task ConsultarProcessamentoImagemPorIdComDadosInvalidos(Guid idProcessamentoImagem, DateTime data, string usuario, DateTime dataEnvio, string nomeArquivo, string nomeArquivoZipDownload, long tamanhoArquivo)
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

            var domainService = new ProcessamentoImagemService(_gatewayProcessamentoImagemMock, _validator, _notificacaoGatewayMock, _messagerService, _storageService);

            //Act
            var result = await domainService.FindByIdAsync(idProcessamentoImagem);

            //Assert
            Assert.False(result.IsValid);
        }

        /// <summary>
        /// Testa a consulta Valida
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Consulta, true, 3)]
        public async Task ConsultarProcessamentoImagem(IPagingQueryParam filter, Expression<Func<ProcessamentoImagem, object>> sortProp, IEnumerable<ProcessamentoImagem> processamentoImagens)
        {
            ///Arrange
            var domainService = new ProcessamentoImagemService(_gatewayProcessamentoImagemMock, _validator, _notificacaoGatewayMock, _messagerService, _storageService);

            //Mockando retorno do metodo interno do GetItemsAsync
            _gatewayProcessamentoImagemMock.GetItemsAsync(Arg.Any<PagingQueryParam<ProcessamentoImagem>>(),
                Arg.Any<Expression<Func<ProcessamentoImagem, object>>>())
                .Returns(new ValueTask<PagingQueryResult<ProcessamentoImagem>>(new PagingQueryResult<ProcessamentoImagem>(new List<ProcessamentoImagem>(processamentoImagens))));


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
        public async Task ConsultarProcessamentoImagemComCondicao(IPagingQueryParam filter, Expression<Func<ProcessamentoImagem, object>> sortProp, IEnumerable<ProcessamentoImagem> processamentoImagens)
        {
            ///Arrange
            var param = new PagingQueryParam<ProcessamentoImagem>() { CurrentPage = 1, Take = 10 };
            var command = new ProcessamentoImagemGetItemsCommand(filter, param.ConsultRule(), sortProp);

            //Mockando retorno do metodo interno do GetItemsAsync
            _gatewayProcessamentoImagemMock.GetItemsAsync(Arg.Any<PagingQueryParam<ProcessamentoImagem>>(),
                Arg.Any<Expression<Func<ProcessamentoImagem, bool>>>(),
                Arg.Any<Expression<Func<ProcessamentoImagem, object>>>())
                .Returns(new ValueTask<PagingQueryResult<ProcessamentoImagem>>(new PagingQueryResult<ProcessamentoImagem>(new List<ProcessamentoImagem>(processamentoImagens))));

            //Act
            var result = await _gatewayProcessamentoImagemMock.GetItemsAsync(filter, param.ConsultRule(), sortProp);

            //Assert
            Assert.True(result.Content.Any());
        }

        /// <summary>
        /// Testa a consulta sem condição de pesquisa
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Consulta, true, 3)]
        public async Task ConsultarProcessamentoImagemSemCondicao(IPagingQueryParam filter, Expression<Func<ProcessamentoImagem, object>> sortProp, IEnumerable<ProcessamentoImagem> processamentoImagens)
        {
            ///Arrange
            var command = new ProcessamentoImagemGetItemsCommand(filter, sortProp);

            //Mockando retorno do metodo interno do GetItemsAsync
            _gatewayProcessamentoImagemMock.GetItemsAsync(filter, sortProp)
                .Returns(new ValueTask<PagingQueryResult<ProcessamentoImagem>>(new PagingQueryResult<ProcessamentoImagem>(new List<ProcessamentoImagem>(processamentoImagens))));

            //Act
            var result = await _gatewayProcessamentoImagemMock.GetItemsAsync(filter, sortProp);

            //Assert
            Assert.True(result.Content.Any());
        }

        [Fact]
        public async Task SendMessageToQueueAsyncTest()
        {
            ///Arrange
            var domainService = new ProcessamentoImagemService(_gatewayProcessamentoImagemMock, _validator, _notificacaoGatewayMock, _messagerService, _storageService);

            //Mockando retorno do metodo interno do GetItemsAsync
            _gatewayProcessamentoImagemMock.GetItemsAsync(Arg.Any<Expression<Func<ProcessamentoImagem, bool>>>())
                .Returns(new ValueTask<PagingQueryResult<ProcessamentoImagem>>(new PagingQueryResult<ProcessamentoImagem>(new List<ProcessamentoImagem>())));


            //Act
            var result = await domainService.SendMessageToQueueAsync();

            //Assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public async Task ReceiverMessageInQueueAsyncTest()
        {
            ///Arrange
            var domainService = new ProcessamentoImagemService(_gatewayProcessamentoImagemMock, _validator, _notificacaoGatewayMock, _messagerService, _storageService);

            var msg = new ProcessamentoImagemProcessModel
            {
                IdProcessamentoImagem = Guid.NewGuid(),
                Usuario = "usuario",
                DataFimProcessamento = DateTime.Now,
                NomeArquivo = "nomeArquivo",
                TamanhoArquivo = 0,
                NomeArquivoZipDownload = "nomeArquivoZipDownload.zip"
            };

            //Mockando retorno do metodo interno do ReceiveMessageAsync
            _messagerService.ReceiveMessageAsync()
                .Returns(new MessageModel { MessageId = Guid.NewGuid().ToString(), MessageText = JsonConvert.SerializeObject(msg), PopReceipt = "PopReceipt" });

            _gatewayProcessamentoImagemMock.FindByIdAsync(msg.IdProcessamentoImagem)
                .Returns(new ProcessamentoImagem { NomeArquivo = "nomearquivo", NomeArquivoZipDownload = "zip.zip", Usuario = "usr" });
            //Act
            var result = await domainService.ReceiverMessageInQueueAsync();

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
                default:
                    return null;
            }
        }

        #endregion

    }
}
