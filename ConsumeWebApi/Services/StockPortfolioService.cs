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
            var portfolio = await _portfolioRepository.GetbyId(UserId);
            var portfolioStocks=await this.ShowStocksOfUser(UserId);
            if(portfolioStocks!=null){
                var stock = portfolioStocks.FirstOrDefault(x=>x.StockName==stockName);//burda firstordefaultasync kullanamadık çünkü bellekte sorgu yapıyo veritabanına gitmiyor 
                if(stock!=null){
                    var stockPT=await _stockService.GetStock(stockName);
                    stock.Quantity+=quantity;
                    stock.StockBuyingPrice=(stock.StockBuyingPrice*stock.Quantity+stockPT.c*quantity)/(stock.Quantity+quantity);
                    await _repo.Update(stock);
                    var stockInPortfolio=portfolio.Stocks.FirstOrDefault(x=>x.StockName==stock.StockName);
                    stockInPortfolio.Quantity+=quantity;
                    portfolio.Stocks.Remove(stock);
                    portfolioStocks.Add(stockInPortfolio);
                    await _portfolioRepository.Update(portfolio);
                }
            }
            var stockP=await _stockService.GetStock(stockName);
            StockPortfolio mystock= new StockPortfolio{
                StockBuyingPrice=stockP.c,
                PortfolioId=portfolio.Id,
                StockName = stockName,
                Quantity = quantity,
            };
            await _repo.Add(mystock);
            portfolio.Stocks.Add(mystock);
            await _portfolioRepository.Update(portfolio);

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
