using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Api;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Models;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.IoC;
using Microsoft.AspNetCore.Http.Features;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage(Justification = "Arquivo de configuração")]
public class Program
{
    private static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        App.SetAtributesAppFromDll();

        // Add services to the container.
        builder.Services.AddControllers();
        builder.Services.AddHostedService(sp =>
        {
            return new QueueWorker(sp);
        }); // Adicione o seu Worker Service

        builder.Services.ConfigureModelValidations();
        builder.Services.AddSwagger("Web Api C# Sample");

        builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDb"));

        builder.Services.RegisterDependencies(builder.Configuration);

        builder.Services.Configure<FormOptions>(opt =>
        {
            opt.BufferBody = true;
            opt.MultipartBodyLengthLimit = Util.MaxUploadBytesRequest;
            opt.BufferBodyLengthLimit = Util.MaxUploadBytesRequest;
        });

        WebApplication app = builder.Build();

        app.ConfigureSwagger();

        app.ConfigureReDoc();

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.AddGlobalErrorHandler();

        app.Run();
    }
}