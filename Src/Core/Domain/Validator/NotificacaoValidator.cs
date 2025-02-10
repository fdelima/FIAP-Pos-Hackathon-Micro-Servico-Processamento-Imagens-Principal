using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Entities;
using FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Messages;
using FluentValidation;

namespace FIAP.Pos.Hackathon.Micro.Servico.Processamento.Imagens.Principal.Domain.Validator
{
    /// <summary>
    /// Regras de validação da model
    /// </summary>
    public class NotificacaoValidator : AbstractValidator<Notificacao>
    {
        /// <summary>
        /// Contrutor das regras de validação da model
        /// </summary>
        public NotificacaoValidator()
        {
            RuleFor(c => c.Mensagem).NotEmpty().WithMessage(ValidationMessages.RequiredField);
        }
    }
}
