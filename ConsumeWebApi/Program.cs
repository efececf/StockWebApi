using StockWebApi.Services;

using StockWebApi.Models;
using Microsoft.EntityFrameworkCore;
using StockWebApi.Context;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DataContext>(opt => opt.UseIn("Database"));

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}



app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
