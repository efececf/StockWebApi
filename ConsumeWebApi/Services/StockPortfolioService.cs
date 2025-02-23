using StockWebApi.Interfaces;
using StockWebApi.Models;
using StockWebApi.Repositories;

namespace StockWebApi.Services
{
    public class StockPortfolioService:IStockPortfolioService
    {
        private readonly StockService _stockService;
        private readonly PortfolioStockRepository _repo;
        public StockPortfolioService(StockService stockService,PortfolioStockRepository repo)
        {
            _stockService = stockService;
            _repo = repo;
        }
        public async Task addStock(String stockName, int quantity)
        {
            StockPortfolio mystock= new StockPortfolio{
                StockName = stockName,
                Quantity = quantity,
            };
            await _repo.Add(mystock);

        }

    }
}
