using System.Threading;
using System.Threading.Tasks;
using GlitchedCat.Application.Commands;
using GlitchedCat.Domain.Entities;
using GlitchedCat.Domain.Exceptions;
using GlitchedCat.Infra.Data;
using MediatR;

namespace GlitchedCat.Application.Handlers
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
    {
        private readonly IRepository<User> _userRepository;

        public DeleteUserCommandHandler(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.Id);

            if (user == null)
            {
                throw new NotFoundException(nameof(User), request.Id);
            }

            await _userRepository.DeleteAsync(user);
        }
    }
}