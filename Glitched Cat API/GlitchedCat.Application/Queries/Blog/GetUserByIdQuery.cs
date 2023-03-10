using System;
using GlitchedCat.Domain.Common.Models.Blog;
using MediatR;

namespace GlitchedCat.Application.Queries.Blog;

public class GetUserByIdQuery : IRequest<UserResponse>
{
    public Guid Id { get; set; }
}