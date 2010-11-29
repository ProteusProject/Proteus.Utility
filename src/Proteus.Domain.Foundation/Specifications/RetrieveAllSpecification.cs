using System.Linq;

namespace Proteus.Domain.Foundation.Specifications
{
    public class RetrieveAllSpecification<T> : ISpecification<T>
    {
        #region ISpecification<T> Members

        public IQueryable<T> SatisfyingElementsFrom(IQueryable<T> candidates)
        {
            return from c in candidates select c;
        }

        public bool IsSatisfiedBy(T candidate)
        {
            return true;
        }
        #endregion


        
    }
}