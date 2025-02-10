using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Models
{
    public class StatusModel
    {
        public StatusModel(ProcessamentoImagem p)
        {
            var lstStatus = new List<string>();
            if (p.DataFimProcessamento != null)
            {
                CurrentStatus = "Arquivo processado";
                lstStatus.Add($"DataFimProcessamento:{p.DataFimProcessamento}");
                lstStatus.Add($"DataInicioProcessamento:{p.DataInicioProcessamento}");
                lstStatus.Add($"DataEnviadoFila:{p.DataEnviadoFila}");
                lstStatus.Add($"DataEnvio:{p.DataEnvio}");
                StatusProcess = lstStatus.ToArray();
            }
            else if (p.DataInicioProcessamento != null)
            {
                CurrentStatus = "Arquivo sendo processado";
                lstStatus.Add($"DataInicioProcessamento:{p.DataInicioProcessamento}");
                lstStatus.Add($"DataEnviadoFila:{p.DataEnviadoFila}");
                lstStatus.Add($"DataEnvio:{p.DataEnvio}");
                StatusProcess = lstStatus.ToArray();
            }
            else if (p.DataEnviadoFila != null)
            {
                CurrentStatus = "Arquivo enviado para fila de processamento";
                lstStatus.Add($"DataEnviadoFila:{p.DataEnviadoFila}");
                lstStatus.Add($"DataEnvio:{p.DataEnvio}");
                StatusProcess = lstStatus.ToArray();
            }
            else
            {
                CurrentStatus = "Arquivo Recebido";
                lstStatus.Add($"DataEnvio:{p.DataEnvio}");
                StatusProcess = lstStatus.ToArray();
            }
        }

        public string CurrentStatus { get; set; }
        public string[] StatusProcess { get; set; }
    }
}
