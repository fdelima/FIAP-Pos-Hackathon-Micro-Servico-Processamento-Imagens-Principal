using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Entities;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Messages;
using FluentValidation;

namespace FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Validator
{
    /// <summary>
    /// Regras de validação da model
    /// </summary>
    public class ProcessamentoImagemValidator : AbstractValidator<ProcessamentoImagem>
    {
        /// <summary>
        /// Contrutor das regras de validação da model
        /// </summary>
        public ProcessamentoImagemValidator()
        {
            RuleFor(c => c.IdProcessamentoImagem).NotEmpty().WithMessage(ValidationMessages.RequiredField);
            RuleFor(c => c.Data).NotEmpty().WithMessage(ValidationMessages.RequiredField);
            RuleFor(c => c.Usuario).NotEmpty().WithMessage(ValidationMessages.RequiredField);
            RuleFor(c => c.DataEnvio).NotEmpty().WithMessage(ValidationMessages.RequiredField);
            RuleFor(c => c.NomeArquivo).NotEmpty().WithMessage(ValidationMessages.RequiredField);
            RuleFor(c => c.TamanhoArquivo).NotEmpty().WithMessage(ValidationMessages.RequiredField);
            RuleFor(c => c.NomeArquivoZipDownload).NotEmpty().WithMessage(ValidationMessages.RequiredField);

        }
    }
}
