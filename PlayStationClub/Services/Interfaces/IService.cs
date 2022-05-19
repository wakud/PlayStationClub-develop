using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayStationClub.Areas.Services.Interfaces
{
    public interface IService<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<ICollection<T>> GetAllAsync();
        Task<T> CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteByIdAsync(int id);
        Task DeleteAsync(T entity);

    }
}
