using Microsoft.EntityFrameworkCore;
using StockWebApi.Context;
using StockWebApi.Interfaces;
using StockWebApi.Models;

namespace StockWebApi.Repositories
{
    public class PortfolioStockRepository:IRepository<StockPortfolio>
    {
        private readonly DataContext _dataContext;
        public PortfolioStockRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<List<StockPortfolio>> GetAll()
        {
            var Stocks=await _dataContext.StocksPortfolios.ToListAsync();
            return Stocks;
        }
        public async Task Add(StockPortfolio Stock)
        {
            _dataContext.StocksPortfolios.Add(Stock);
            _dataContext.SaveChangesAsync();
        }
        public Task Update(StockPortfolio Stock)
        {
            _dataContext.Update(Stock);
        }
    }
}
