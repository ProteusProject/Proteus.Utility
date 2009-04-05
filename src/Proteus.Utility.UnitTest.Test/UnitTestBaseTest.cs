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
using System.Text;
using MbUnit.Framework;
using Proteus.Utility.UnitTest;

namespace UnitTestTest
{
    [TestFixture]
    [Category(UnitTestBase.UnitTestType.Unit)]
    public class UnitTestBaseTest : UnitTestBase
    {

        private const string PERSON_TEST_FIELD_NAME = "_firstname";

        private const string PERSON_TEST_FIELD_VALUE = "TestValue";

        /// <summary>
        /// Determines whether this instance [can get private field in instance of object].
        /// </summary>
        [Test]
        public void CanGetPrivateFieldInInstanceOfObject()
        {
            Person p = new Person { Firstname = PERSON_TEST_FIELD_VALUE };
            Assert.AreEqual(PERSON_TEST_FIELD_VALUE, GetInstanceFieldValue(p, PERSON_TEST_FIELD_NAME));
        }

        /// <summary>
        /// Determines whether this instance [can set private field ininstance of object].
        /// </summary>
        [Test]
        public void CanSetPrivateFieldIninstanceOfObject()
        {
            Person p = new Person();
            SetInstanceFieldValue(p, PERSON_TEST_FIELD_NAME, PERSON_TEST_FIELD_VALUE);
            Assert.AreEqual(PERSON_TEST_FIELD_VALUE, p.Firstname);

        }

        /// <summary>
        /// Determines whether this instance [can throw exception on fieldname not found in object].
        /// </summary>
        [Test]
        public void CanThrowExceptionOnFieldnameNotFoundInObject()
        {
            try
            {
                Person p = new Person();
                SetInstanceFieldValue(p, PERSON_TEST_FIELD_NAME + "zzz", PERSON_TEST_FIELD_VALUE);
                Assert.Fail("Expected Exception of type 'InvalidOperationException 'not thrown.");
            }
            catch (ArgumentException)
            {

            }

        }

        [Test]
        public void CanThrowExceptionWhenSettingFieldToInvalidType()
        {

            try
            {
                Person p = new Person();
                SetInstanceFieldValue(p, PERSON_TEST_FIELD_NAME, new Person());
                Assert.Fail("Expected Exception of type 'ArgumentException' not thrown.");
            }
            catch (ArgumentException)
            {

            }

        }

    }
}
