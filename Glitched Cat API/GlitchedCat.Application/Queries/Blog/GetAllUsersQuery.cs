using System.Collections.Generic;
using GlitchedCat.Domain.Common.Models.Blog;
using MediatR;

namespace GlitchedCat.Application.Queries.Blog
{
    public class GetAllUsersQuery : IRequest<IEnumerable<UserResponse>>
    {
    }
}