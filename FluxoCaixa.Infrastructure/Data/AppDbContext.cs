using Microsoft.EntityFrameworkCore;
using FluxoCaixa.Domain.Entities;

namespace FluxoCaixa.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public DbSet<Lancamento> Lancamentos => Set<Lancamento>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Lancamento>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Data).IsRequired();
            entity.Property(e => e.Valor).IsRequired().HasPrecision(18, 2);
            entity.Property(e => e.Tipo).IsRequired();
        });

        base.OnModelCreating(modelBuilder);
    }
}
