using EbookStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EbookStore.Mappings
{
    public abstract class EntityBaseMapping<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : EntidadeBase 
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.CreatedDate)
                .IsRequired();

            builder.Property(e => e.UpdatedDate)
                .IsRequired(false);


        }
    }
}
