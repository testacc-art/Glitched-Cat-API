using System.ComponentModel.DataAnnotations;

namespace GlitchedCat.Domain.Common.Models.Blog
{
    public class PostRequest
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        [MaxLength(500)]
        public string Content { get; set; }
    }
}