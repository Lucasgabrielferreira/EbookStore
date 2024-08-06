using EbookStore.Mappings;
using EbookStore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class ContextoEbookStore : IdentityDbContext<IdentityUser>
{
    public ContextoEbookStore(DbContextOptions<ContextoEbookStore> options)
            : base(options)
    {
    }

    public DbSet<Livro> Livros { get; set; }
    public DbSet<Autor> Autores { get; set; }
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Pedido> Pedidos { get; set; }
    public DbSet<ItemPedido> ItensPedido { get; set; }
    public DbSet<CarrinhoCompras> CarrinhoCompras { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configurações de mapeamento das entidades
        modelBuilder.ApplyConfiguration(new AutorMapping());
        modelBuilder.ApplyConfiguration(new CategoriaMapping());
        modelBuilder.ApplyConfiguration(new ClienteMapping());
        modelBuilder.ApplyConfiguration(new ItemPedidoMapping());
        modelBuilder.ApplyConfiguration(new PedidoMapping());
        modelBuilder.ApplyConfiguration(new LivroMapping());
        modelBuilder.ApplyConfiguration(new CarrinhoComprasMapping());
    }
}
