using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using GlitchedCat.API.Models.Blog;
using GlitchedCat.Application.Queries;
using GlitchedCat.Domain.Entities;
using GlitchedCat.Infra.Data;
using MediatR;

namespace GlitchedCat.Application.Handlers
{
    public class SearchPostsQueryHandler : IRequestHandler<SearchPostsQuery, IEnumerable<PostResponse>>
    {
        private readonly IRepository<Post> _postRepository;
        private readonly IMapper _mapper;

        public SearchPostsQueryHandler(IRepository<Post> postRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PostResponse>> Handle(SearchPostsQuery request, CancellationToken cancellationToken)
        {
            var posts = await _postRepository.SearchAsync(request.Title, request.Author, request.StartDate, request.EndDate);
            var postResponses = _mapper.Map<IEnumerable<PostResponse>>(posts);
            return postResponses;
        }
    }
}