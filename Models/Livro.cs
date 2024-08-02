using System.ComponentModel.DataAnnotations;

namespace EbookStore.Models
{
    public class Livro : EntidadeBase
    {
        public string? Titulo { get; set; }
        public string? Descricao { get; set; }
        public decimal Preco { get; set; }

        // Chaves estrangeiras
        public Guid AutorId { get; set; }
        public Guid CategoriaId { get; set; }

        // Propriedades de navegação
        public virtual Autor? Autor { get; set; } 
        public virtual Categoria? Categoria { get; set; }

        public Livro()
        {
        }
        public Livro(string? titulo, string? descricao, decimal preco, Guid autorId, Guid categoriaId, Autor? autor, Categoria? categoria)
        {
            Titulo = titulo;
            Descricao = descricao;
            Preco = preco;
            AutorId = autorId;
            CategoriaId = categoriaId;
            Autor = autor;
            Categoria = categoria;
        }

    }
}
