using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockWebApi.Models
{
    public class Prediction
    {
    public string? stockSymbol { get; set; }
    public List<double>? predictedPrice { get; set; }
    }
}