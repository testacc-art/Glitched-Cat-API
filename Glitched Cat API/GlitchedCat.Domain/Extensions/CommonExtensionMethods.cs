using System;
using System.Linq;
using System.Linq.Expressions;

namespace GlitchedCat.Domain.Extensions;

public static class CommonExtensionMethods
{
    public static Func<T, bool> ToPredicate<T>(this T entity)
    {
        var parameter = Expression.Parameter(typeof(T), "x");
        var expressions = typeof(T).GetProperties()
            .Select(property =>
            {
                var value = property.GetValue(entity, null);
                if (value == null) return null;
                var propertyExpression = Expression.Property(parameter, property.Name);
                var constantExpression = Expression.Constant(value, property.PropertyType);
                return Expression.Equal(propertyExpression, constantExpression);
            })
            .Where(expression => expression != null);

        var binaryExpressions = expressions as BinaryExpression[] ?? expressions.ToArray();
        if (!binaryExpressions.Any()) return null;
        var body = binaryExpressions.Aggregate<Expression>((current, expression) => current == null ? expression : Expression.AndAlso(current, expression));
        return Expression.Lambda<Func<T, bool>>(body, parameter).Compile();
    }

}