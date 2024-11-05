using System.Net.Http;
using StockWebApi.Models;
using System.Threading.Tasks;
//using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace StockWebApi.Services
{
    public class StockService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apikey;
        public StockService(HttpClient httpClient, string apikey)
        {
            _httpClient = httpClient;
            _apikey = apikey ?? throw new ArgumentNullException(nameof(apikey));
        }
        public async Task <Stock> GetStock(string symbol)
        {
            try
            {
                string url = $"https://finnhub.io/api/v1/quote?symbol={symbol}&token={_apikey}";
                HttpResponseMessage response =await  _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                HttpContent content = response.Content;
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"API Error: {response.StatusCode}");
                    return null; // veya uygun bir nesne dönebilirsiniz
                }



                
                
                
                string jsonData =await content.ReadAsStringAsync();
                Console.WriteLine($"JSON Response: {jsonData}");

                var stock = JsonConvert.DeserializeObject<Stock>(jsonData);
                return stock;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error fetching data: {ex.Message}");
                return null;
            }
            

        }
    }
}
