using System;
using System.Linq;
using System.Linq.Expressions;

namespace Proteus.Domain.Foundation.Specifications
{
    public abstract class Specification<T> : Specification<T, T>
    {
        public override Converter<T, T> ResultMap
        {
            get { return t => t; }
        }
    }

    public abstract class Specification<T, TResult> : QuerySpecification<T, TResult>
    {
        public virtual Expression<Func<T, object>> OrderingCriteria
        {
            get { return null; }
        }

        public virtual QueryOrder OrderingDirection
        {
            get { return QueryOrder.Ascending; }
        }

        public virtual int? NumberOfResults
        {
            get { return null; }
        }

        public override IQueryable<TResult> SatisfyingElementsFrom(IQueryable<T> candidates)
        {
            var result = candidates;

            if (MatchingCriteria != null)
                result = candidates.Where(MatchingCriteria);

            if (OrderingCriteria != null)
            {
                result = (OrderingDirection == QueryOrder.Ascending)
                             ? result.OrderBy(OrderingCriteria)
                             : result.OrderByDescending(OrderingCriteria);
            }

            if (NumberOfResults.HasValue)
                result = result.Take(NumberOfResults.Value);

            return result.Convert(ResultMap);
        }
    }
}