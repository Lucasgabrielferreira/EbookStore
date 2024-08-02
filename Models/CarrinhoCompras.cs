namespace EbookStore.Models
{
    public class CarrinhoCompras : EntidadeBase
    {
      
        public Guid ClienteId { get; set; }

        // Propriedades de navegação
        public virtual Cliente? Cliente { get; set; }
        public virtual ICollection<ItemPedido> ItensPedido { get; set; } = new List<ItemPedido>();

        public CarrinhoCompras() 
        {
        }    

        public CarrinhoCompras(Guid clienteId, Cliente? cliente, ICollection<ItemPedido> itensPedido)
        {
        
            ClienteId = clienteId;
            Cliente = cliente;
            ItensPedido = itensPedido;
        }
    }

}
