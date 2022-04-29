using Microsoft.EntityFrameworkCore;
using PlayStationClub.Areas.Services.Interfaces;
using PlayStationClub.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayStationClub.Areas.Services
{
    public class GenericService<T> : IService<T> where T : class
    {
        protected readonly PlayStationClubDbContext dbContext;
        public GenericService(PlayStationClubDbContext context)
        {
            dbContext = context;
        }
        public virtual async Task<T> CreateAsync(T entity)
        {
            await dbContext.Set<T>().AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            dbContext.Set<T>().Remove(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            var dbEntity = await dbContext.Set<T>().FindAsync(id);
            if (dbEntity != null) await DeleteAsync(dbEntity);
        }

        public virtual async Task<ICollection<T>> GetAllAsync()
        {
            return await dbContext.Set<T>().ToListAsync();
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await dbContext.Set<T>().FindAsync(id);
        }

        public virtual async Task UpdateAsync(T entity)
        {
            dbContext.Set<T>().Update(entity);
            await dbContext.SaveChangesAsync();
        }
    }
}
