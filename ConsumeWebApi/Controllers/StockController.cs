using Microsoft.AspNetCore.Mvc;
using StockWebApi.Services;
using StockWebApi.Models;
using System.IdentityModel.Tokens.Jwt;
using StockWebApi.Interfaces;
namespace StockWebApi.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class StockController : Controller
    {
        public readonly StockService _stockService;
        public readonly PredictionService _predictionService;
        public readonly IStockPortfolioService _stockPortfolioService;
        public StockController(StockService stockService,IStockPortfolioService stockPortfolioService, PredictionService predictionService)
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
            var stock =await _stockService.GetStock(symbol);
            if (stock == null)
            {
                // Null durumunda özel bir hata mesajı veya boş bir model döndürebilirsiniz
                return View("Error"); // veya bir hata mesajı gösterebiliriz
            }
            else{
                TempData["searchString"] = searchString;
                TempData.Keep("searchString");//bu sayede tempdata predition veya buythisstock içinde bir kere kullanılınca silinmicek ve diğer metodlar tarafından da kullanılacak
                ViewBag.SearchString = searchString;
                return View(stock);
            }
        }
        [HttpPost]
        public async Task<IActionResult> BuyThisStock(int quantity){

            var token=Request.Cookies["token"];
            if (token == null){
                return RedirectToAction("Index","Login");
            }
            var handler= new JwtSecurityTokenHandler();
            var jwtToken=handler.ReadJwtToken(token);
            var userIdString=jwtToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value;
            Guid.TryParse(userIdString, out Guid userId);
            var symbol=TempData["searchString"] as string;
            await _stockPortfolioService.addStock(symbol, quantity, userId);
            return View("Index");



        }
        [HttpGet("Prediction")]
        public async Task<IActionResult> Prediction(){
            var searchString = TempData["searchString"] as string;
            if (!string.IsNullOrEmpty(searchString))
                {
                    var prediction = await _predictionService.GetPrediction(searchString);
                    return View(prediction);
                }
            else
                {
        // Eğer searchString boşsa veya yoksa uygun bir hata işlemi yapılabilir
                return View("Error");
                }
            }

        }
}
