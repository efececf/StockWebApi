using System;
using StockWebApi.Models.Login;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockWebApi.Models;

namespace StockWebApi.Interfaces
{
    public interface ILoginService
    {
        public Task<AuthToken> Login(LoginRequest request);
        
    }
}