using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GlitchedCat.Domain.Entities;
using GlitchedCat.Infra.Data;

namespace GlitchedCat.Application.Services
{
    public interface IDomainService<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(Guid id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task RemoveAsync(T entity);
    }

    public class PostService : IDomainService<Post>
    {
        private readonly IRepository<Post> _postRepository;

        public PostService(IRepository<Post> postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<IEnumerable<Post>> GetAllAsync()
        {
            return await _postRepository.GetAllAsync();
        }

        public async Task<Post> GetByIdAsync(Guid id)
        {
            return await _postRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Post entity)
        {
            await _postRepository.AddAsync(entity);
        }

        public async Task UpdateAsync(Post entity)
        {
            await _postRepository.UpdateAsync(entity);

        }

        public async Task RemoveAsync(Post entity)
        {
            await _postRepository.RemoveAsync(entity);
        }
    }
}