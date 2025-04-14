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

        public RegisterController(ILogger<RegisterController> logger,IRegisterService service)
        {
            _logger = logger;
            _registerService = service;
        }

        public IActionResult Index()
        {
            return View();
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
                };
                var RegisterResu=await _registerService.Register(RegisterReq);
                if(RegisterResu.IsRegistered==true){
                    return RedirectToAction("Index","Home");
                }
                else{
                    ModelState.AddModelError("","Bu kullanıcı adı başkası tarafından kullanılıyor!");
                    return View();
                }
            }
        }
    }
}