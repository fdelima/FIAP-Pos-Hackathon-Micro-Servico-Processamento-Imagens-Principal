using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.EntityFrameworkCore.Extensions;

namespace FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Infra.Mappings;

internal class MercadoPagoWebhoockMap : IEntityTypeConfiguration<MercadoPagoWebhoock>
{
    public void Configure(EntityTypeBuilder<MercadoPagoWebhoock> builder)
    {
        builder.ToCollection("pagamento");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedNever()
            .HasElementName("_id");
        builder.Property(e => e.Origem).HasElementName("_origem");
        builder.Property(e => e.IdMercadoPago).HasElementName("id");
        builder.Property(e => e.LiveMode).HasElementName("live_mode");
        builder.Property(e => e.Type).HasElementName("type");
        builder.Property(e => e.DateCreated).HasElementName("date_created");
        builder.Property(e => e.UserId).HasElementName("user_id");
        builder.Property(e => e.ApiVersion).HasElementName("api_version");
        builder.Property(e => e.Action).HasElementName("action");
        builder.Property(e => e.Data).HasElementName("data");

    }
}
