using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GlitchedCat.Domain.Entities;

namespace GlitchedCat.Application.Services;

public interface IDomainService<T> where T : BaseEntity
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByIdAsync(Guid id);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task RemoveAsync(T entity);
    Task<T> GetFirstOrDefaultAsync();
}