/*
 *
 * Proteus
 * Copyright (C) 2008, 2009, 2010
 * Stephen A. Bohlen
 * http://www.unhandled-exceptions.com
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
using MbUnit.Framework;
using Proteus.Domain.Foundation;

namespace Domain.Foundation.Tests
{
    [TestFixture]
    public class SpecificationTests
    {
        [Test]
        public void IsFalseWhenNotSatisfied()
        {
            Assert.IsFalse(new GreaterThanZeroSpecification().IsSatisfiedBy(-1));
        }

        [Test]
        public void IsTrueWhenSatisfied()
        {
            Assert.IsTrue(new GreaterThanZeroSpecification().IsSatisfiedBy(1));
        }

    }

    [TestFixture]
    public class CompositeSpecificationTests
    {
        [Test]
        public void ANDIsFALSEWhenBothNotSatisfied()
        {
            Assert.IsFalse(new GreaterThanZeroSpecification().And(new LessThanTenSpecification()).IsSatisfiedBy(20));
        }

        [Test]
        public void ANDIsTRUEWhenBothSatisfied()
        {
            Assert.IsTrue(new GreaterThanZeroSpecification().And(new LessThanTenSpecification()).IsSatisfiedBy(1));
        }

        [Test]
        public void ORisTRUEWhenOneSatisfied()
        {
            Assert.IsTrue(new GreaterThanZeroSpecification().Or(new LessThanTenSpecification()).IsSatisfiedBy(-1));
        }

        [Test]
        public void ORisFALSEWhenNeitherSatisfied()
        {
            Assert.IsFalse(new LessThanTwentySpecification().Or(new LessThanTenSpecification()).IsSatisfiedBy(50));
        }

        [Test]
        public void NOTisTRUEWhenSatisfied()
        {
            Assert.IsTrue(new LessThanTwentySpecification().Not().IsSatisfiedBy(50));
        }

        [Test]
        public void NOTisFALSEWhenNotSatisfied()
        {
            Assert.IsFalse(new LessThanTwentySpecification().Not().IsSatisfiedBy(10));
        }

    }


    internal class LessThanTwentySpecification : CompositeSpecification<int>
    {
        public override bool IsSatisfiedBy(int candidate)
        {
            return candidate < 20;
        }
    }


    internal class LessThanTenSpecification : CompositeSpecification<int>
    {
        public override bool IsSatisfiedBy(int candidate)
        {
            return candidate < 10;
        }
    }

    internal class GreaterThanZeroSpecification : CompositeSpecification<int>
    {
        public override bool IsSatisfiedBy(int candidate)
        {
            return candidate > 0;
        }

    }
}