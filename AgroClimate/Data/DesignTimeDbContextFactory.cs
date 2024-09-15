using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AgroClimate.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

            // Configure a string de conexão diretamente aqui ou use uma alternativa de configuração
            optionsBuilder.UseOracle("Data Source=oracle.fiap.com.br:1521/orcl; User Id=rm550981; Password=200605;");

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
