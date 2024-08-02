namespace EbookStore.Models
{
    public class Pedido : EntidadeBase
    {
        public Guid ClienteId { get; set; }
        public DateTime DataPedido { get; set; }
        public decimal ValorTotal { get; set; }

        // Propriedade de navegação
        public virtual Cliente? Cliente { get; set; }
        public virtual ICollection<ItemPedido> ItensPedido { get; set; } = new List<ItemPedido>();

        public Pedido () 
        { 
        } 
        public Pedido(Guid clienteId, DateTime dataPedido, decimal valorTotal, Cliente? cliente, ICollection<ItemPedido> itensPedido)
        {
            ClienteId = clienteId;
            DataPedido = dataPedido;
            ValorTotal = valorTotal;
            Cliente = cliente;
            ItensPedido = itensPedido;
        }                                               
    }
}
