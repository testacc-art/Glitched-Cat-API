using System.ComponentModel.DataAnnotations;

namespace GlitchedCat.API.Models.Blog
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