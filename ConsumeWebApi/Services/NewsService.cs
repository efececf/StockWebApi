using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace StockWebApi.Services
{
    public class NewsService
    {

        private readonly HttpClient _httpClient;
        private readonly string _apikey;
         public NewsService(HttpClient httpClient, string apikey)
        {
            _httpClient = httpClient;
            _apikey = apikey ?? throw new ArgumentNullException(nameof(apikey));
        }
        public async Task<List<News?>> GetNews(string Category){
            try
            {
                string url = $"https://finnhub.io/api/v1/news?category={Category}&token=cvudj6pr01qjg1394o7gcvudj6pr01qjg1394o80";
                HttpResponseMessage response =await  _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                HttpContent content = response.Content;
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"API Error: {response.StatusCode}");
                    return null; 
                }

                string jsonData =await content.ReadAsStringAsync();
                //Console.WriteLine($"JSON Response: {jsonData}");

                var news = JsonConvert.DeserializeObject<List<News>>(jsonData);
                return news;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error fetching data: {ex.Message}");
                return null;
            }
           
        }
        
    }
}