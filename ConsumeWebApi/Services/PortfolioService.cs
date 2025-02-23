using StockWebApi.Interfaces;
using StockWebApi.Models;
using StockWebApi.Repositories;

namespace StockWebApi.Services
{
    public class PortfolioService:IPortfolioService
    {
        private StockService _stockService;
        private IRepository<Portfolio> _repo;
        public PortfolioService(StockService stockService,IRepository<Portfolio> repo) 
        {
            _stockService = stockService;
            _repo=repo;
        }
        public async Task createPortfolio(string name)
        {
            Portfolio portfolio = new Portfolio
            {
                Name = name
            };
            await _repo.Add(portfolio);
        }
        public async Task deletePortfolio(Guid id)
        {
            await _repo.DeleteById(id);
        }

    }
}
