using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GlitchedCat.Domain.Entities;
using GlitchedCat.Infra.Data;

namespace GlitchedCat.Application.Services
{
    public class CommentService : IDomainService<Comment>
    {
        private readonly IRepository<Comment> _commentRepository;

        public CommentService(IRepository<Comment> commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task<IEnumerable<Comment>> GetAllAsync()
        {
            return await _commentRepository.GetAllAsync();
        }

        public async Task<Comment> GetByIdAsync(Guid id)
        {
            return await _commentRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Comment entity)
        {
            await _commentRepository.AddAsync(entity);
        }

        public async Task UpdateAsync(Comment entity)
        {
            await _commentRepository.UpdateAsync(entity);
        }

        public async Task RemoveAsync(Comment entity)
        {
            await _commentRepository.RemoveAsync(entity);
        }
        
        public async Task<Comment> GetFirstOrDefaultAsync()
        {
            return await _commentRepository.FirstOrDefaultAsync();
        }
    }
}