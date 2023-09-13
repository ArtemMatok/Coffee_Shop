using Coffee_Shop.Data;
using Coffee_Shop.Models;
using Coffee_Shop.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Coffee_Shop.Service.Repository
{
    public class ProductRepository : IRepository<Product>
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(Product entity)
        {
            _context.Products.Add(entity);
            return Save();
        }

        public bool Delete(Product entity)
        {
            _context.Products.Remove(entity);
            return Save();
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetById(int? id)
        {
            return await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Product> GetSmtById(int id)
        {
            return await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Product entity)
        {
            _context.Products.Update(entity);
            return Save();
        }
    }
}
