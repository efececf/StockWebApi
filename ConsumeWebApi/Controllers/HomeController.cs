using Microsoft.AspNetCore.Mvc;
using StockWebApi.Services;
using StockWebApi.Controllers;
using StockWebApi.Models;

namespace StockWebApi.Controllers
{
    public class HomeController : Controller
    {
        private readonly StockService _stockService;
        private readonly NewsService _newsService;
        public HomeController(StockService stockService, NewsService newsService)
        {
            _stockService = stockService;
            _newsService = newsService;
        }
        [HttpPost]
        public IActionResult SearchStock(string searchString)
        {
            var stock = _stockService.GetStock(searchString);
            if (stock == null)
            {
                TempData["Error"] = "Hisse bulunamadı!";
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index", "Stock", new { searchString = searchString });
            }
        }
        public async Task<IActionResult> Index(string Category)
        {
            var news = await _newsService.GetNews(Category);
            var stocks=await _stockService.GetStockList();
            var orderedStocks=stocks.OrderBy(s=>s.symbol).ToList();
            var list=new List<decimal?>();
        //     foreach (var stock in orderedStocks)
        //     {
        //         var stockParticular = await _stockService.GetStock(stock.symbol);
        //         if (stockParticular != null)
        //         {
        //             var stockChange = stockParticular.dp;
        //             list.Add(stockChange);
        //         }
        //         else
        //         {
       
        //             list.Add(null); // Örneğin null değerini ekleyebilirsin
        //         }
        //     }
        //burda amaç anasayfada gözüken stocklistteki hisselerin günlük degişimini göstermekti ama her hisse için istek yolladığımız için anlık ve apide request limiti oldugu için bu kod uygulamayı çökertti 
            var model=new HomeViewModel{
                News = news,
                Stocks=orderedStocks,
                StockChanges=list
            };
            return View(model);
            
        }
    }
}
