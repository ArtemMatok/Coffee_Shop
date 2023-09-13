using Coffee_Shop.Models;

namespace Coffee_Shop.Service.Interfaces
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetSmtById(int id);
        Task<T> GetById(int? id);
        bool Add(T entity);
        bool Update(T entity);  
        bool Delete(T entity);
        bool Save();

    }
}
