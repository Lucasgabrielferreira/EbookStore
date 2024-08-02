namespace EbookStore.Models
{
    public class ItemPedido : EntidadeBase
    { 
        public Guid PedidoId { get; set; }
        public Guid LivroId { get; set; }
        public int Quantidade { get; set; }
        public decimal Preco { get; set; }

        // Propriedades de navegação
        public virtual Pedido? Pedido { get; set; }
        public virtual Livro? Livro { get; set; }

        public ItemPedido() 
        {
        }

        public ItemPedido( Guid livroId, int quantidade, decimal preco, Pedido? pedido, Livro? livro)
        {
            LivroId = livroId;
            Quantidade = quantidade;
            Preco = preco;
            Pedido = pedido;
            Livro = livro;
        }
    }
}
