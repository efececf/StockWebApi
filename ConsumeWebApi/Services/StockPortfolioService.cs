using StockWebApi.Interfaces;
using StockWebApi.Models;

namespace StockWebApi.Services
{
    public class StockPortfolioService:IStockPortfolioService
    {
        private readonly StockService _stockService;
        private readonly IRepository<Portfolio> _repo;
        public StockPortfolioService(StockService stockService, IRepository<Portfolio> repo)
        {
            _stockService = stockService;
            _repo = repo;
        }
        public async Task addStock(Stock stock, int quantity)
        {

        }
    }
}
