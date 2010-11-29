using System.Linq;
using Proteus.Domain.Foundation.Tests.Specifications.Domain;
using Proteus.Domain.Foundation.Specifications;

namespace Proteus.Domain.Foundation.Tests.Specifications
{
    public class BasicISpecificationTests : SpecificationTestFixture<User>

    {
        #region Nested type: UserByNameSpecification

        private class UserByNameSpecification : ISpecification<User>
        {
            private readonly string username;

            public UserByNameSpecification(string username)
            {
                this.username = username;
            }

            public IQueryable<User> SatisfyingElementsFrom(IQueryable<User> candidates)
            {
                //return from u in candidates where u.Username == username select u;
                return from u in candidates where IsSatisfiedBy(u) select u;
            }


            public bool IsSatisfiedBy(User candidate)
            {
                return candidate.Username == username;
            }
        }

        #endregion
    }
}