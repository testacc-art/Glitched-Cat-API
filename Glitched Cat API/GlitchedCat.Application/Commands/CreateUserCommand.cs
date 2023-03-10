using MediatR;
using System;
using GlitchedCat.Domain.Common.Models.Blog;

namespace GlitchedCat.Application.Commands
{
    public class CreateUserCommand : IRequest<Guid>
    {
        public UserRequest UserRequest { get; set; }
    }
}