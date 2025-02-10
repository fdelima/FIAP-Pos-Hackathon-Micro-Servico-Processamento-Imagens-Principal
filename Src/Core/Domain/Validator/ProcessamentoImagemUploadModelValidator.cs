using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Messages;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Models;
using FluentValidation;

namespace FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Validator
{
    /// <summary>
    /// Regras de validação da model
    /// </summary>
    public class ProcessamentoImagemUploadModelValidator : AbstractValidator<ProcessamentoImagemUploadModel>
    {
        /// <summary>
        /// Contrutor das regras de validação da model
        /// </summary>
        public ProcessamentoImagemUploadModelValidator()
        {
            RuleFor(c => c.Data).NotEmpty().WithMessage(ValidationMessages.RequiredField);
            RuleFor(c => c.Usuario).NotEmpty().WithMessage(ValidationMessages.RequiredField);
            RuleFor(c => c.FormFile).NotEmpty().WithMessage(ValidationMessages.RequiredField);
            RuleFor(c => c.FormFile).NotNull().WithMessage(ValidationMessages.RequiredField);
            RuleFor(c => c.FormFile).Must(x => x.FileName.EndsWith(".mp4")).WithMessage($"{ValidationMessages.unknownFileFormat} {ValidationMessages.inputFile} .mp4 de até 500Mb");
        }
    }
}
