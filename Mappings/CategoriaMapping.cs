using EbookStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EbookStore.Mappings
{
    public class CategoriaMapping : EntityBaseMapping<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            base.Configure(builder);

            builder.ToTable("Categorias");

            builder.Property(c => c.Nome)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
