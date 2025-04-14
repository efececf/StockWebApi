using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using StockWebApi.Context;

namespace StockWebApi
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=StockDb;Username=postgres;Password=Venividivici_19"); // Bağlantı dizesini buraya ekleyin

            return new DataContext(optionsBuilder.Options);
        }
    }
}
