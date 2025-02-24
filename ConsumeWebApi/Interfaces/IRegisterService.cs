using System;
using StockWebApi.Models.Register;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockWebApi.Interfaces
{
    public interface IRegisterService
    {
        public Task<RegisterResult> Register(RegisterRequest request);
    }
}