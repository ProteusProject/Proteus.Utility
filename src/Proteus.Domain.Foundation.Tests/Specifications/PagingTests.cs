/*
 *
 * Proteus
 * Copyright (C) 2010, 2011
 * Stephen A. Bohlen
 * http://www.unhandled-exceptions.com
 *
 * Portions Copyright (C) 2008
 * Steve Burman
 * Linq.Specifications http://code.google.com/p/linq-specifications/
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * 
 *     http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 * 
 */

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
