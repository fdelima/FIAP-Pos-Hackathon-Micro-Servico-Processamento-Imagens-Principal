﻿using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.EntityFrameworkCore.Extensions;

namespace FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Infra.Mappings;

internal class NotificacaoMap : IEntityTypeConfiguration<Notificacao>
{
    public void Configure(EntityTypeBuilder<Notificacao> builder)
    {
        builder.ToCollection("notificacao");

        builder.HasKey(e => e.IdNotificacao);

        builder.Property(e => e.IdNotificacao)
            .ValueGeneratedNever()
            .HasElementName("_id");

        builder.Property(e => e.Data)
            .HasElementName("data");

        builder.Property(e => e.Usuario)
            .HasElementName("usuario");

        builder.Property(e => e.Mensagem)
            .HasElementName("mensagem");
    }
}
