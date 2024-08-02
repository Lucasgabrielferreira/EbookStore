using EbookStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EbookStore.Mappings
{
    public class AutorMapping : IEntityTypeConfiguration<Autor>
    {
        public void Configure(EntityTypeBuilder<Autor> builder)
        {
            builder.ToTable("Autores");
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Nome)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(a => a.Biografia)
                .IsRequired()
                .HasMaxLength(500);

        }
    }
}
