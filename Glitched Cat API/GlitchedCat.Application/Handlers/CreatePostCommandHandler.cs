using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using GlitchedCat.Application.Commands;
using GlitchedCat.Domain.Entities;
using GlitchedCat.Domain.Services;
using MediatR;

namespace GlitchedCat.Application.Handlers
{
    public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, Guid>
    {
        private readonly IMapper _mapper;
        private readonly IDomainService<Post> _postService;

        public CreatePostCommandHandler(IDomainService<Post> postService, IMapper mapper)
        {
            _postService = postService;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            var post = _mapper.Map<Post>(request.PostRequest);
            post.UserId = new Guid(request.UserId);
            await _postService.AddAsync(post);
            return post.Id;
        }
    }
}