using StockWebApi.Models;
namespace StockWebApi.Interfaces
{
    public interface IStockPortfolioService
    {
        public Task addStock(Stock stock,int quantity);
        public Task delStock(Stock stock,int quantity);
        public Task showProfit();
    }
}
