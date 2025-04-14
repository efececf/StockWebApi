using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using StockWebApi.Models;
using StockWebApi.Services;
using StockWebApi.Interfaces;
{
    
}

namespace StockWebApi.Controllers
{
    [Route("[controller]")]
    public class PortfolioController : Controller
    {
        private readonly ILogger<PortfolioController> _logger;
        private readonly IPortfolioService _portfolioService;
        private readonly IStockPortfolioService _stockPortfolioService;

        public PortfolioController(ILogger<PortfolioController> logger,IPortfolioService portfolioService,IStockPortfolioService stockPortfolioService)
        {
            _logger = logger;
            _portfolioService = portfolioService;
            _stockPortfolioService = stockPortfolioService;
        }

        public async Task<IActionResult> Index()
        {
            var token=Request.Cookies["token"];
            if (token == null){
                return RedirectToAction("Index","Login");
            }
            var handler= new JwtSecurityTokenHandler();
            var jwtToken=handler.ReadJwtToken(token);
            //var userId = jwtToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value;
            //var stocks=_stockPortfolioService.ShowStocksOfUser(userId);
            //kod bu durumdayken hata alıyorum çünkü userid burada string ama showstockuser guid ile tanımlı, useridnin string olma sebebi token olustururken sub bütün claimler string olmalıydı sub kısmı yani id yi guidden stringe cevirmistik
            //simdi string userid yi geri guid yapmalıyız.
            var userIdString=jwtToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value;
            Guid.TryParse(userIdString, out Guid userId);
            var stocks=_stockPortfolioService.ShowStocksOfUser(userId);
            return View(stocks);
        }
        [HttpPut]
        public async Task<IActionResult> UpdatePortfolio(Guid id,[FromForm]int quantity,[FromForm]string stockName,[FromForm]int editType){
            var token=Request.Cookies["token"];
            var handler= new JwtSecurityTokenHandler();
            var jwtToken=handler.ReadJwtToken(token);
            var userIdString=jwtToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value;
            Guid.TryParse(userIdString, out Guid userId);
            if(editType == 1){
                await _stockPortfolioService.addStock(stockName,quantity,userId);
            }
            else if(editType == 2){
                await _stockPortfolioService.delStock(stockName,quantity,userId);
            }
            return View();

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}