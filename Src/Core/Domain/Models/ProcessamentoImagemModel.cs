using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Models
{
    public class ProcessamentoImagemModel : ProcessamentoImagem
    {
        public required IFormFile FormFile { get; set; }
    }
}
