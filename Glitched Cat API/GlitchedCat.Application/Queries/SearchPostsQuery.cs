using System;
using System.Collections.Generic;
using GlitchedCat.API.Models.Blog;
using MediatR;

namespace GlitchedCat.Application.Queries
{
    public class SearchPostsQuery : IRequest<IEnumerable<PostResponse>>
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}