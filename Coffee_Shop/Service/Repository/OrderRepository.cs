using Coffee_Shop.Data;
using Coffee_Shop.Models;
using Coffee_Shop.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Coffee_Shop.Service.Repository;

public class OrderRepository:IRepository<Order>
{
    private readonly ApplicationDbContext _context;

    public OrderRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Order>> GetAll()
    {
        return await _context.Orders.ToListAsync();
    }

    public async Task<Order> GetSmtById(int id)
    {
        return await _context.Orders.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Order> GetById(int? id)
    {
        return await _context.Orders.FirstOrDefaultAsync(x => x.Id == id);
    }

    public bool Add(Order entity)
    {
        _context.Orders.Add(entity);
        return Save();
    }

    public bool Update(Order entity)
    {
        _context.Orders.Update(entity);
        return Save();
    }

    public bool Delete(Order entity)
    {
        _context.Orders.Remove(entity);
        return Save();
    }

    public bool Save()
    {
        var saved = _context.SaveChanges();
        return saved > 0 ? true : false;
    }
}