using System;
using System.Threading;
using System.Threading.Tasks;
using GlitchedCat.Application.Commands;
using GlitchedCat.Application.Services;
using GlitchedCat.Domain.Entities;
using GlitchedCat.Domain.Exceptions;
using MediatR;

namespace GlitchedCat.Application.Handlers
{
    public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand>
    {
        private readonly IDomainService<Post> _postService;

        public DeletePostCommandHandler(IDomainService<Post> postService)
        {
            _postService = postService;
        }

        public async Task Handle(DeletePostCommand request, CancellationToken cancellationToken)
        {
            var post = await _postService.GetByIdAsync(request.Id);
            if (post == null)
            {
                throw new PostNotFoundException(request.Id);
            }

            await _postService.RemoveAsync(post);
        }
    }
}