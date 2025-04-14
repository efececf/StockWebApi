using StockWebApi.Services;
using StockWebApi.Models;
using StockWebApi.Interfaces;
using Microsoft.EntityFrameworkCore;
using StockWebApi.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using StockWebApi.Repositories;
using System.Text;

namespace StockWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            File.AppendAllText("startup.log", $"Main metodu başladı: {DateTime.Now}\n");

            try{
                var builder = WebApplication.CreateBuilder(args);
                builder.Logging.ClearProviders();
                builder.Logging.AddConsole();

                builder.Services.AddDbContext<DataContext>(options =>
                    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

                builder.Services.AddControllersWithViews();

                var apiKey = builder.Configuration["Finnhub:ApiKey"];
                builder.Services.AddHttpClient<StockService>(client =>
                {
                    client.BaseAddress = new Uri("https://finnhub.io/api/v1/");
                });
                builder.Services.AddScoped<StockService>(provider =>
                {
                    var httpClient = provider.GetRequiredService<IHttpClientFactory>().CreateClient();
                    return new StockService(httpClient, apiKey); // Burada apiKey doğru geçiliyor
                });

                builder.Services.AddScoped<ILoginService, LoginService>();
                builder.Services.AddScoped<IPortfolioService, PortfolioService>();
                builder.Services.AddScoped<IRegisterService, RegisterService>();
                builder.Services.AddScoped<IStockPortfolioService, StockPortfolioService>();
                builder.Services.AddScoped<IUserRepository, UserRepository>();
                builder.Services.AddScoped<IPortfolioRepository, PortfolioRepository>();
                builder.Services.AddScoped<IPortfolioStockRepository, PortfolioStockRepository>();
                builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
                builder.Services.AddScoped<ITokenService, TokenService>();

                builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = "http://localhost:5094",   // ✅ Token'ı üreten MVC Web App
                            ValidAudience = "http://localhost:5094", // ✅ Token'ı kullanan uygulama (MVC Web App)
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
            }
            catch(Exception ex){
                File.AppendAllText("error.log", $"HATA: {ex.Message}\nSTACK: {ex.StackTrace}\n");
                Console.WriteLine("Hata: " + ex.Message);
                Console.WriteLine("StackTrace: " + ex.StackTrace);
            }
            

        }
    }
}
