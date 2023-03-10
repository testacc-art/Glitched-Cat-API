using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GlitchedCat.Domain.Entities;
using GlitchedCat.Infra.Data;

namespace GlitchedCat.Application.Services
{
    public class UserService : IDomainService<User>
    {
        private readonly IRepository<User> _userRepository;

        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(User entity)
        {
            await _userRepository.AddAsync(entity);
        }

        public async Task UpdateAsync(User entity)
        {
            await _userRepository.UpdateAsync(entity);
        }

        public async Task RemoveAsync(User entity)
        {
            await _userRepository.RemoveAsync(entity);
        }
        
        public async Task<User> GetFirstOrDefaultAsync()
        {
            return await _userRepository.FirstOrDefaultAsync();
        }
    }
}