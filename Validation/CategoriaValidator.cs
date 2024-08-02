using EbookStore.Models;
using FluentValidation;
namespace EbookStore.Validation
{
    public class CategoriaValidator : AbstractValidator<Categoria>
    {
        public CategoriaValidator() 
        {
            RuleFor(categoria => categoria.Nome)
                .NotEmpty().WithMessage("O nome é obrigatório.")
                .Length(1, 50).WithMessage("O nome deve ter entre 1 e 50 caracteres.");
        }
    }
}
