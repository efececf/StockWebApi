using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockWebApi.Interfaces
{
    public interface IPasswordHasher
    {
        public string Hash(string password);
        public bool VerifyPassword(string password,string inputPassword);
    }
}