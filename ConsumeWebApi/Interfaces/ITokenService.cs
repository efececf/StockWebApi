using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockWebApi.Models;

namespace StockWebApi.Interfaces
{
    public interface ITokenService
    {
        public Task<AuthToken> GenerateToken(User user); 
    }
}