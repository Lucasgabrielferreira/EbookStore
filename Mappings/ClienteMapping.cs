using EbookStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EbookStore.Mappings
{
    public class ClienteMapping : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("Clientes");
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                 .IsRequired()
                 .HasMaxLength(50);

            builder.Property(c => c.Email)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(c => c.Senha)
                .IsRequired()
                .HasMaxLength(20);
        }
    }
}
