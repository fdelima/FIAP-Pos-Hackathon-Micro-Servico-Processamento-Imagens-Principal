using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Models.MercadoPago;
using Newtonsoft.Json;

namespace FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Entities
{
    public class MercadoPagoWebhoock : Pagamento
    {
        public int IdMercadoPago { get; set; }

        public bool LiveMode { get; set; }

        public string Type { get; set; }

        public DateTime DateCreated { get; set; }

        public int UserId { get; set; }

        public string ApiVersion { get; set; }

        public string Action { get; set; }

        public string Data { get; set; }

        public static explicit operator MercadoPagoWebhoock(MercadoPagoWebhoockModel model)
        {
            if (model == null)
                return null;

            return new MercadoPagoWebhoock
            {
                Id = Guid.NewGuid(),
                Origem = "Mercado Pago",
                IdMercadoPago = model.Id,
                LiveMode = model.LiveMode,
                Type = model.Type,
                DateCreated = model.DateCreated,
                UserId = model.UserId,
                ApiVersion = model.ApiVersion,
                Action = model.Action,
                Data = JsonConvert.SerializeObject(model.Data)
            };
        }
    }
}
