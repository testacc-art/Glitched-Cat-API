using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using GlitchedCat.Application.Commands;
using GlitchedCat.Application.Services;
using GlitchedCat.Domain.Entities;
using GlitchedCat.Domain.Exceptions;
using MediatR;

namespace GlitchedCat.Application.Handlers
{
    public class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommand>
    {
        private readonly IMapper _mapper;
        private readonly IDomainService<Post> _postService;

        public UpdatePostCommandHandler(IDomainService<Post> postService, IMapper mapper)
        {
            _postService = postService;
            _mapper = mapper;
        }

        public async Task Handle(UpdatePostCommand request, CancellationToken cancellationToken)
        {
            var post = await _postService.GetByIdAsync(request.Id);
            if (post == null)
            {
                throw new PostNotFoundException(request.Id);
            }

            _mapper.Map(request, post);
            await _postService.UpdateAsync(post);
        }
    }
}