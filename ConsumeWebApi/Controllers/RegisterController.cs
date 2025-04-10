using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StockWebApi.Interfaces;

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

        }
    }
}