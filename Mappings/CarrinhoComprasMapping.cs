﻿using EbookStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace EbookStore.Mappings
{
    public class CarrinhoComprasMapping : EntityBaseMapping<CarrinhoCompras>
    {
        public void Configure(EntityTypeBuilder<CarrinhoCompras> builder)
        {
            base.Configure(builder);

            builder.ToTable("CarrinhoCompras");

            builder.Property(cc => cc.Id)
               .IsRequired();

            builder.Property(cc => cc.ClienteId)
                .IsRequired();

            builder.HasOne(cc => cc.Cliente)
           .WithOne(c => c.CarrinhoCompras)
           .HasForeignKey<CarrinhoCompras>(cc => cc.ClienteId);
        }
    }
}
