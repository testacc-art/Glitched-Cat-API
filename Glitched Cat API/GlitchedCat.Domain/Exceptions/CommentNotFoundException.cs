using System;

namespace GlitchedCat.Domain.Exceptions
{
    public class CommentNotFoundException : DomainException
    {
        public CommentNotFoundException(Guid commentId)
            : base($"Comment with id {commentId} was not found.")
        {
        }
    }
}