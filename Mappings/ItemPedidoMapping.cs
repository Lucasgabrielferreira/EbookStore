﻿using EbookStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace EbookStore.Mappings
{
    public class ItemPedidoMapping : EntityBaseMapping<ItemPedido>
    {
        public void Configure(EntityTypeBuilder<ItemPedido> builder)
        {
            base.Configure(builder);

            builder.ToTable("ItensPedido");

            builder.Property(ip => ip.LivroId)
                .IsRequired();

            builder.Property(ip => ip.Quantidade)
                .IsRequired();

            builder.Property(ip => ip.Preco)
            .IsRequired();

            builder.HasOne(ip => ip.Pedido)
            .WithMany(p => p.ItensPedido)
            .HasForeignKey(ip => ip.PedidoId)
            .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ip => ip.Livro)
                .WithMany()
                .HasForeignKey(ip => ip.LivroId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
