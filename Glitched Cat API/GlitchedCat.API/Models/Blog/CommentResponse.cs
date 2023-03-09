using System;

namespace GlitchedCat.API.Models.Blog
{
    public class CommentResponse
    {
        public Guid Id { get; set; }
        
        public string Content { get; set; }
        public string AuthorUsername { get; set; }
    }
}