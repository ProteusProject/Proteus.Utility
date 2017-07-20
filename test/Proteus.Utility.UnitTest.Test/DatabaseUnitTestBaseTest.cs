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
using System.Data;
using NUnit.Framework;
using Proteus.Utility.UnitTest.Database;

namespace Proteus.Utility.UnitTest.Test
{
    [TestFixture]
    [Category(UnitTestType.DatabaseDependent)]
    public class DatabaseUnitTestBaseTest : DatabaseUnitTestBase
    {
        [TestFixtureSetUp]
        public void _TestFixtureSetUp()
        {
            DatabaseTestFixtureSetUp();
        }

        [TestFixtureTearDown]
        public void _TestFixtureTearDown()
        {
            DatabaseTestFixtureTearDown();
        }

        [SetUp]
        public void _SetUp()
        {
            DatabaseSetUp();
        }

        [TearDown]
        public void _TearDown()
        {
            DatabaseTearDown();
        }

    }

    [TestFixture]
    [Category(UnitTestType.Unit)]
    public class NonDatabaseRelatedTests : DatabaseUnitTestBase
    {
        [Test]
        public void DerivesFromProperAncestor()
        {
            Assert.IsInstanceOfType(typeof(UnitTestBase), this);
        }

        [Test]
        public void GuidGen_Repeat_ReturnsCorrectGuidWhenCharacterIsInt()
        {
            Guid actual = GuidGen.Repeat(1);
            Guid expected = new Guid("11111111-1111-1111-1111-111111111111");

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GuidGen_Repeat_ReturnsCorrectGuidWhenCharacterIsString()
        {
            Guid actual = GuidGen.Repeat("1");
            Guid expected = new Guid("11111111-1111-1111-1111-111111111111");

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GuidGen_Repeat_ThrowsWhenCharacterIsComplexObject()
        {

            DataSet dataSet = new DataSet();

            try
            {
                Guid actual = GuidGen.Repeat(dataSet);
                Assert.Fail("Expected ArgumentException not Thrown!");
            }
            catch (ArgumentException)
            {

            }

        }

        [Test]
        public void GuidGen_Repeat_ThrowsWhenCharacterIsDouble()
        {

            try
            {
                Guid actual = GuidGen.Repeat(2.1);
                Assert.Fail("Expected ArgumentException not Thrown!");
            }
            catch (ArgumentException)
            {

            }

        }

        [Test]
        public void GuidGen_Repeat_ThrowsWhenCharacterIsEmptyString()
        {
            try
            {
                Guid actual = GuidGen.Repeat(string.Empty);
                Assert.Fail("Expected ArgumentException not Thrown!");
            }
            catch (ArgumentException)
            {

            }

        }

        [Test]
        public void GuidGen_Repeat_ThrowsWhenCharacterIsNull()
        {
            try
            {
                Guid actual = GuidGen.Repeat(null);
                Assert.Fail("Expected ArgumentNullException not Thrown!");
            }
            catch (ArgumentNullException)
            {

            }

        }

        [Test]
        public void GuidGen_Repeat_ThrowsWhenCharacterLengthGreaterThan1()
        {
            try
            {
                Guid actual = GuidGen.Repeat("11");
                Assert.Fail("Expected ArgumentOutOfRangeException not Thrown!");
            }
            catch (ArgumentOutOfRangeException)
            {

            }

        }

        [Test]
        public void GuidGen_Repeat_ThrowsWhenCharacterOutOfRange()
        {
            try
            {
                Guid actual = GuidGen.Repeat("Z");
                Assert.Fail("Expected ArgumentException not Thrown!");
            }
            catch (ArgumentException)
            {

            }

        }

    }
}
