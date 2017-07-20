/*
 *
 * Proteus
 * Copyright (c) 2008 - 2017
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
using NUnit.Framework;
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
        /// Determines whether this instance [can set private field instance of object].
        /// </summary>
        [Test]
        public void CanSetPrivateFieldInstanceOfObject()
        {
            Person p = new Person();
            SetInstanceFieldValue(p, PERSON_TEST_FIELD_NAME, PERSON_TEST_FIELD_VALUE);
            Assert.AreEqual(PERSON_TEST_FIELD_VALUE, p.Firstname);

        }

        /// <summary>
        /// Determines whether this instance [can throw exception on fieldName not found in object].
        /// </summary>
        [Test]
        public void CanThrowExceptionOnFieldNameNotFoundInObject()
        {
            try
            {
                Person p = new Person();
                SetInstanceFieldValue(p, $"{PERSON_TEST_FIELD_NAME}zzz", PERSON_TEST_FIELD_VALUE);
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
