using Microsoft.EntityFrameworkCore;
using AgroClimate.Models;

namespace AgroClimate.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Agricultor> AgricultoresSP { get; set; }
        public DbSet<Fazenda> FazendasSP { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Mapeamento da tabela de Agricultores
            modelBuilder.Entity<Agricultor>().ToTable("NomeDaTabelaRealNoBanco");

            // Configurando a relação muitos-para-muitos entre Agricultor e Fazenda
            modelBuilder.Entity<Agricultor>()
                .HasMany(a => a.Fazendas)
                .WithMany(f => f.Agricultores)
                .UsingEntity<Dictionary<string, object>>(
                    "AgricultorFazenda",
                    j => j
                        .HasOne<Fazenda>()
                        .WithMany()
                        .HasForeignKey("FazendaId"),
                    j => j
                        .HasOne<Agricultor>()
                        .WithMany()
                        .HasForeignKey("AgricultorId"),
                    j =>
                    {
                        j.HasKey("AgricultorId", "FazendaId"); // Define a chave primária
                    }
                );
        }
    }
}
