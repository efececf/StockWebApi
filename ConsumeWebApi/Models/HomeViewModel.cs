using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockWebApi.Models
{
    public class HomeViewModel
    {
        public List<News?> News { get; set; }
        public List<StockList?> Stocks { get; set; }
        public List <decimal?> StockChanges{ get; set; }
    }
}