using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Specifications;

namespace Core.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        // id ile getiren metod
        Task<T> GetByIdAsync(int id);
        
        // hepsini liste olarak getiren metod
        Task<IReadOnlyList<T>> ListAllAsync();
        Task<T> GetEntityWithSpec(ISpecification<T> spec);
        Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);

        //kaç tane item olduğunu count eden metod
        Task<int> CountAsync(ISpecification<T> spec);
        
        // direkt olarak database'e kayıt edilmediği için async yapmaya gerek yok
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        
    }
}