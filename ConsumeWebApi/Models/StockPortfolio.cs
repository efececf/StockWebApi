namespace StockWebApi.Models
{
    public class StockPortfolio
    {
        public Guid Id { get; set; }
        public Guid PortfolioId { get; set; }
        public string StockName { get; set; }
        public decimal StockBuyingPrice { get; set; }
        public int Quantity { get; set; }
    }
}
