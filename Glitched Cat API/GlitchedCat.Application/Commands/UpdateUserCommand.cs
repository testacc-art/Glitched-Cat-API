using MediatR;
using System;
using GlitchedCat.Domain.Common.Models.Blog;

namespace GlitchedCat.Application.Commands
{
    public class UpdateUserCommand : IRequest
    {
        public Guid Id { get; set; }
        public UserRequest UserRequest { get; set; }
    }
}