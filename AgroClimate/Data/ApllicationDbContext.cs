
using Microsoft.EntityFrameworkCore;
using AgroClimate.Models;

namespace AgroClimate.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Agricultor> Agricultores { get; set; }
        public DbSet<Fazenda> Fazendas { get; set; }
    

      protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurando a relação muitos-para-muitos entre Paciente e Plano
            modelBuilder.Entity<Agricultor>()
                .HasMany(p => p.Fazendas)
                .WithMany(p => p.Agricultores)
                .UsingEntity<Dictionary<string, object>>(
                    "AgricultorFazenda", // Nome da tabela de junção
                    j => j
                        .HasOne<Fazenda>()
                        .WithMany()
                        .HasForeignKey("FazendaId"), // Chave estrangeira
                    j => j
                        .HasOne<Agricultor>()
                        .WithMany()
                        .HasForeignKey("AgricultorId"), // Chave estrangeira
                    j =>
                    {
                        j.HasKey("AgricultorId", "FazendaId"); // Define a chave primária
                    }
                );
        }
    }
}