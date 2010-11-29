using System.Linq;

namespace Proteus.Domain.Foundation.Specifications
{
    public interface ISpecification<T> : ISpecification<T, T>
    {
    }

    public interface ISpecification<T, TResult>
    {
        IQueryable<TResult> SatisfyingElementsFrom(IQueryable<T> candidates);
        bool IsSatisfiedBy(T candidate);
    }
}