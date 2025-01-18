using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Application.UseCases.ProcessamentoImagem.Commands;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Application.UseCases.ProcessamentoImagem.Handlers;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Entities;
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

            //ProcessamentoImagem
            services.AddScoped<IRequestHandler<ProcessamentoImagemPostCommand, ModelResult>, ProcessamentoImagemPostHandler>();
            services.AddScoped<IRequestHandler<ProcessamentoImagemPutCommand, ModelResult>, ProcessamentoImagemPutHandler>();
            services.AddScoped<IRequestHandler<ProcessamentoImagemDeleteCommand, ModelResult>, ProcessamentoImagemDeleteHandler>();
            services.AddScoped<IRequestHandler<ProcessamentoImagemFindByIdCommand, ModelResult>, ProcessamentoImagemFindByIdHandler>();
            services.AddScoped<IRequestHandler<ProcessamentoImagemGetItemsCommand, PagingQueryResult<ProcessamentoImagem>>, ProcessamentoImagemGetItemsHandler>();

        }
    }
}
