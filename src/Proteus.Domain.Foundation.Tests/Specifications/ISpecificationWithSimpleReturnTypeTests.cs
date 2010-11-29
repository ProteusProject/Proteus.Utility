using System.Linq;
using Proteus.Domain.Foundation.Tests.Specifications.Domain;

namespace Proteus.Domain.Foundation.Tests.Specifications
{
    public class ISpecificationWithSimpleReturnTypeTests : SpecificationTestFixture<User, string>
    {
        //TODO
        //#region Nested type: UserByNameSpecification

        //private class AllBlogNamesByAuthorNameSpecification : ISpecification<User, string>
        //{
        //    private string username;

        //    public IQueryable<string> SatisfyingElementsFrom(IQueryable<User> candidates)
        //    {
        //        return (from u in candidates where u.Username == username select (from g in u.Blogs select g.Name));
        //    }
        //}

        //#endregion
    }
}