using System;
using GlitchedCat.API.Models.Blog;
using MediatR;

namespace GlitchedCat.Application.Queries
{
    public class GetPostByIdQuery : IRequest<PostResponse>
    {
        public Guid Id { get; set; }
    }
}