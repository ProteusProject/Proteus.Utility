using System.Linq;
using MbUnit.Framework;
using Proteus.Domain.Foundation.Tests.Specifications.Domain.Base;
using Proteus.Domain.Foundation.Tests.Specifications.Infrastructure;
using Proteus.Domain.Foundation.Specifications;

namespace Proteus.Domain.Foundation.Tests.Specifications
{
    public abstract class SpecificationTestFixture<T> : SpecificationTestFixture<T, T> where T : class, IEntity
    {
    }

    public abstract class SpecificationTestFixture<T, TResult> where T : class, IEntity
    {
        protected static void SpecificationDoesMatch(T instance, TResult expected,
                                                     ISpecification<T, TResult> specification)
        {
            IQueryable<TResult> matching = specification.SatisfyingElementsFrom(new[] {instance}.AsQueryable());
            Assert.AreEqual(1, matching.Count());
            Assert.AreEqual(expected, matching.First());
        }

        protected static void SpecificationDoesMatch(T instance, ISpecification<T, TResult> specification)
        {
            IQueryable<TResult> matching = specification.SatisfyingElementsFrom(new[] {instance}.AsQueryable());
            Assert.AreEqual(1, matching.Count());
            Assert.AreEqual(instance, matching.Cast<T>().First());
        }

        protected static void SpecificationDoesMatchViaRepository(TResult expected,
                                                                  ISpecification<T, TResult> specification)
        {
            IQueryable<TResult> matching = Repository<T>.FindAll(specification);
            Assert.AreEqual(1, matching.Count());
            Assert.AreEqual(expected, matching.First());
        }

        protected static void SpecificationDoesNotMatch(T instance, ISpecification<T, TResult> specification)
        {
            IQueryable<TResult> matching = specification.SatisfyingElementsFrom(new[] {instance}.AsQueryable());
            Assert.AreEqual(0, matching.Count());
        }

        protected static void SpecificationDoesNotMatchViaRepository(ISpecification<T, TResult> specification)
        {
            Assert.IsNull(Repository<T>.FindOne(specification));
        }
    }
}