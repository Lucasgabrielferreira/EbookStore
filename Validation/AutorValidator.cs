using EbookStore.Models;
using FluentValidation;

namespace EbookStore.Validation
{
    public class AutorValidator : AbstractValidator<Autor>
    {
        public AutorValidator()
        {
            RuleFor(autor => autor.Nome)
                .NotEmpty().WithMessage("O nome é obrigatório.")
                .Length(1, 50).WithMessage("O nome deve ter entre 1 e 50 caracteres.");

            RuleFor(autor => autor.Biografia)
              .NotEmpty().WithMessage("A biografia é obrigatória.")
              .Length(1, 500).WithMessage("A biografia deve ter entre 1 e 500 caracteres.");
        }
    }
}
