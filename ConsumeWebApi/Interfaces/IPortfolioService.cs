namespace StockWebApi.Interfaces
{
    public interface IPortfolioService
    {
        public Task createPortfolio(int name);
        public Task deletePortfolio(Guid id);

    }
}
