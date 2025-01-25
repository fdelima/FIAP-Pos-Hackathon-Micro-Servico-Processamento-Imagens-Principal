﻿using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Application.UseCases.ProcessamentoImagem.Commands;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Interfaces;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Models;
using MediatR;

namespace FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Application.UseCases.ProcessamentoImagem.Handlers
{
    public class ProcessamentoImagemFindByIdHandler : IRequestHandler<ProcessamentoImagemFindByIdCommand, ModelResult>
    {
        private readonly IProcessamentoImagemService _service;

        public ProcessamentoImagemFindByIdHandler(IProcessamentoImagemService service)
        {
            _service = service;
        }

        public async Task<ModelResult> Handle(ProcessamentoImagemFindByIdCommand command, CancellationToken cancellationToken = default)
        {
            return await _service.FindByIdAsync(command.Id);
        }
    }
}
