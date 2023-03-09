using System;

namespace GlitchedCat.Domain.Common.Models.Blog
{
    public class PostResponse
    {
        public Guid Id { get; set; }
        
        public string Title { get; set; }
        
        public string Content { get; set; }
        public string AuthorUsername { get; set; }
        public int TotalComments { get; set; }
    }
}