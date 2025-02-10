using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update.Internal;
using StockWebApi.Context;
using StockWebApi.Interfaces;
using StockWebApi.Models;

namespace StockWebApi.Repositories

{
    public class PortfolioRepository:IRepository<Portfolio>
    {
        private readonly DataContext _context;
        public PortfolioRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<List<Portfolio>> GetAll()
        {
            var portfolios= await _context.Portfolios.ToListAsync();
            return portfolios;
        }
        public async Task<Portfolio> GetbyId(Guid id)
        {
            return await _context.Portfolios.FindAsync(id);
        }
        public async Task Add(Portfolio port)
        {
            _context.Portfolios.Add(port);
            _context.SaveChangesAsync();
        }
        public async Task Update(Portfolio port)
        {
            _context.Portfolios.Update(port);
        }
        public async Task DeleteById(Guid id)
        {
            var port=await _context.Portfolios.FindAsync();
            _context.Portfolios.Remove(port);

            _context.SaveChangesAsync();
        }
    }
}
