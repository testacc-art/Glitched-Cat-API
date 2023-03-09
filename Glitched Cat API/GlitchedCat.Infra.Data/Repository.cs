using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using GlitchedCat.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GlitchedCat.Infra.Data
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync( Guid id );
        Task AddAsync( T entity );
        Task UpdateAsync( T entity );
        Task RemoveAsync( T entity );
        Task<IEnumerable<T>> SearchAsync(Expression<Func<T, bool>> predicate);
    }

    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly BlogContext _dataContext;

        public Repository( BlogContext dataContext )
        {
            _dataContext = dataContext;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dataContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync( Guid id )
        {
            return await _dataContext.Set<T>().SingleOrDefaultAsync(e => e.Id == id);
        }

        public async Task AddAsync( T entity )
        {
            await _dataContext.Set<T>().AddAsync(entity);
            await _dataContext.SaveChangesAsync();
        }

        public async Task UpdateAsync( T entity )
        {
            _dataContext.Entry(entity).State = EntityState.Modified;
            await _dataContext.SaveChangesAsync();
        }

        public async Task RemoveAsync( T entity )
        {
            _dataContext.Set<T>().Remove(entity);
            await _dataContext.SaveChangesAsync();
        }
        
        public async Task<IEnumerable<T>> SearchAsync(Expression<Func<T, bool>> predicate)
        {
            var entities = await _dataContext.Set<T>()
                .Where(predicate)
                .AsQueryable()
                .ToListAsync();
            return entities;
        }
    }
}