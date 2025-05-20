using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace StockWebApi.Services
{
    public class PredictionService
    {
        private readonly HttpClient _httpClient;
        public PredictionService(HttpClient httpClient){
            _httpClient = httpClient;
        }
        public async Task<Prediction> GetPrediction(string symbol,int? year=null,int? steps=null){
            try
            {
                string url;
                if (year.HasValue && steps.HasValue && year.Value==0 && steps.Value==0)
                    url = $"http://127.0.0.1:8000/predict/{symbol}";
                else
                    url = $"http://127.0.0.1:8000/predict/{symbol}/{year.Value}/{steps.Value}";
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

                var predictions = JsonConvert.DeserializeObject<Prediction>(jsonData);
                return predictions;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error fetching data: {ex.Message}");
                return null;
            }
        }
    }
}