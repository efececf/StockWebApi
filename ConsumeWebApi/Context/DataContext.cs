using Microsoft.EntityFrameworkCore;
using StockWebApi.Models;
namespace StockWebApi.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<StockPortfolio> StocksPortfolios { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder){
            if(!optionBuilder.IsConfigured){
                optionBuilder.UseNpgsql("Host=localhost;Port=5432;Database=StockDb;Username=postgres;Password=Venividivici_19");
            }

        }
    


    }
}
