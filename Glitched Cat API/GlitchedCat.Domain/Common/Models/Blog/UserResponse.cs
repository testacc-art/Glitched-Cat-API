using System;

namespace GlitchedCat.Domain.Common.Models.Blog
{
    public class UserResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int TotalPosts { get; set; }
        public int TotalComments { get; set; }
    }
}