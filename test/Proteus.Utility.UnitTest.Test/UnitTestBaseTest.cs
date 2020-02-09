#region License

/*
*
*  Copyright (c) 2020 Stephen A. Bohlen
*
*  Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"),
*  to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense,
*  and/or sell copies of the Software, and to permit persons to whom the Software is *furnished to do so, subject to the following conditions:
* 
*  The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
*
*  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
*  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
*  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS
*  IN THE SOFTWARE.
*
*/

#endregion


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