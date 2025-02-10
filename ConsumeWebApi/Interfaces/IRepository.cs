namespace StockWebApi.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAll();
        Task<T> GetbyId(Guid id);
        Task Add(T entity);
        Task Update(T entity);
        Task DeleteById(Guid id);


    }
}
