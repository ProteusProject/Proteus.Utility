/*
 *
 * Proteus
 * Copyright (C) 2008, 2009, 2010
 * Stephen A. Bohlen
 * http://www.unhandled-exceptions.com
 *
 * This library is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Lesser General Public
 * License as published by the Free Software Foundation; either
 * version 3.0 of the License, or (at your option) any later version.
 *
 * This library is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * Lesser General Public License for more details.
 *
 * You should have received a copy of the GNU Lesser General Public
 * License along with this library; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
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