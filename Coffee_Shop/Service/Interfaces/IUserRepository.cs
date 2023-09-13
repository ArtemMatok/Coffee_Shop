using Coffee_Shop.Models;

namespace Coffee_Shop.Service.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByName(string name);
    }
}
