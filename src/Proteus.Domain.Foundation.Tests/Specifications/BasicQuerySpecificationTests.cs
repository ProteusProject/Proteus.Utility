using System;
using System.Linq.Expressions;
using MbUnit.Framework;
using Proteus.Domain.Foundation.Tests.Specifications.Domain;
using Proteus.Domain.Foundation.Tests.Specifications.Infrastructure;
using Proteus.Domain.Foundation.Specifications;

namespace Proteus.Domain.Foundation.Tests.Specifications
{
    public class BasicQuerySpecificationTests : SpecificationTestFixture<User>
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
            SpecificationDoesMatchViaRepository(user1, new UserByNameSpecification("user1"));
            SpecificationDoesMatchViaRepository(user2, new UserByNameSpecification("user2"));
            SpecificationDoesMatchViaRepository(user3, new UserByNameSpecification("user3"));
        }

        [Test]
        public void MatchesIfNameMatches()
        {
            SpecificationDoesMatch(user1, new UserByNameSpecification("user1"));
            SpecificationDoesMatch(user2, new UserByNameSpecification("user2"));
            SpecificationDoesMatch(user3, new UserByNameSpecification("user3"));
        }

        [Test]
        public void PartialMatchDoesNotMatchAny()
        {
            var specification = new UserByNameSpecification("user");
            SpecificationDoesNotMatch(user1, specification);
            SpecificationDoesNotMatch(user2, specification);
            SpecificationDoesNotMatch(user3, specification);
        }

        [Test]
        public void PartialMatchDoesNotMatchAnyViaRepository()
        {
            SpecificationDoesNotMatchViaRepository(new UserByNameSpecification("user"));
        }

        #region Nested type: UserByNameSpecification

        private class UserByNameSpecification : QuerySpecification<User>
        {
            private readonly string username;

            public UserByNameSpecification(string username)
            {
                this.username = username;
            }

            public override Expression<Func<User, bool>> MatchingCriteria
            {
                get { return u => u.Username == username; }
            }
        }

        #endregion
    }
}