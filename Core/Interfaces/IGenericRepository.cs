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
        
    }
}