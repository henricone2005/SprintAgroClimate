using AgroClimate.Models;
using Microsoft.EntityFrameworkCore;

namespace AgroClimate.Data;

using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Agricultor> Agricultores { get; set; }
    public DbSet<Fazenda> Fazendas { get; set; }
    public DbSet<AgricultorFazenda> AgricultorFazendas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        
        modelBuilder.Entity<AgricultorFazenda>()
            .HasKey(af => new { af.AgricultorId, af.FazendaId });

        modelBuilder.Entity<AgricultorFazenda>()
            .HasOne(af => af.Agricultor)
            .WithMany(a => a.AgricultorFazendas)
            .HasForeignKey(af => af.AgricultorId);

        modelBuilder.Entity<AgricultorFazenda>()
            .HasOne(af => af.Fazenda)
            .WithMany(f => f.AgricultorFazendas)
            .HasForeignKey(af => af.FazendaId);
    }
}
