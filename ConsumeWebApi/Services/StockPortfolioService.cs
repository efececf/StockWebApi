using StockWebApi.Interfaces;
using System.Linq;
using StockWebApi.Models;
using StockWebApi.Repositories;

namespace StockWebApi.Services
{
    public class StockPortfolioService: IStockPortfolioService
    {
        private readonly StockService _stockService;
        private readonly IPortfolioStockRepository _repo;
        private readonly IPortfolioRepository _portfolioRepository;
        public StockPortfolioService(StockService stockService,IPortfolioStockRepository repo,IPortfolioRepository portfolioRepository)
        {
            _stockService = stockService;
            _repo = repo;
            _portfolioRepository = portfolioRepository;
        }
        ///hangi portfolyoyoa eklicen portfolioİd koy metodlara acilli
        public async Task addStock(String stockName, int quantity,Guid UserId)
        {   
            var stocks=await this.ShowStocksOfUser(UserId);
            if(stocks!=null){
                var stock = stocks.FirstOrDefault(x=>x.StockName==stockName);//burda firstordefaultasync kullanamadık çünkü bellekte sorgu yapıyo veritabanına gitmiyor 
                if(stock!=null){
                    stock.Quantity+=quantity;
                    await _repo.Update(stock);
                }
            }
            StockPortfolio mystock= new StockPortfolio{
                StockName = stockName,
                Quantity = quantity,
            };
            await _repo.Add(mystock);

        }
         public async Task delStock(String stockName, int quantity, Guid portfolioID)
        {
            var stocks= await _repo.GetAll();
            var myStock=stocks.FirstOrDefault(x=>x.PortfolioId==portfolioID&&x.StockName==stockName);
            if(myStock!=null&&myStock.Quantity>=0){
                myStock.Quantity-=quantity;
                await _repo.Update(myStock);
            }

        }
        public async Task<List<StockPortfolio>> ShowStocksOfUser(Guid userId)
            {
                var portfolio = await _portfolioRepository.GetbyId(userId);
                if (portfolio == null)
                    return null;

                var stockList = await _repo.GetbyId(portfolio.Id);
    
                if (stockList != null)
                    {
                        return stockList.Where(s => s.Quantity > 0).ToList();
                    }

                return null;
}



    }
}
