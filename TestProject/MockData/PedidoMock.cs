using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Entities;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Extensions;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.ValuesObject;

namespace TestProject.MockData
{
    /// <summary>
    /// Mock de dados das ações
    /// </summary>
    public class PedidoMock
    {

        /// <summary>
        /// Mock de dados válidos
        /// </summary>
        public static IEnumerable<object[]> ObterDadosValidos(int quantidade)
        {
            for (var index = 1; index <= quantidade; index++)
                yield return new object[]
                {
                    Guid.NewGuid(),
                    Guid.NewGuid(),
                    Guid.NewGuid(),
                    DateTime.Now,
                    enmPedidoStatus.RECEBIDO.ToString(),
                    DateTime.Now,
                    enmPedidoStatusPagamento.PENDENTE.ToString(),
                    DateTime.Now
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
                    Guid.Empty,
                    Guid.Empty,
                    Guid.Empty,
                    DateTime.Now,
                    enmPedidoStatus.RECEBIDO.ToString(),
                    DateTime.MinValue,
                    enmPedidoStatusPagamento.PENDENTE.ToString(),
                    DateTime.MinValue
                };
        }

        /// <summary>
        /// Mock de dados válidos
        /// </summary>
        public static IEnumerable<object[]> ObterDadosConsultaValidos(int quantidade)
        {
            for (var index = 1; index <= quantidade; index++)
            {
                var pedidos = new List<Pedido>();
                for (var index2 = 1; index <= quantidade; index++)
                {
                    var idPedido = Guid.NewGuid();
                    pedidos.Add(new Pedido
                    {
                        IdPedido = Guid.NewGuid(),
                        IdCliente = Guid.NewGuid(),
                        IdDispositivo = Guid.NewGuid(),
                        Data = DateTime.Now,
                        Status = enmPedidoStatus.RECEBIDO.ToString(),
                        DataStatusPedido = DateTime.Now,
                        StatusPagamento = enmPedidoStatusPagamento.PENDENTE.ToString(),
                        DataStatusPagamento = DateTime.Now
                    });
                }
                var param = new PagingQueryParam<Pedido>() { CurrentPage = 1, Take = 10 };
                yield return new object[]
                {
                    param,
                    param.SortProp(),
                    pedidos
                };
            }
        }

        /// <summary>
        /// Mock de dados inválidos
        /// </summary>
        public static IEnumerable<object[]> ObterDadosConsultaInValidos(int quantidade)
        {
            var pedidos = new List<Pedido>();
            for (var index = 1; index <= quantidade; index++)
                pedidos.Add(new Pedido
                {
                    IdPedido = Guid.NewGuid(),
                    IdDispositivo = Guid.NewGuid(),
                    Status = ((enmPedidoStatus)new Random().Next(0, 2)).ToString()
                });

            pedidos.Add(
                new Pedido
                {
                    IdPedido = Guid.NewGuid(),
                    Status = enmPedidoStatus.FINALIZADO.ToString()
                });

            for (var index = 1; index <= quantidade; index++)
            {
                var param = new PagingQueryParam<Pedido>() { CurrentPage = index, Take = 10 };
                yield return new object[]
                {
                    param,
                    param.SortProp(),
                    pedidos
                };
            }
        }

        public static IEnumerable<object[]> ObterDadosConsultaPorIdValidos(int quantidade)
        {
            for (var index = 1; index <= quantidade; index++)
                yield return new object[]
                {
                    Guid.NewGuid()
                };
        }

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
