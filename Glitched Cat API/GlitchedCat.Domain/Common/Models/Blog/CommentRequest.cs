using System.ComponentModel.DataAnnotations;

namespace GlitchedCat.Domain.Common.Models.Blog
{
    public class CommentRequest
    {
        [Required]
        [MaxLength(500)]
        public string Content { get; set; }
    }
}