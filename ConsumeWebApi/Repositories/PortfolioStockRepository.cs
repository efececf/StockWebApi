using Microsoft.EntityFrameworkCore;
using StockWebApi.Context;
using StockWebApi.Interfaces;
using StockWebApi.Models;

namespace StockWebApi.Repositories
{
    public class PortfolioStockRepository: IPortfolioStockRepository
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
        public async Task<List<StockPortfolio>> GetbyId(Guid id){
            var Stocks = await _dataContext.StocksPortfolios.Where(x => x.PortfolioId == id).ToListAsync();
            return Stocks;
        }
        public async Task DeleteById(Guid id){
            var Stock = await _dataContext.StocksPortfolios.FirstOrDefaultAsync(x => x.Id == id);
            _dataContext.StocksPortfolios.Remove(Stock);
            await _dataContext.SaveChangesAsync();
        }
        public async Task Add(StockPortfolio Stock)
        {
            _dataContext.StocksPortfolios.Add(Stock);
            await _dataContext.SaveChangesAsync();
        }
        public async Task Update(StockPortfolio Stock)
        {
            _dataContext.Update(Stock);
            await _dataContext.SaveChangesAsync();
        }
    }
}
