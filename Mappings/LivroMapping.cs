using EbookStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EbookStore.Mappings
{
    public class LivroMapping : EntityBaseMapping<Livro>
    {
        public void Configure(EntityTypeBuilder<Livro> builder)
        {
            base.Configure(builder);

            builder.ToTable("Livros");

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
