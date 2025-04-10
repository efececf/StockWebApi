using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockWebApi.Models;

namespace StockWebApi.Interfaces
{
    public interface IPortfolioStockRepository
    {
        Task<List<StockPortfolio>> GetAll();
        Task<List<StockPortfolio>> GetbyId(Guid id);
        Task Add(StockPortfolio portfoliostock);
        Task Update(StockPortfolio stockportfolio);
        Task DeleteById(Guid id);
    }
}