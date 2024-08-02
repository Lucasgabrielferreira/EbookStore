using System.ComponentModel.DataAnnotations;

namespace EbookStore.Models
{
    public class Cliente : EntidadeBase
    {
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public string? Senha { get; set; }

        // Propriedade de navegação
        public virtual CarrinhoCompras? CarrinhoCompras { get; set; }
        public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();

        public Cliente()
        {
        }

        public Cliente(string? nome, string? email, string? senha, CarrinhoCompras? carrinhoCompras, ICollection<Pedido> pedidos)
        {
            Nome = nome;
            Email = email;
            Senha = senha;
            CarrinhoCompras = carrinhoCompras;
            Pedidos = pedidos;
        }
    }
}
