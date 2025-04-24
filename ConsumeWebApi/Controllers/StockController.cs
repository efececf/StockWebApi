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
        public readonly IStockPortfolioService _stockPortfolioService;
        public StockController(StockService stockService,IStockPortfolioService stockPortfolioService)
        {
            _stockService = stockService;
            _stockPortfolioService = stockPortfolioService;
        }

        [HttpGet]
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
            var symbol=Request.Query["searchString"].ToString();
            await _stockPortfolioService.addStock(symbol, quantity, userId);
            return View();



        }

    }
}
