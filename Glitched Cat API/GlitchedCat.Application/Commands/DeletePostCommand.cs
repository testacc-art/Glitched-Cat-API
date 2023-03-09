using MediatR;
using System;
using System.ComponentModel.DataAnnotations;

namespace GlitchedCat.Application.Commands
{
    public class DeletePostCommand : IRequest
    {
        [Required]
        public Guid Id { get; set; }
    }
}