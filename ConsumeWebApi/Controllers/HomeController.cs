using Microsoft.AspNetCore.Mvc;
using StockWebApi.Services;
using StockWebApi.Controllers;

namespace StockWebApi.Controllers
{
    public class HomeController : Controller
    {
        private readonly StockService _stockService;
        public HomeController(StockService stockService)
        {
            _stockService = stockService;
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
        public IActionResult Index()
        {
           return View();
            
        }
    }
}
