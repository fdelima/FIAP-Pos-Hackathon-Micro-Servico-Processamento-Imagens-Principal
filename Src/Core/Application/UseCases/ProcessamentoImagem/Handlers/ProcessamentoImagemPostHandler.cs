﻿using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Application.UseCases.ProcessamentoImagem.Commands;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Interfaces;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Models;
using MediatR;

namespace FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Application.UseCases.ProcessamentoImagem.Handlers
{
    public class ProcessamentoImagemPostHandler : IRequestHandler<ProcessamentoImagemPostCommand, ModelResult>
    {
        private readonly IService<Domain.Entities.ProcessamentoImagem> _service;

        public ProcessamentoImagemPostHandler(IService<Domain.Entities.ProcessamentoImagem> service)
        {
            _service = service;
        }

        public async Task<ModelResult> Handle(ProcessamentoImagemPostCommand command, CancellationToken cancellationToken = default)
        {
            return await _service.InsertAsync(command.Entity, command.BusinessRules);
        }
    }
}
