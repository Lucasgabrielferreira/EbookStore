using System.ComponentModel.DataAnnotations;

namespace EbookStore.Models
{
    public class Autor : EntidadeBase
    {
        public string? Nome { get; set; }
        public string? Biografia { get; set; }

        // Propriedade de navegação
        public virtual ICollection<Livro> Livros { get; set; } = new List<Livro>();

        public Autor()
        {
        }

        public  Autor(string? nome, string? biografia, ICollection<Livro> livros)
        {
            Nome = nome;
            Biografia = biografia;
            Livros = livros;
        }
    }
}
