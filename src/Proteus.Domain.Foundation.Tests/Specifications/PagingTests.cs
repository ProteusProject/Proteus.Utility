using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Proteus.Domain.Foundation.Specifications;
using System.Linq.Expressions;


namespace Proteus.Domain.Foundation.Tests.Specifications
{
    [TestFixture]
    public class PagingTests
    {
        private IList<int> _numbers;

        [TestFixtureSetUp]
        public void _TestTestFixtureSetUp()
        {
            _numbers = new List<int>();

            for (int i = 0; i < 100; i++)
            {
                _numbers.Add(i);
            }

        }

        [Test]
        public void EvenNumbersSpecification_Returns_Only_Even_Members()
        {
            var spec = new EvenNumbersSpecification();

            var results = spec.SatisfyingElementsFrom(_numbers.AsQueryable());

            foreach (var item in results)
            {
                Assert.That(item % 2 == 0);
            }
        }

        [Test]
        public void Can_Page_Results()
        {
            var spec = new EvenNumbersSpecification();
            var results = spec.SatisfyingElementsFrom(_numbers.AsQueryable());

            Assert.That(results.Count(), Is.EqualTo(10));
        }

    }

    public class EvenNumbersSpecification : Specification<int>
    {

        public override Expression<Func<int, bool>> Predicate
        {
            get { return t => t % 2 == 0; }
        }

        public override int? NumberOfResults
        {
            get
            {
                return 10;
            }
        }
    }

}
