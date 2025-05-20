namespace StockWebApi.Models
{
    public class Portfolio
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Profit { get; set; }
        public Guid? UserId { get; set; }
        public List<StockPortfolio> Stocks { get; set; }
    }
}
