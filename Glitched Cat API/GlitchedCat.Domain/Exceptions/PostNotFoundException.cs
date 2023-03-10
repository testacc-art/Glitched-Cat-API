using System;

namespace GlitchedCat.Domain.Exceptions
{
    public class PostNotFoundException : DomainException
    {
        public PostNotFoundException(Guid postId)
            : base($"Post with id {postId} was not found.")
        {
        }
    }
}