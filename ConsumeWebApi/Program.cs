using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using StockWebApi.Services;
using StockWebApi.Models;
using StockWebApi.Interfaces;
using Microsoft.EntityFrameworkCore;
using StockWebApi.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using StockWebApi.Repositories;
using System.Text;
using System.Diagnostics;


namespace StockWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
        

            try
            {
                var builder = WebApplication.CreateBuilder(args);

                builder.Logging.ClearProviders();
                builder.Logging.AddConsole();

                // DbContext
                builder.Services.AddDbContext<DataContext>(options =>
                    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

                builder.Services.AddControllersWithViews();

                // HttpClient ve StockService için ApiKey ile injection
                var apiKey = builder.Configuration["Finnhub:ApiKey"];

                builder.Services.AddHttpClient(); // IHttpClientFactory kullanılacak

                builder.Services.AddTransient<StockService>(sp =>
                {
                    var httpClientFactory = sp.GetRequiredService<IHttpClientFactory>();
                    var httpClient = httpClientFactory.CreateClient();
                    httpClient.BaseAddress = new Uri("https://finnhub.io/api/v1/");
                    return new StockService(httpClient, apiKey!);
                });
                builder.Services.AddTransient<NewsService>(sp =>
                {
                    var httpClientFactory = sp.GetRequiredService<IHttpClientFactory>();
                    var httpClient = httpClientFactory.CreateClient();
                    httpClient.BaseAddress = new Uri("https://finnhub.io/api/v1/");
                    return new NewsService(httpClient, apiKey!);
                });
                builder.Services.AddTransient<PredictionService>(sp =>
                {
                    var httpClientFactory = sp.GetRequiredService<IHttpClientFactory>();
                    var httpClient = httpClientFactory.CreateClient();
                    httpClient.BaseAddress = new Uri("http://127.0.0.1:8000");
                    return new PredictionService(httpClient);
                });

                // Dependency Injection
                builder.Services.AddScoped<ILoginService, LoginService>();
                builder.Services.AddScoped<IPortfolioService, PortfolioService>();
                builder.Services.AddScoped<IRegisterService, RegisterService>();
                builder.Services.AddScoped<IStockPortfolioService, StockPortfolioService>();
                builder.Services.AddScoped<IUserRepository, UserRepository>();
                builder.Services.AddScoped<IPortfolioRepository, PortfolioRepository>();
                builder.Services.AddScoped<IPortfolioStockRepository, PortfolioStockRepository>();
                builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
                builder.Services.AddScoped<ITokenService, TokenService>();

                // JWT Authentication
                builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = "http://localhost:5094",
                            ValidAudience = "http://localhost:5094",
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Venividivici_19"))
                        };
                    });

                builder.Services.AddAuthorization();

                var app = builder.Build();
                app.Logger.LogInformation("Uygulama başlatılıyor...");

                if (!app.Environment.IsDevelopment())
                {
                    app.UseExceptionHandler("/Home/Error");
                    app.UseHsts();
                }

                app.UseHttpsRedirection();
                app.UseStaticFiles();
                app.UseRouting();
                app.UseAuthentication();
                app.UseAuthorization();

                app.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                app.Logger.LogInformation("Uygulama başlatıldı 🚀");

                app.Run();
                System.Diagnostics.Process.Start(new ProcessStartInfo
                    {
                        FileName = "open",
                        Arguments = "http://localhost:5001", // senin adresin neyse
                        UseShellExecute = false
                    });

            }
            catch (Exception ex)
            {
                File.AppendAllText("error.log", $"HATA: {ex.Message}\nSTACK: {ex.StackTrace}\n");
                Console.WriteLine("Hata: " + ex.Message);
                Console.WriteLine("StackTrace: " + ex.StackTrace);
            }
        }
    }
}
