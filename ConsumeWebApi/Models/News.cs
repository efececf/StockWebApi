using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockWebApi.Models
{
    public class News
    {
        public string Category { get; set; }
        public long Datetime { get; set; }
        public string Headline { get; set; }
        public int Id { get; set; }
        public string Image { get; set; }
        public string Related { get; set; }
        public string Source { get; set; }
        public string Summary { get; set; }
        public string Url { get; set; }
    }
}