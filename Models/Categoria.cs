
namespace EbookStore.Models
{
    public class Categoria : EntidadeBase
    {
        public string? Nome { get; set; }

        // Propriedade de navegação
        public ICollection<Livro> Livros { get; set; } = new List<Livro>();

        public Categoria()
        {
        }

        public Categoria(string? nome, ICollection<Livro> livros)
        {
            Nome = nome;
            Livros = livros;
        }
    }
}
