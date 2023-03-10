using MediatR;
using System;
using System.ComponentModel.DataAnnotations;

namespace GlitchedCat.Application.Commands
{
    public class UpdatePostCommand : IRequest
    {
        [Required]
        public Guid Id { get; set; }
        
        [Required]
        public string Title { get; set; }
        
        [Required]
        public string Content { get; set; }
        
        public string AuthorId { get; set; }
    }
}