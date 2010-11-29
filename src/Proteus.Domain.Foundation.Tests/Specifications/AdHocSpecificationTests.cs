using MbUnit.Framework;
using Proteus.Domain.Foundation.Tests.Specifications.Domain;
using Proteus.Domain.Foundation.Tests.Specifications.Infrastructure;
using Proteus.Domain.Foundation.Specifications;

namespace Proteus.Domain.Foundation.Tests.Specifications
{
    public class AdHocSpecificationTests : SpecificationTestFixture<User>
    {
        private User user1;
        private User user2;
        private User user3;

        [SetUp]
        public void TestSetup()
        {
            Repository<User>.Clear();

            user1 = new User("user1");
            user2 = new User("user2");
            user3 = new User("user3");

            Repository<User>.Save(user1);
            Repository<User>.Save(user2);
            Repository<User>.Save(user3);
        }

        [Test]
        public void MatchesIfNameMatchesThroughRepository()
        {
            SpecificationDoesMatchViaRepository(user1, new AdHoc<User>(u => u.Username == "user1"));
            SpecificationDoesMatchViaRepository(user2, new AdHoc<User>(u => u.Username == "user2"));
            SpecificationDoesMatchViaRepository(user3, new AdHoc<User>(u => u.Username == "user3"));
        }

        [Test]
        public void MatchesIfNameMatches()
        {
            SpecificationDoesMatch(user1, new AdHoc<User>(u => u.Username == "user1"));
            SpecificationDoesMatch(user2, new AdHoc<User>(u => u.Username == "user2"));
            SpecificationDoesMatch(user3, new AdHoc<User>(u => u.Username == "user3"));
        }

        [Test]
        public void PartialMatchDoesNotMatchAny()
        {
            var specification = new AdHoc<User>(u => u.Username == "user");
            SpecificationDoesNotMatch(user1, specification);
            SpecificationDoesNotMatch(user2, specification);
            SpecificationDoesNotMatch(user3, specification);
        }

        [Test]
        public void PartialMatchDoesNotMatchAnyViaRepository()
        {
            SpecificationDoesNotMatchViaRepository(new AdHoc<User>(u => u.Username == "user"));
        }
    }
}