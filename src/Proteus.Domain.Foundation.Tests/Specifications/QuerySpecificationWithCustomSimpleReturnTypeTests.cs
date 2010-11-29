using System;
using System.Linq.Expressions;
using MbUnit.Framework;
using Proteus.Domain.Foundation.Tests.Specifications.Domain;
using Proteus.Domain.Foundation.Tests.Specifications.Infrastructure;
using Proteus.Domain.Foundation.Specifications;

namespace Proteus.Domain.Foundation.Tests.Specifications
{
    public class QuerySpecificationWithCustomSimpleReturnTypeTests : SpecificationTestFixture<User, string>
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
        public void UserNameMatchesIfIdMatchesThroughRepository()
        {
            SpecificationDoesMatchViaRepository("user1", new UsernameByIdSpecification(user1.Id));
            SpecificationDoesMatchViaRepository("user2", new UsernameByIdSpecification(user2.Id));
            SpecificationDoesMatchViaRepository("user3", new UsernameByIdSpecification(user3.Id));
        }

        [Test]
        public void UserNameMatchesIfIdMatches()
        {
            SpecificationDoesMatch(user1, "user1", new UsernameByIdSpecification(user1.Id));
            SpecificationDoesMatch(user2, "user2", new UsernameByIdSpecification(user2.Id));
            SpecificationDoesMatch(user3, "user3", new UsernameByIdSpecification(user3.Id));
        }

        [Test]
        public void NonExistentIdDoesNotMatchAny()
        {
            var specification = new UsernameByIdSpecification(Guid.NewGuid());
            SpecificationDoesNotMatch(user1, specification);
            SpecificationDoesNotMatch(user2, specification);
            SpecificationDoesNotMatch(user3, specification);
        }

        [Test]
        public void NonExistentIdDoesNotMatchAnyViaRepository()
        {
            SpecificationDoesNotMatchViaRepository(new UsernameByIdSpecification(Guid.NewGuid()));
        }

        #region Nested type: UsernameByIdSpecification

        private class UsernameByIdSpecification : QuerySpecification<User, string>
        {
            private readonly Guid id;

            public UsernameByIdSpecification(Guid id)
            {
                this.id = id;
            }

            public override Converter<User, string> ResultMap
            {
                get { return u => u.Username; }
            }

            public override Expression<Func<User, bool>> MatchingCriteria
            {
                get { return u => u.Id == id; }
            }
        }

        #endregion
    }
}