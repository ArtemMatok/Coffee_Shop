using Coffee_Shop.Data;
using Coffee_Shop.Models;
using Coffee_Shop.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Coffee_Shop.Service.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(User entity)
        {
            _context.Users.Add(entity);
            return Save();
        }

        public bool Delete(User entity)
        {
            _context.Users.Remove(entity);
            return Save();
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }

       

        public  async Task<User> GetById(int? id)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<User> GetByName(string name)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Name == name);
        }

        public async Task<User> GetSmtById(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(User entity)
        {
            _context.Users.Update(entity);
            return Save();
        }
    }
}
