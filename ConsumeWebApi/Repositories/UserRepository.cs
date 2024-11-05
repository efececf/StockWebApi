using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update.Internal;
using StockWebApi.Context;
using StockWebApi.Interfaces;
using StockWebApi.Models;

namespace StockWebApi.Repositories
{
    public class UserRepository:IRepository<User>
    {
        private readonly DataContext _context;
        public UserRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<List<User>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }
        public async Task<User> GetbyId(Guid id) 
        {
            return await _context.Users.FindAsync(id);
        }
        public async Task Add(User user) 
        {
            _context.Users.Add(user);
            _context.SaveChangesAsync();
        }
        public async Task Update(User user)
        {
            _context.Users.Update(user);
        }
        public async Task DeleteById(Guid id)
        {
            _context.Remove(id);

            _context.SaveChangesAsync();
        }

    }
}
