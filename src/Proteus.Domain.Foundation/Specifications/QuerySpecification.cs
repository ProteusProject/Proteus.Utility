using System;
using System.Linq;
using System.Linq.Expressions;

namespace Proteus.Domain.Foundation.Specifications
{
    public abstract class QuerySpecification<T> : QuerySpecification<T, T>
    {
        public override Converter<T, T> ResultMap
        {
            get { return t => t; }
        }
    }

    public abstract class QuerySpecification<T, TResult> : ISpecification<T, TResult>
    {
        public abstract Converter<T, TResult> ResultMap { get; }

        public virtual Expression<Func<T, bool>> MatchingCriteria
        {
            get { return t=> IsSatisfiedBy(t); }
        }

        #region ISpecification<T,TResult> Members

        public virtual IQueryable<TResult> SatisfyingElementsFrom(IQueryable<T> candidates)
        {
            if (MatchingCriteria != null)
                return candidates.Where(MatchingCriteria).ToList().ConvertAll(ResultMap).AsQueryable();

            return candidates.ToList().ConvertAll(ResultMap).AsQueryable();
        }

        public virtual bool IsSatisfiedBy(T candidate)
        {
            if (MatchingCriteria ==null)
                throw new InvalidOperationException("You cannot evaluate single candidate against a specification with a NULL MatchiingCriteria!");

            Func<T, bool> criteria = MatchingCriteria.Compile();

            return criteria(candidate);

        }

        #endregion


        
    }
}