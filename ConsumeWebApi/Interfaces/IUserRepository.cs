using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockWebApi.Models;

namespace StockWebApi.Interfaces
{
    public interface IUserRepository
    {
         Task<List<User>> GetAll();
        Task<User> GetbyId(Guid id);
        Task Add(User user);
        Task Update(User user);
        Task DeleteById(Guid id);
        Task<User> GetByUserName(string userName);

    }
}