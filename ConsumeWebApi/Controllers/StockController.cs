using Microsoft.AspNetCore.Mvc;
using StockWebApi.Services;
using StockWebApi.Models;

namespace StockWebApi.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class StockController : Controller
    {
        public readonly StockService _stockService;
        public StockController(StockService stockService)
        {
            _stockService = stockService;
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

    }
}
