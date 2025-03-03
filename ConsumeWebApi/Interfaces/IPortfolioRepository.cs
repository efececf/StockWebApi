using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockWebApi.Models;

namespace StockWebApi.Interfaces
{
    public interface IPortfolioRepository
    {
        Task<List<Portfolio>> GetAll();
        Task<Portfolio> GetbyId(Guid id);
        Task Add(Portfolio portfolio);
        Task Update(Portfolio portfolio);
        Task DeleteById(Guid id);

    }
}