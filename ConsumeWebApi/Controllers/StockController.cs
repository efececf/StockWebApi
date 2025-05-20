using Microsoft.AspNetCore.Mvc;
using StockWebApi.Services;
using StockWebApi.Models;
using System.IdentityModel.Tokens.Jwt;
using StockWebApi.Interfaces;
using ConsumeWebApi.Models;
using System.Diagnostics;

namespace StockWebApi.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class StockController : Controller
    {
        public readonly StockService _stockService;
        public readonly PredictionService _predictionService;
        public readonly IStockPortfolioService _stockPortfolioService;
        public StockController(StockService stockService, IStockPortfolioService stockPortfolioService, PredictionService predictionService)
        {
            _stockService = stockService;
            _stockPortfolioService = stockPortfolioService;
            _predictionService = predictionService;
        }

        [HttpGet("Index/{searchString}")]
        public async Task<IActionResult> Index(string searchString)
        {

            string symbol = searchString;

            //string symbol = "AAPL";

            //string symbol = searchString;
            var stock = await _stockService.GetStock(symbol);
            if (stock == null)
            {
                var errormodel = new ErrorViewModel
                {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
                };
                // Null durumunda özel bir hata mesajı veya boş bir model döndürebilirsiniz
                return View("Error", errormodel); // veya bir hata mesajı gösterebiliriz
            }
            else
            {
                TempData["searchString"] = searchString;
                TempData.Keep("searchString");//bu sayede tempdata predition veya buythisstock içinde bir kere kullanılınca silinmicek ve diğer metodlar tarafından da kullanılacak
                ViewBag.SearchString = searchString;
                return View(stock);
            }
        }
        [HttpPost]
        public async Task<IActionResult> BuyThisStock([FromForm] int quantity)
        {
            Console.WriteLine("Formdan gelen quantity: " + quantity);

            var token = Request.Cookies["token"];
            if (token == null)
            {
                return RedirectToAction("Index", "Login");
            }
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            var userIdString = jwtToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value;
            Guid.TryParse(userIdString, out Guid userId);
            var symbol = TempData["searchString"] as string;
            if (string.IsNullOrEmpty(symbol))
            {
                TempData["ErrorMessage"] = "Stock symbol not found.";
                return RedirectToAction("Index", "Home");
            }
            await _stockPortfolioService.addStock(symbol, quantity, userId);
            TempData["SuccessMessage"] = "Successfully added to portfolio!";
            return RedirectToAction("Index", new { searchString = symbol });



        }
        [HttpGet("Prediction")]
        public async Task<IActionResult> Prediction(int year, int step)
        {
            // Önce TempData'dan almaya çalış
            var searchString = TempData["searchString"] as string;

            // AJAX'tan geliyorsa query string'ten al
            if (string.IsNullOrEmpty(searchString))
            {
                searchString = Request.Query["symbol"];
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                // Eğer AJAX ise JSON döndür
                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    var prediction = await _predictionService.GetPrediction(searchString, year, step);
                    return Json(prediction); // AJAX için JSON
                }

                // Normal view isteği için
                var predictionView = await _predictionService.GetPrediction(searchString, year, step);
                return View(predictionView);
            }
            else
            {
                return View("Error");
            }
        }

    }
}
