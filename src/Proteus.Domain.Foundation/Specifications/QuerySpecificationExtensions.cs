using System;
using System.Linq;
using System.Linq.Expressions;

namespace Proteus.Domain.Foundation.Specifications
{
    public static class QuerySpecificationExtensions
    {
        public static QuerySpecification<T> And<T>(this QuerySpecification<T> specification1,
                                                   QuerySpecification<T> specification2)
        {
            var adHocSpec1 = new AdHoc<T>(specification1.MatchingCriteria);
            var adHocSpec2 = new AdHoc<T>(specification2.MatchingCriteria);

            InvocationExpression invokedExpr = Expression.Invoke(adHocSpec2.MatchingCriteria,
                                                                 adHocSpec1.MatchingCriteria.Parameters.Cast<Expression>
                                                                     ());
            Expression<Func<T, bool>> dynamicClause = Expression.Lambda<Func<T, bool>>
                (Expression.AndAlso(adHocSpec1.MatchingCriteria.Body, invokedExpr),
                 adHocSpec1.MatchingCriteria.Parameters);

            return new AdHoc<T>(dynamicClause);
        }

        public static QuerySpecification<T> Or<T>(this QuerySpecification<T> specification1,
                                                  QuerySpecification<T> specification2)
        {
            var adHocSpec1 = new AdHoc<T>(specification1.MatchingCriteria);
            var adHocSpec2 = new AdHoc<T>(specification2.MatchingCriteria);

            InvocationExpression invokedExpr = Expression.Invoke(adHocSpec2.MatchingCriteria,
                                                                 adHocSpec1.MatchingCriteria.Parameters.Cast<Expression>
                                                                     ());
            Expression<Func<T, bool>> dynamicClause = Expression.Lambda<Func<T, bool>>
                (Expression.OrElse(adHocSpec1.MatchingCriteria.Body, invokedExpr),
                 adHocSpec1.MatchingCriteria.Parameters);

            return new AdHoc<T>(dynamicClause);
        }
    }
}