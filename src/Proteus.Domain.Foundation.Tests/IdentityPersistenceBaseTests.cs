/*
 *
 * Proteus
 * Copyright (C) 2008, 2009
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
using Proteus.Utility.UnitTest;
using System.Reflection;
using System.Diagnostics;

namespace Domain.Foundation.Tests
{
    [TestFixture]
    public class IdentityPersistenceBaseTests : UnitTestBase
    {
        //TODO: figure out why this test ALWAYS succeeds even if not all of the props are indeed virtual in the base-class
        [Test]
        public void AllPublicPropertiesAreDeclaredVirtual()
        {
            ClassUsingGuidIdentity p = new ClassUsingGuidIdentity();

            foreach (PropertyInfo prop in p.GetType().BaseType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                foreach (MethodInfo methodInfo in prop.GetAccessors())
                {
                    if ((methodInfo.Attributes & MethodAttributes.Virtual) != MethodAttributes.Virtual)
                    {
                        Assert.Fail(string.Format("Property {0} must be declared virtual in type {1}", prop.Name, p.GetType().BaseType.ToString()));
                    }
                }

            }

        }

        [Test]
        public void CanDeriveFromBaseClass()
        {
            Assert.IsTrue(new ClassUsingGuidIdentity() is IdentityPersistenceBase<ClassUsingGuidIdentity, Guid>);
        }

        [Test]
        public void DoublePersistedInstanceIsNotTransient()
        {
            ClassUsingIntIdentity p = new ClassUsingIntIdentity();

            //use reflection to set the internal ID of the object to 'simulate' persistence
            SetInstanceFieldValue(p, "_persistenceId", 1);

            Assert.IsFalse(p.IsTransient);

        }

        [Test]
        public void EqualWhenInstancesHaveSameIdentityValue()
        {
            ClassUsingGuidIdentity p1 = new ClassUsingGuidIdentity();
            ClassUsingGuidIdentity p2 = new ClassUsingGuidIdentity();

            Guid g = Guid.NewGuid();

            SetInstanceFieldValue(p1, "_persistenceId", g);
            SetInstanceFieldValue(p2, "_persistenceId", g);

            Assert.IsTrue(p1.Equals(p2));
        }

        [Test]
        public void HashCodesAreEqualIfIdentitiesAreEqual()
        {
            ClassUsingGuidIdentity p1 = new ClassUsingGuidIdentity();
            ClassUsingGuidIdentity p2 = new ClassUsingGuidIdentity();

            Guid g = Guid.NewGuid();

            SetInstanceFieldValue(p1, "_persistenceId", g);
            SetInstanceFieldValue(p2, "_persistenceId", g);

            Assert.IsTrue(p1.GetHashCode() == p2.GetHashCode());
        }

        [Test]
        public void NewInstanceIsTransient()
        {
            ClassUsingGuidIdentity p = new ClassUsingGuidIdentity();

            Assert.IsTrue(p.IsTransient);

        }

        [Test]
        public void NotEqualWhenBothInstancesAreTransient()
        {
            ClassUsingGuidIdentity p1 = new ClassUsingGuidIdentity();
            ClassUsingGuidIdentity p2 = new ClassUsingGuidIdentity();

            Assert.IsFalse(p1.Equals(p2));
        }

        [Test]
        public void NotEqualWhenFirstInstanceIsTransient()
        {
            ClassUsingGuidIdentity p1 = new ClassUsingGuidIdentity();
            ClassUsingGuidIdentity p2 = new ClassUsingGuidIdentity();

            SetInstanceFieldValue(p1, "_persistenceId", Guid.NewGuid());

            Assert.IsFalse(p1.Equals(p2));
        }

        [Test]
        public void NotEqualWhenInstancesAreDifferentTypes()
        {
            ClassUsingGuidIdentity p1 = new ClassUsingGuidIdentity();
            int i = 1;

            Assert.IsFalse(p1.Equals(i));
        }

        [Test]
        public void NotEqualWhenSecondInstanceIsNull()
        {
            ClassUsingGuidIdentity p1 = new ClassUsingGuidIdentity();
            ClassUsingGuidIdentity p2 = null;

            Assert.IsFalse(p1.Equals(p2));
        }

        [Test]
        public void NotEqualWhenSecondInstanceIsTransient()
        {
            ClassUsingGuidIdentity p1 = new ClassUsingGuidIdentity();
            ClassUsingGuidIdentity p2 = new ClassUsingGuidIdentity();

            SetInstanceFieldValue(p2, "_persistenceId", Guid.NewGuid());

            Assert.IsFalse(p1.Equals(p2));
        }

        [Test]
        public void OperatorEqualOverloadTest()
        {
            ClassUsingGuidIdentity p1 = new ClassUsingGuidIdentity();
            ClassUsingGuidIdentity p2 = new ClassUsingGuidIdentity();

            Assert.IsFalse(p1.Equals(p2));

            Assert.IsFalse(p1 == p2);
            Assert.IsTrue(p1 != p2);
        }

        [Test]
        public void PersistedInstanceIsNotTransientWithDoubleIdentity()
        {
            ClassUsingDoubleIdentity p = new ClassUsingDoubleIdentity();

            //use reflection to set the internal ID of the object to 'simulate' persistence
            SetInstanceFieldValue(p, "_persistenceId", 1d);

            Assert.IsFalse(p.IsTransient);

        }

        [Test]
        public void PersistedInstanceIsNotTransientWithGuidIdentity()
        {
            ClassUsingGuidIdentity p = new ClassUsingGuidIdentity();

            //use reflection to set the internal ID of the object to 'simulate' persistence
            SetInstanceFieldValue(p, "_persistenceId", Guid.NewGuid());

            Assert.IsFalse(p.IsTransient);

        }

        [Test]
        public void PersistedInstanceIsNotTransientWithIntIdentity()
        {
            ClassUsingIntIdentity p = new ClassUsingIntIdentity();

            //use reflection to set the internal ID of the object to 'simulate' persistence
            SetInstanceFieldValue(p, "_persistenceId", 1);

            Assert.IsFalse(p.IsTransient);

        }

        [Test]
        public void ThrowsWhenIdentityTypeIsUnsupported()
        {
            ClassUsingIntentionallyInvalidIdentity p = new ClassUsingIntentionallyInvalidIdentity();

            //use reflection to set the internal ID of the object to 'simulate' persistence
            SetInstanceFieldValue(p, "_persistenceId", new IntentionallyInvalidIdentityType());

            try
            {
                Assert.IsFalse(p.IsTransient);
                Assert.Fail("Expected ArgumentException thrown but didn't get it!");
            }
            catch (ArgumentException)
            {
                //do nothing, just swallow the expected exception
            }

        }

    }

    internal class ClassUsingDoubleIdentity : IdentityPersistenceBase<ClassUsingDoubleIdentity, double> { }

    internal class ClassUsingGuidIdentity : IdentityPersistenceBase<ClassUsingGuidIdentity, Guid>
    {
        private string _nonVirtualProp;
        public string NonVirtualProp
        {
            get { return _nonVirtualProp; }
            set
            {
                _nonVirtualProp = value;
            }
        }

    }

    internal class ClassUsingIntIdentity : IdentityPersistenceBase<ClassUsingIntIdentity, int> { }

    internal class IntentionallyInvalidIdentityType { }

    internal class ClassUsingIntentionallyInvalidIdentity : IdentityPersistenceBase<ClassUsingIntentionallyInvalidIdentity, IntentionallyInvalidIdentityType> { }


}
