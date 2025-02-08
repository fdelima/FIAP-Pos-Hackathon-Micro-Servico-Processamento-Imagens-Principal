using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Entities;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Extensions;

namespace TestProject.MockData
{
    /// <summary>
    /// Mock de dados das ações
    /// </summary>
    public class ProcessamentoImagemMock
    {
        /// <summary>
        /// Mock de dados válidos
        /// </summary>
        public static IEnumerable<object[]> ObterDadosValidos(int quantidade)
        {
            for (var index = 1; index <= quantidade; index++)
                yield return new object[]
                {
                    DateTime.Now,
                    $"usuario{index}@fiap.com.br",
                    DateTime.Now,
                    $"Nome Arquivo {index}",
                    $"Nome Arquivo Zip Download{index}",
                    1000
                };
        }

        /// <summary>
        /// Mock de dados inválidos
        /// </summary>
        public static IEnumerable<object[]> ObterDadosInvalidos(int quantidade)
        {
            for (var index = 1; index <= quantidade; index++)
                yield return new object[]
                {
                    null,
                    string.Empty,
                    null,
                    null,
                    null,
                    0
                };
        }

        /// <summary>
        /// Mock de dados válidos
        /// </summary>
        /// Mock de dados válidos para consulta
        /// </summary>
        public static IEnumerable<object[]> ObterDadosConsultaValidos(int quantidade)
        {
            for (var index = 1; index <= quantidade; index++)
            {
                var notificacoes = new List<ProcessamentoImagem>();
                for (var index2 = 1; index2 <= quantidade; index2++)
                {
                    notificacoes.Add(new ProcessamentoImagem
                    {
                        IdProcessamentoImagem = Guid.NewGuid(),
                        Data = DateTime.Now,
                        Usuario = $"usuario{index}@fiap.com.br",
                        DataEnvio = DateTime.Now,
                        NomeArquivo = $"Nome Arquivo {index}",
                        TamanhoArquivo = 1000,
                        NomeArquivoZipDownload = $"Nome Arquivo Zip Download{index}"
                    });
                }
                var param = new PagingQueryParam<ProcessamentoImagem>() { CurrentPage = 1, Take = 10 };
                yield return new object[]
                {
                    param,
                    param.SortProp(),
                    notificacoes
                };
            }
        }

        /// <summary>
        /// Mock de dados inválidos
        /// </summary>
        public static IEnumerable<object[]> ObterDadosConsultaInValidos(int quantidade)
        {
            for (var index = 1; index <= quantidade; index++)
            {
                var notificacoes = new List<ProcessamentoImagem>();
                for (var index2 = 1; index2 <= quantidade; index2++)
                {
                    notificacoes.Add(new ProcessamentoImagem
                    {
                        IdProcessamentoImagem = Guid.NewGuid(),
                        Data = DateTime.Now,
                        Usuario = $"usuario{index}@fiap.com.br",
                        DataEnvio = DateTime.Now,
                        NomeArquivo = string.Empty,
                        TamanhoArquivo = 1000,
                        NomeArquivoZipDownload = $"Nome Arquivo Zip Download{index}"
                    });
                }
                var param = new PagingQueryParam<ProcessamentoImagem>() { CurrentPage = 1, Take = 10 };
                yield return new object[]
                {
                    param,
                    param.SortProp(),
                    notificacoes
                };
            }
        }

        /// <summary>
        /// Mock de dados Válidos
        /// </summary>
        public static IEnumerable<object[]> ObterDadosConsultaPorIdValidos(int quantidade)
        {
            for (var index = 1; index <= quantidade; index++)
                yield return new object[]
                {
                    Guid.NewGuid()
                };
        }

        /// <summary>
        /// Mock de dados inválidos
        /// </summary>
        public static IEnumerable<object[]> ObterDadosConsultaPorIdInvalidos(int quantidade)
        {
            for (var index = 1; index <= quantidade; index++)
                yield return new object[]
                {
                    Guid.Empty
                };
        }
    }
}
