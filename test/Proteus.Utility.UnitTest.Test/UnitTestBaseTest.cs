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
using UnitTestTest;

namespace Proteus.Utility.UnitTest.Test
{
    [TestFixture]
    [Category(UnitTestType.Unit)]
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
            Person p = new Person {Firstname = PERSON_TEST_FIELD_VALUE};
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
            Person p = new Person();
            Assert.Throws<ArgumentException>(
                () => SetInstanceFieldValue(p, $"{PERSON_TEST_FIELD_NAME}zzz", PERSON_TEST_FIELD_VALUE),
                "Expected Exception of type 'InvalidOperationException 'not thrown.");
        }

        [Test]
        public void CanThrowExceptionWhenSettingFieldToInvalidType()
        {
            Person p = new Person();
            Assert.Throws<ArgumentException>(() => SetInstanceFieldValue(p, PERSON_TEST_FIELD_NAME, new Person()),
                "Expected Exception of type 'ArgumentException' not thrown.");
        }

        [Test]
        public void CanReturnCorrectTypeWhenTypeIsCorrect()
        {
            var p = new Person {Firstname = PERSON_TEST_FIELD_VALUE};
            var instanceFieldValue = GetInstanceFieldValue<string>(p, PERSON_TEST_FIELD_NAME);
            Assert.AreEqual(PERSON_TEST_FIELD_VALUE, instanceFieldValue);
            Assert.IsInstanceOf<string>(instanceFieldValue);
        }

        [Test]
        public void CanThrowWhenTypeIsMismatched()
        {
            var p = new Person {Firstname = PERSON_TEST_FIELD_VALUE};
            Assert.Throws<ArgumentException>(() => GetInstanceFieldValue<int>(p, PERSON_TEST_FIELD_NAME));
        }

        [Test]
        public void CanThrowIfFieldNotFound()
        {
            var p = new Person();
            Assert.Throws<ArgumentException>(() => GetInstanceFieldValue(p, $"NOT_{PERSON_TEST_FIELD_NAME}"));
            Assert.Throws<ArgumentException>(() => SetInstanceFieldValue(p, $"NOT_{PERSON_TEST_FIELD_NAME}", PERSON_TEST_FIELD_VALUE));
        }

        [Test]
        public void CanThrowIfObjectIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => GetInstanceFieldValue(null, PERSON_TEST_FIELD_NAME));
            Assert.Throws<ArgumentNullException>(() => SetInstanceFieldValue(null, PERSON_TEST_FIELD_NAME, PERSON_TEST_FIELD_VALUE));
        }

        [Test]
        public void CanThrowIfFieldNameIsNull()
        {
            var p = new Person();
            Assert.Throws<ArgumentException>(() => GetInstanceFieldValue(p, null));
            Assert.Throws<ArgumentException>(() => SetInstanceFieldValue(p, null, PERSON_TEST_FIELD_VALUE));
        }

        [Test]
        public void CanThrowIfFieldNameIsEmptyString()
        {
            var p = new Person();
            Assert.Throws<ArgumentException>(() => GetInstanceFieldValue(p, string.Empty));
            Assert.Throws<ArgumentException>(() => SetInstanceFieldValue(p, string.Empty, PERSON_TEST_FIELD_VALUE));
        }
    }
}