using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StockWebApi.Interfaces;
using StockWebApi.Models;
using StockWebApi.Services;

namespace StockWebApi.Controllers
{
    [Route("[controller]")]
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;
        private  ILoginService _loginService;

        public LoginController(ILogger<LoginController> logger,ILoginService loginService)
        {
            _logger = logger;
            _loginService = loginService;
        }

        public IActionResult Index()
        {
            return View();
        }

      [HttpPost]
      public async Task<IActionResult> Login(string userName, string password,string? returnUrl=null){
            if (string.IsNullOrEmpty(userName)){
                ModelState.AddModelError("","Bütün Alanları doldurmak zorunludur!");
                return View();
            }
            else{
                var request=new LoginRequest{
                    UserName=userName,
                    Password=password
                };
                var authtoken= await _loginService.Login(request);
                if (authtoken!=null){//oluşan tokeni frontende yollar ve Response controller sınıfının alt üyesidir httpContext parçasıdır ve doğrudan erişilir 
                    Response.Cookies.Append("token",authtoken.Token,new CookieOptions{//cookie şeklinde frontende yollar.btw Httpcontext sadece controller veya midddlewareda kullanılır
                        HttpOnly=true,
                        Secure=true,
                        Expires=Expires = DateTimeOffset.UtcNow.AddHours(3)
                    });
                    //return RedirectToAction("Index","Home");
                    return LocalRedirect(returnUrl ?? Url.Action("Index", "Home"));
                }
                else {
                    ModelStateAddModelError("","Geçersiz kullanıcı adı veya şifre");
                    return View();
                }
                
            }
        }
      [HttpPost]
      public async IActionResult Logout(){
        Resposne.Cookies.Delete("token");//frontendde cookilerin içinde saklanan token isimli şeyi siler
        return Ok();
      }
    }
}