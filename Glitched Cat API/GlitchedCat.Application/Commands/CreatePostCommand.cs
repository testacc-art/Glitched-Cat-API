using MediatR;
using System;
using GlitchedCat.Domain.Common.Models.Blog;

namespace GlitchedCat.Application.Commands
{
    public class CreatePostCommand : IRequest<Guid>
    {
        public PostRequest PostRequest { get; set; }
        public string UserId { get; set; }
    }
}