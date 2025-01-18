using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Application.UseCases.MercadoPago.Commands;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Application.UseCases.MercadoPago.Handlers;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Application.UseCases.Pedido.Commands;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Application.UseCases.Pedido.Handlers;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Models;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Application.IoC
{
    [ExcludeFromCodeCoverage(Justification = "Arquivo de configuração")]
    internal static class CommandsRegistry
    {
        public static void RegisterCommands(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

            //Pedido
            services.AddScoped<IRequestHandler<ReceberPedidoCommand, ModelResult>, ReceberPedidoHandler>();
            services.AddScoped<IRequestHandler<MercadoPagoWebhoockCommand, ModelResult>, MercadoPagoWebhoockHandler>();
            services.AddScoped<IRequestHandler<PedidoConsultarPagamentoCommand, ModelResult>, PedidoConsultarPagamentoHandler>();
        }
    }
}
