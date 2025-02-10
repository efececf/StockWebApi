namespace StockWebApi.Models
{
    public class StockPortfolio
    {
        public int Id { get; set; }
        public int PortfolioId { get; set; }
        public string StockName { get; set; }
        public decimal StockBuyingPrice { get; set; }
    }
}
