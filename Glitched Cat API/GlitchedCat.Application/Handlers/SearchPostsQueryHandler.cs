using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using GlitchedCat.Application.Queries.Blog;
using GlitchedCat.Domain.Common.Models.Blog;
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
            var predicate = SearchPostsQuery.ToPredicate(request);
            var posts = await _postRepository.SearchAsync(predicate);
            var postResponses = _mapper.Map<IEnumerable<PostResponse>>(posts);
            return postResponses;
        }
    }
}