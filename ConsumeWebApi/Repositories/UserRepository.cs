using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update.Internal;
using Microsoft.VisualBasic;
using StockWebApi.Context;
using StockWebApi.Interfaces;
using StockWebApi.Models;

namespace StockWebApi.Repositories
{
    public class UserRepository: IUserRepository
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
            var user=await _context.Users.FindAsync(id);
            return user;
        }
        public async Task Add(User user) 
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }
        public async Task Update(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteById(Guid id)
        {
            _context.Remove(id);

            await _context.SaveChangesAsync();
        }
        public async Task<User> GetByUserName(string userName){
            var user=await _context.Users.FirstOrDefaultAsync(x => x.UserName == userName);
            return user;
        }

    }
}
