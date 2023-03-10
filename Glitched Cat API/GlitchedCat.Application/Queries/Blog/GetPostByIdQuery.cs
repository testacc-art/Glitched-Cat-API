using System;
using GlitchedCat.Domain.Common.Models.Blog;
using MediatR;

namespace GlitchedCat.Application.Queries.Blog
{
    public class GetPostByIdQuery : IRequest<PostResponse>
    {
        public Guid Id { get; set; }
    }
}