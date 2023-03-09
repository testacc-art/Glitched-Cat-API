using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using GlitchedCat.Application.Commands;
using GlitchedCat.Application.Services;
using GlitchedCat.Domain.Entities;
using MediatR;

namespace GlitchedCat.Application.Handlers
{
    public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, Guid>
    {
        private readonly IMapper _mapper;
        private readonly IDomainService<Post> _postService;
        private readonly IDomainService<User> _userService;

        public CreatePostCommandHandler(IDomainService<Post> postService, IMapper mapper, IDomainService<User> userService)
        {
            _postService = postService;
            _mapper = mapper;
            _userService = userService;
        }

        public async Task<Guid> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            //TODO: Change it when implement auth
            #region ToChange

            var userId = (await _userService.GetFirstOrDefaultAsync()).Id;

            #endregion
            
            var post = _mapper.Map<Post>(request.PostRequest);
            post.UserId = userId;
            await _postService.AddAsync(post);
            return post.Id;
        }
    }
}