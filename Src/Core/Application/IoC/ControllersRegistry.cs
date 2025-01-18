using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Application.Controllers;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Application.IoC
{
    [ExcludeFromCodeCoverage(Justification = "Arquivo de configuração")]
    internal static class ControllersRegistry
    {
        public static void RegisterAppControllers(this IServiceCollection services)
        {
            //Controlles
            services.AddScoped(typeof(IController<Domain.Entities.ProcessamentoImagem>), typeof(ProcessamentoImagemController));
        }
    }
}