using System;
using System.Linq;
using System.Linq.Expressions;

namespace Proteus.Domain.Foundation.Specifications
{
    public static class ExpressionExtensions
    {
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expression1,
                                                       Expression<Func<T, bool>> expression2)
        {
            InvocationExpression invokedExpr = Expression.Invoke(expression2, expression1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(expression1.Body, invokedExpr),
                                                    expression1.Parameters);
        }
    }
}