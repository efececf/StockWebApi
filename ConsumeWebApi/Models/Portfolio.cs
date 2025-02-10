namespace StockWebApi.Models
{
    public class Portfolio
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Profit { get; set; }
        public List<StockPortfolio> Stocks { get; set; }
    }
}
