using System.ComponentModel.DataAnnotations;

namespace GlitchedCat.Domain.Common.Models.Blog
{
    public class UserRequest
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}