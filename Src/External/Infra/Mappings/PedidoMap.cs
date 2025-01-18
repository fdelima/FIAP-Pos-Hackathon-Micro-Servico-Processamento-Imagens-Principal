using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.EntityFrameworkCore.Extensions;

namespace FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Infra.Mappings;

internal class PedidoMap : IEntityTypeConfiguration<Pedido>
{
    public void Configure(EntityTypeBuilder<Pedido> builder)
    {
        builder.ToCollection("pedido");
        builder.HasKey(e => e.IdPedido);

        builder.Property(e => e.IdPedido)
            .ValueGeneratedNever()
            .HasElementName("_id");
        builder.Property(e => e.Data)
            .HasElementName("data");
        builder.Property(e => e.DataStatusPedido)
            .HasElementName("data_status_pedido");
        builder.Property(e => e.IdCliente).HasElementName("id_cliente");
        builder.Property(e => e.IdDispositivo).HasElementName("id_dispositivo");
        builder.Property(e => e.Status)
            .HasMaxLength(50)
            .HasElementName("status");
        builder.Property(e => e.StatusPagamento)
            .HasMaxLength(50)
            .HasElementName("status_pagamento");
        builder.Property(e => e.DataStatusPagamento)
            .HasElementName("data_status_pagamento");
    }
}
