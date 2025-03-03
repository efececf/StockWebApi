using Microsoft.EntityFrameworkCore;
using StockWebApi.Context;
using StockWebApi.Interfaces;
using StockWebApi.Models;

namespace StockWebApi.Repositories
{
    public class PortfolioStockRepository:IPortfolioStockRepository
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
        public async Task<StockPortfolio> GetbyId(Guid id){
            var Stock = await _dataContext.StocksPortfolios.FirstOrDefaultAsync(x => x.Id == id);
            return Stock;
        }
        public async Task DeleteById(Guid id){
            var Stock = await _dataContext.StocksPortfolios.FirstOrDefaultAsync(x => x.Id == id);
            _dataContext.StocksPortfolios.Remove(Stock);
            _dataContext.SaveChanges();
        }
        public async Task Add(StockPortfolio Stock)
        {
            _dataContext.StocksPortfolios.Add(Stock);
            _dataContext.SaveChangesAsync();
        }
        public async Task Update(StockPortfolio Stock)
        {
            _dataContext.Update(Stock);
        }
    }
}
