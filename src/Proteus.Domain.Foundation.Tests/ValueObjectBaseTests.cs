using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MbUnit.Framework;

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
