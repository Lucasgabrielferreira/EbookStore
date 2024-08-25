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
                .NotEmpty().WithMessage("A descrição é obrigatória.")
                .Length(1, 200).WithMessage("A descrição deve ter entre 1 e 200 caracteres.");

            RuleFor(livro => livro.Preco)
                .NotEmpty().WithMessage("O preço é obrigatório.")
                .GreaterThan(0).WithMessage("O preço deve ser maior que zero.");

            RuleFor(livro => livro.CategoriaId)
               .NotEmpty().WithMessage("A categoria é obrigatória.");

            RuleFor(livro => livro.AutorId)
                .NotEmpty().WithMessage("O autor é obrigatório.");

            RuleFor(livro => livro.ImagemUpload)
                .NotNull().WithMessage("A imagem é obrigatória ao criar um novo livro.")
                .When(livro => string.IsNullOrEmpty(livro.Imagem));

            RuleFor(livro => livro.ImagemUpload)
                .Must(img => img == null || img.Length > 0).WithMessage("O arquivo da imagem deve ser válido.")
                .When(livro => livro.ImagemUpload != null);

            RuleFor(livro => livro.Imagem)
                .Length(1, 300).WithMessage("O caminho da imagem deve ter entre 1 e 300 caracteres.")
                .When(livro => !string.IsNullOrEmpty(livro.ImagemUpload?.FileName));
        }
    }

}
