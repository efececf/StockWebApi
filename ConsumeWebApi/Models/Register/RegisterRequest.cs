using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockWebApi.Models.Register
{
    public class RegisterRequest
    {
        public string Username{ get; set; }
        public string Password{ get; set; }
        public string Name{ get; set; }
        
    }
}