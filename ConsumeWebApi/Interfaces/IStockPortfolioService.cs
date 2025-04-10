using StockWebApi.Models;
namespace StockWebApi.Interfaces
{
    public interface IStockPortfolioService
    {
        public Task addStock(String stock,int quantity,Guid portfolioId);
        public Task delStock(String stock,int quantity,Guid portfolioId);
        public Task<List<StockPortfolio>> ShowStocksOfUser(Guid id);
        //public Task showProfit();
    }
}
