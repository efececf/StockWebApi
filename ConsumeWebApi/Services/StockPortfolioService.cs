using StockWebApi.Interfaces;
using StockWebApi.Models;
using StockWebApi.Repositories;

namespace StockWebApi.Services
{
    public class StockPortfolioService: IStockPortfolioService
    {
        private readonly StockService _stockService;
        private readonly IPortfolioStockRepository _repo;
        public StockPortfolioService(StockService stockService,IPortfolioStockRepository repo)
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
         public async Task delStock(String stockName, int quantity, Guid portfolioID)
        {
            var mystocks= async _repo.StockPortfolios.FindAsync(portfolioID);
            var theStock=mystocks.FirstOrDefault(x => x.StockName==stockName);
            theStock.Quantity-=quantity;
            await _repo.Update(theStock);

        }


    }
}
