using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using GlitchedCat.Application.Commands;
using GlitchedCat.Application.Services;
using GlitchedCat.Domain.Entities;
using MediatR;

namespace GlitchedCat.Application.Handlers
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
    {
        private readonly IMapper _mapper;
        private readonly IDomainService<User> _userService;

        public CreateUserCommandHandler(IDomainService<User> userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<User>(request.UserRequest);
            await _userService.AddAsync(user);
            return user.Id;
        }
    }
}