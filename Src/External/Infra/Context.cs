﻿using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Entities;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Infra.Mappings;
using Microsoft.EntityFrameworkCore;

namespace FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Infra
{
    public partial class Context : DbContext
    {
        public Context(DbContextOptions<Context> options)
            : base(options) { }

        #region [ DbSets ]

        public virtual DbSet<Notificacao> Notificacaos { get; set; }

        public virtual DbSet<ProcessamentoImagem> Pedidos { get; set; }

        #endregion DbSets

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //TODO: Map :: 2 - Adicione sua configuração aqui

            modelBuilder.ApplyConfiguration(new NotificacaoMap());
            modelBuilder.ApplyConfiguration(new ProcessamentoImagemMap());

            base.OnModelCreating(modelBuilder);

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }
    }
}