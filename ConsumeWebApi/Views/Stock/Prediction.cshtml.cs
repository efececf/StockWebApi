using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace StockWebApi.Views.Stock
{
    public class Prediction : PageModel
    {
        private readonly ILogger<Prediction> _logger;

        public Prediction(ILogger<Prediction> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}