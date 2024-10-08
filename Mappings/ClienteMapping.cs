﻿using EbookStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EbookStore.Mappings
{
    public class ClienteMapping : EntityBaseMapping<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            base.Configure(builder);

            builder.ToTable("Clientes");
           
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
