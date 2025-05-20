using StockWebApi.Interfaces;
using StockWebApi.Models;
using StockWebApi.Repositories;

namespace StockWebApi.Services
{
    public class PortfolioService:IPortfolioService
    {
        private StockService _stockService;
        private IPortfolioRepository _repo;
        public PortfolioService(StockService stockService,IPortfolioRepository repo) 
        {
            _stockService = stockService;
            _repo=repo;
        }
        // public async Task createPortfolio(string name)
        // {
        //     try{
        //         Portfolio portfolio = new Portfolio
        //     {
        //         Name = name
        //     };
        //     await _repo.Add(portfolio);
        //     }
        //     catch(Exception ex){
        //         Console.WriteLine(ex.Message);
        //         throw;
        //     }
            
        // }
        public async Task deletePortfolio(Guid id)
        {
            await _repo.DeleteById(id);
        }
        public async Task changePortfolioName(Guid id,string newName){
            await _repo.changeName(id,newName);
        }

    }
}
