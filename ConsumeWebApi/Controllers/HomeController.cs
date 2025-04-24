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
            return View(news);
            
        }
    }
}
