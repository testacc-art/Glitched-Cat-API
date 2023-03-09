// using System;
// using System.Collections.Generic;
// using System.Threading.Tasks;
// using GlitchedCat.Domain.Entities;
// using Microsoft.EntityFrameworkCore;
//
// namespace GlitchedCat.Infra.Data
// {
//     public interface IRepository<T> where T : BaseEntity
//     {
//         Task<IEnumerable<T>> GetAllAsync();
//         Task<T> GetByIdAsync(Guid id);
//         Task AddAsync(T entity);
//         Task UpdateAsync(T entity);
//         Task RemoveAsync(T entity);
//     }
//
//     public class Repository<T> : IRepository<T> where T : BaseEntity
//     {
//         private readonly BlogDbContext _dataContext;
//
//         public Repository(BlogDbContext dataContext)
//         {
//             _dataContext = dataContext;
//         }
//
//         public async Task<IEnumerable<T>> GetAllAsync()
//         {
//             return await _dataContext.Set<T>().ToListAsync();
//         }
//
//         public async Task<T> GetByIdAsync(Guid id)
//         {
//             return await _dataContext.Set<T>().SingleOrDefaultAsync(e => e.Id == id);
//         }
//
//         public async Task AddAsync(T entity)
//         {
//             await _dataContext.Set<T>().AddAsync(entity);
//             await _dataContext.SaveChangesAsync();
//         }
//
//         public async Task UpdateAsync(T entity)
//         {
//             _dataContext.Entry(entity).State = EntityState.Modified;
//             await _dataContext.SaveChangesAsync();
//         }
//
//         public async Task RemoveAsync(T entity)
//         {
//             _dataContext.Set<T>().Remove(entity);
//             await _dataContext.SaveChangesAsync();
//         }
//     }
// }