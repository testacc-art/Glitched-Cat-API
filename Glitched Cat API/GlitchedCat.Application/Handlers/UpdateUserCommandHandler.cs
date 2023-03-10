using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using GlitchedCat.Application.Commands;
using GlitchedCat.Application.Services;
using GlitchedCat.Domain.Entities;
using GlitchedCat.Domain.Exceptions;
using MediatR;

namespace GlitchedCat.Application.Handlers
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
    {
        private readonly IMapper _mapper;
        private readonly IDomainService<User> _userService;

        public UpdateUserCommandHandler(IDomainService<User> userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userService.GetByIdAsync(request.Id);
            if (user == null)
            {
                throw new NotFoundException(nameof(User), request.Id);
            }

            _mapper.Map(request, user);
            await _userService.UpdateAsync(user);
        }
    }
}