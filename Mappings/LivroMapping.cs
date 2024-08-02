using EbookStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EbookStore.Mappings
{
    public class LivroMapping : IEntityTypeConfiguration<Livro>
    {
        public void Configure(EntityTypeBuilder<Livro> builder)
        {
            builder.ToTable("Livros");
            builder.HasKey(l => l.Id);

            builder.Property(l => l.Titulo)
                 .IsRequired()
                 .HasMaxLength(200);

            builder.Property(l => l.Descricao)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(l => l.Preco)
                .IsRequired();
               

            builder.HasOne(l => l.Autor)
                     .WithMany(a => a.Livros)
                     .HasForeignKey(l => l.AutorId)
                     .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(l => l.Categoria)
                .WithMany(c => c.Livros)
                .HasForeignKey(l => l.CategoriaId)
                .OnDelete(DeleteBehavior.Cascade);
        }
     
    }
}
