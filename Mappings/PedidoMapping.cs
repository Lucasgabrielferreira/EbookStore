using EbookStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EbookStore.Mappings
{
    public class PedidoMapping : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            builder.ToTable("Pedidos");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.ClienteId)
                .IsRequired();

            builder.Property(p => p.DataPedido)
                .IsRequired();

            builder.Property(p => p.ValorTotal)
                .IsRequired();
            
            builder.HasOne(p => p.Cliente)
                .WithMany(c => c.Pedidos)
                .HasForeignKey(p => p.ClienteId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
