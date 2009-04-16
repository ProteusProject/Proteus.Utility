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
    [Category(UnitTestType.DatabaseDependent)]
    public class DatabaseUnitTestBaseTest : DatabaseUnitTestBase
    {
        [FixtureSetUp]
        public void _FixtureSetUp()
        {
            DatabaseFixtureSetUp();
        }

        [FixtureTearDown]
        public void _FixtureTearDown()
        {
            DatabaseFixtureTearDown();
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
        public void GuidGen_Repeat_ReturnsCorrectGuid()
        {
            Guid actual = GuidGen.Repeat("1");
            Guid expected = new Guid("11111111-1111-1111-1111-111111111111");

            Assert.AreEqual(expected, actual);
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

    }
}
