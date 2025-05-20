using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StockWebApi.Interfaces;
using StockWebApi.Models.Register;

namespace StockWebApi.Controllers
{
    [Route("[controller]")]
    public class RegisterController : Controller
    {
        private readonly ILogger<RegisterController> _logger;
        private IRegisterService _registerService;
        private readonly IPortfolioService _portfolioService;

        public RegisterController(ILogger<RegisterController> logger,IRegisterService service,IPortfolioService portfolioService)
        {
            _logger = logger;
            _registerService = service;
            _portfolioService = portfolioService;
        }

        [HttpGet]
        public IActionResult Index(RegisterRequest request)
        {
            var token=Request.Cookies["token"];
            if (token != null){
                return RedirectToAction("Index","Home");
            }
            return View(request);
        }

        [HttpPost]
        public async Task<IActionResult> Register(string name,string username,string password,string userrole){
            if (string.IsNullOrEmpty(name)){
                ModelState.AddModelError("","Bütün Alanları doldurmak zorunludur!");
                return View();
            }
            else{
                var RegisterReq=new RegisterRequest{
                    Name = name,
                    Username=username,
                    Password=password,
                    UserRole=userrole
                };
                var RegisterResu=await _registerService.Register(RegisterReq);
                if(RegisterResu.IsRegistered==true){
                    //await _portfolioService.createPortfolio(username);
                    return RedirectToAction("Index","Home");
                }
                else{
                    ModelState.AddModelError("","Bu kullanıcı adı başkası tarafından kullanılıyor!");
                    return View("Index");
                }
            }
        }
    }
}