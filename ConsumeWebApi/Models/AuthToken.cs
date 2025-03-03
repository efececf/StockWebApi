using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockWebApi.Models
{
    public class AuthToken
    {
        public string Token { get; set; }
        public Guid UserId { get; set;}
        public string UserName { get; set;}
        public DateTime Expiration{ get; set; }
        public string UserRole{ get; set; }
    }
}