using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Entities;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Messages;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.ValuesObject;
using FluentValidation;

namespace FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Validator
{
    /// <summary>
    /// Regras de validação da model
    /// </summary>
    public class PedidoValidator : AbstractValidator<ProcessamentoImagem>
    {
        /// <summary>
        /// Contrutor das regras de validação da model
        /// </summary>
        public PedidoValidator()
        {
            RuleFor(c => c.IdProcessamentoImagem).NotEmpty().WithMessage(ValidationMessages.RequiredField);
            RuleFor(c => c.Data).NotEmpty().WithMessage(ValidationMessages.RequiredField);
            RuleFor(c => c.Usuario).NotEmpty().WithMessage(ValidationMessages.RequiredField);
            RuleFor(c => c.DataEnvio).NotEmpty().WithMessage(ValidationMessages.RequiredField);
            RuleFor(c => c.NomeArquivo).NotEmpty().WithMessage(ValidationMessages.RequiredField);

        }
    }
}
