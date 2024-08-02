using EbookStore.Models;
using FluentValidation;

namespace EbookStore.Validation
{
    public class LivroValidator : AbstractValidator<Livro>
    {
        public LivroValidator()
        {
            RuleFor(livro => livro.Titulo)
                .NotEmpty().WithMessage("O titulo é obrigatório.")
                .Length(1, 200).WithMessage("O titulo deve ter entre 1 e 200 caracteres.");

            RuleFor(livro => livro.Descricao)
                .NotEmpty().WithMessage("A descrição é obrigatório.")
                .Length(1, 200).WithMessage("A descrição deve ter entre 1 e 200 caracteres.");


            RuleFor(livro => livro.Preco)
                .NotEmpty().WithMessage("O preço é obrigatório.")
                .GreaterThan(0).WithMessage("O preço deve ser maior que zero.");

            RuleFor(livro => livro.CategoriaId)
               .NotEmpty().WithMessage("A categoria é obrigatória.");

            RuleFor(livro => livro.AutorId)
                .NotEmpty().WithMessage("O autor é obrigatório.");

        }
    }
}
