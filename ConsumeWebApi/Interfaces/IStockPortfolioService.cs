using StockWebApi.Models;
namespace StockWebApi.Interfaces
{
    public interface IStockPortfolioService
    {
        public Task addStock(String stock,int quantity);
        public Task delStock(String stock,int quantity);
        //public Task showProfit();
    }
}
