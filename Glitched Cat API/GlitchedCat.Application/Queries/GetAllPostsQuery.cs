using System.Collections.Generic;
using GlitchedCat.API.Models.Blog;
using MediatR;

namespace GlitchedCat.Application.Queries
{
    public class GetAllPostsQuery : IRequest<IEnumerable<PostResponse>>
    {
    }
}