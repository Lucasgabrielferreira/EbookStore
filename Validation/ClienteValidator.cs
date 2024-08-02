using EbookStore.Models;
using FluentValidation;

namespace EbookStore.Validation
{
    public class ClienteValidator : AbstractValidator<Cliente>
    {
        public ClienteValidator() 
        {
            RuleFor(cliente => cliente.Nome)
                .NotEmpty().WithMessage("O nome é obrigatório.")
                .Length(1, 50).WithMessage("O nome deve ter entre 1 e 50 caracteres.");

            RuleFor(cliente => cliente.Email)
                .NotEmpty().WithMessage("O e-mail é obrigatório.")
                .EmailAddress().WithMessage("O e-mail deve ser válido.");

            RuleFor(cliente => cliente.Senha)
                .NotEmpty().WithMessage("A senha é obrigatória.")
                .Length(6, 20).WithMessage("A senha deve ter entre 6 e 20 caracteres.");
        }
    }
}
