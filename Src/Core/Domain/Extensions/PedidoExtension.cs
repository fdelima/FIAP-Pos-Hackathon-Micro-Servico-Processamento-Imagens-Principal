using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Entities;
using System.Linq.Expressions;

namespace FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Extensions
{
    /// <summary>
    /// Extensão da model para informar os campos de validação.
    /// </summary>
    public static class PedidoExtension
    {
        /// <summary>
        /// Retorna a regra de validação a ser utilizada na atualização.
        /// </summary>
        public static Expression<Func<Pedido, bool>> ConsultRule(this PagingQueryParam<Pedido> param)
        {
            return x => (x.IdPedido.Equals(param.ObjFilter.IdPedido) || param.ObjFilter.IdPedido.Equals(default)) &&
                        (x.Data.Equals(param.ObjFilter.Data) || param.ObjFilter.Data.Equals(default)) &&
                        (x.IdDispositivo.Equals(param.ObjFilter.IdDispositivo) || param.ObjFilter.IdDispositivo.Equals(default)) &&
                        (x.Status.ToString().Contains(param.ObjFilter.Status.ToString()) || string.IsNullOrWhiteSpace(param.ObjFilter.Status.ToString())) &&
                        (x.DataStatusPedido.Equals(param.ObjFilter.DataStatusPedido) || param.ObjFilter.DataStatusPedido.Equals(default)) &&
                        (x.IdCliente.Equals(param.ObjFilter.IdCliente) || param.ObjFilter.IdCliente == null || param.ObjFilter.IdCliente.Equals(default));
        }

        /// <summary>
        /// Retorna a propriedade a ser ordenada
        /// </summary>
        public static Expression<Func<Pedido, object>> SortProp(this PagingQueryParam<Pedido> param)
        {
            switch (param?.SortProperty?.ToLower())
            {
                case "idpedido":
                    return fa => fa.IdPedido;
                case "iddispositivo":
                    return fa => fa.IdDispositivo;
                case "status":
                    return fa => fa.Status;
                case "datastatuspedido":
                    return fa => fa.DataStatusPedido;
                case "idcliente":
                    return fa => fa.IdCliente;
                default: return fa => fa.Data;
            }
        }
    }
}
