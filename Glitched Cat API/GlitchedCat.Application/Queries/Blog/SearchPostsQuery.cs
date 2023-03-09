using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using GlitchedCat.Domain.Common.Models.Blog;
using GlitchedCat.Domain.Entities;
using MediatR;

namespace GlitchedCat.Application.Queries.Blog
{
    public class SearchPostsQuery : IRequest<IEnumerable<PostResponse>>
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        
        //TODO: Think a way to make this Generic
        public static Expression<Func<Post, bool>> ToPredicate(SearchPostsQuery query)
        {
            var parameter = Expression.Parameter(typeof(Post), "x");
            var expressions = new List<Expression>();

            if (!string.IsNullOrEmpty(query.Title))
            {
                var titleProperty = typeof(Post).GetProperty("Title");
                var titleValue = Expression.Constant(query.Title);
                if (titleProperty != null)
                {
                    var titleExpression = Expression.Equal(Expression.Property(parameter, titleProperty), titleValue);
                    expressions.Add(titleExpression);
                }
            }

            if (!string.IsNullOrEmpty(query.Author))
            {
                var authorProperty = typeof(Post).GetProperty("Author");
                var authorValue = Expression.Constant(query.Author);
                if (authorProperty != null)
                {
                    var authorExpression = Expression.Equal(Expression.Property(parameter, authorProperty), authorValue);
                    expressions.Add(authorExpression);
                }
            }

            if (query.StartDate != null)
            {
                var dateProperty = typeof(Post).GetProperty("CreatedAt");
                var dateValue = Expression.Constant(query.StartDate.Value.Date, typeof(DateTime));
                if (dateProperty != null)
                {
                    var dateExpression = Expression.GreaterThanOrEqual(Expression.Property(parameter, dateProperty), dateValue);
                    expressions.Add(dateExpression);
                }
            }

            if (query.EndDate != null)
            {
                var dateProperty = typeof(Post).GetProperty("CreatedAt");
                var dateValue = Expression.Constant(query.EndDate.Value.Date.AddDays(1), typeof(DateTime));
                if (dateProperty != null)
                {
                    var dateExpression = Expression.LessThan(Expression.Property(parameter, dateProperty), dateValue);
                    expressions.Add(dateExpression);
                }
            }

            if (expressions.Count == 0)
            {
                return null;
            }

            var body = expressions.Aggregate(Expression.AndAlso);
            return Expression.Lambda<Func<Post, bool>>(body, parameter);
        }

    }
}