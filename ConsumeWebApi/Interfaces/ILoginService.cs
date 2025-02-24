using System;
using StockWebApi.Models.Login;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockWebApi.Interfaces
{
    public interface ILoginService
    {
        public Task<LoginResult> Login(LoginRequest request);
        
    }
}