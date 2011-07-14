/*
 *
 * Proteus
 * Copyright (c) 2008 - 2011
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
using NUnit.Framework;

namespace Proteus.Domain.Foundation.Tests
{
    [TestFixture]
    public class ValueObjectBaseTests
    {
        [Test]
        public void CanDeriveFromBaseClass()
        {
            Assert.IsTrue(new Address() is ValueObjectBase<Address>);
        }

        [Test]
        public void ObjectsWithAllDifferntPropertiesAreNotEqual()
        {
            var address1 = new Address() { BuildingNumber = 123, City = "New York", State = "NY", Street = "Main Street", UnitNumber = "6B" };
            var address2 = new Address() { BuildingNumber = 321, City = "York New", State = "YN", Street = "Street Main", UnitNumber = "B6" };

            Assert.AreNotEqual(address2, address1);
        }

        [Test]
        public void ObjectsWithSamePropertiesAreEqual()
        {
            var address1 = new Address() { BuildingNumber = 123, City = "New York", State = "NY", Street = "Main Street", UnitNumber = "6B" };
            var address2 = new Address() { BuildingNumber = address1.BuildingNumber, City = address1.City, State = address1.State, Street = address1.Street, UnitNumber = address1.UnitNumber };

            Assert.AreEqual(address1, address2);

        }

        [Test]
        public void ObjectsWithSingleDifferntPropertyAreNotEqual()
        {
            var address1 = new Address() { BuildingNumber = 123, City = "New York", State = "NY", Street = "Main Street", UnitNumber = "6B" };
            var address2 = new Address() { BuildingNumber = 321, City = address1.City, State = address1.State, Street = address1.Street, UnitNumber = address1.UnitNumber };

            Assert.AreNotEqual(address2, address1);
        }

    }
}
