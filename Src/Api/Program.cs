using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Api;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Models;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.IoC;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage(Justification = "Arquivo de configura��o")]
public class Program
{
    private static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        App.SetAtributesAppFromDll();

        // Add services to the container.
        builder.Services.AddControllers();
        builder.Services.ConfigureModelValidations();
        builder.Services.AddSwagger("Web Api C# Sample");

        builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDb"));

        builder.Services.RegisterDependencies(builder.Configuration);

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