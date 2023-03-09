using System.ComponentModel.DataAnnotations;

namespace GlitchedCat.API.Models.Blog
{
    public class CommentRequest
    {
        [Required]
        [MaxLength(500)]
        public string Content { get; set; }
    }
}