namespace StockWebApi.Interfaces
{
    public interface IPortfolioService
    {
        public Task createPortfolio(string name);
        public Task deletePortfolio(Guid id);

    }
}
