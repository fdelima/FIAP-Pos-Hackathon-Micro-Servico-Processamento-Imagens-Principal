using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Entities;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Extensions;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Interfaces;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Infra.Gateways;
using System.Linq.Expressions;
using TestProject.Infra;
using TestProject.MockData;

namespace TestProject.IntegrationTest.External
{
    /// <summary>
    /// Classe de teste.
    /// </summary>
    public partial class ProcessamentoImagemGatewayTest : IClassFixture<IntegrationTestsBase>
    {
        internal readonly MongoTestFixture _mongoTestFixture;

        /// <summary>
        /// Construtor da classe de teste.
        /// </summary>
        public ProcessamentoImagemGatewayTest(IntegrationTestsBase data)
        {
            _mongoTestFixture = data._mongoTestFixture;
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

            //Act
            var _processamentoImagemGateway = new BaseGateway<ProcessamentoImagem>(_mongoTestFixture.GetDbContext());
            var result = await _processamentoImagemGateway.InsertAsync(processamentoImagem);

            //Assert
            try
            {
                await _processamentoImagemGateway.CommitAsync();
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

            var _processamentoImagemGateway = new BaseGateway<ProcessamentoImagem>(_mongoTestFixture.GetDbContext());
            var result = await _processamentoImagemGateway.InsertAsync(processamentoImagem);
            await _processamentoImagemGateway.CommitAsync();

            //Alterando
            processamentoImagem.NomeArquivo = nomeArquivo + " ALTERADO !!! ";

            var dbEntity = await _processamentoImagemGateway.FindByIdAsync(idProcessamentoImagem);

            //Act
            await _processamentoImagemGateway.UpdateAsync(dbEntity, processamentoImagem);
            await _processamentoImagemGateway.UpdateAsync(processamentoImagem);

            try
            {
                await _processamentoImagemGateway.CommitAsync();
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

            var _processamentoImagemGateway = new BaseGateway<ProcessamentoImagem>(_mongoTestFixture.GetDbContext());
            await _processamentoImagemGateway.InsertAsync(processamentoImagem);
            await _processamentoImagemGateway.CommitAsync();

            //Act
            await _processamentoImagemGateway.DeleteAsync(idProcessamentoImagem);

            //Assert
            try
            {
                await _processamentoImagemGateway.CommitAsync();
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

            var _processamentoImagemGateway = new BaseGateway<ProcessamentoImagem>(_mongoTestFixture.GetDbContext());
            await _processamentoImagemGateway.InsertAsync(processamentoImagem);
            await _processamentoImagemGateway.CommitAsync();

            //Act
            var result = await _processamentoImagemGateway.FindByIdAsync(idProcessamentoImagem);

            //Assert
            Assert.True(result != null);
        }

        /// <summary>
        /// Testa a consulta Valida
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Consulta, true, 3)]
        public async Task ConsultarProcessamentoImagem(IPagingQueryParam filter, Expression<Func<ProcessamentoImagem, object>> sortProp, IEnumerable<ProcessamentoImagem> processamentoImagens)
        {
            ///Arrange
            var _processamentoImagemGateway = new BaseGateway<ProcessamentoImagem>(_mongoTestFixture.GetDbContext());

            foreach (var processamentoImagem in processamentoImagens)
            {
                await _processamentoImagemGateway.InsertAsync(processamentoImagem);
                await _processamentoImagemGateway.CommitAsync();
            }

            //Act
            var result = await _processamentoImagemGateway.GetItemsAsync(filter, sortProp);

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
            var _processamentoImagemGateway = new BaseGateway<ProcessamentoImagem>(_mongoTestFixture.GetDbContext());

            foreach (var processamentoImagem in processamentoImagens)
            {
                await _processamentoImagemGateway.InsertAsync(processamentoImagem);
                await _processamentoImagemGateway.CommitAsync();
            }

            var param = new PagingQueryParam<ProcessamentoImagem>() { CurrentPage = 1, Take = 10, ObjFilter = processamentoImagens.ElementAt(0) };

            //Act
            var result = await _processamentoImagemGateway.GetItemsAsync(filter, param.ConsultRule(), sortProp);

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
            var _processamentoImagemGateway = new BaseGateway<ProcessamentoImagem>(_mongoTestFixture.GetDbContext());

            foreach (var processamentoImagem in processamentoImagens)
            {
                await _processamentoImagemGateway.InsertAsync(processamentoImagem);
                await _processamentoImagemGateway.CommitAsync();
            }

            //Act
            var result = await _processamentoImagemGateway.GetItemsAsync(filter, sortProp);

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
