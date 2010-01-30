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
using System.Text;
using MbUnit.Framework;
using Proteus.Utility.UnitTest;
using System.Diagnostics;

namespace UnitTestTest
{
    [TestFixture]
    [Category(UnitTestType.DatabaseDependent)]
    public class DatabaseUnitTestBaseHarness : DatabaseUnitTestBase
    {
        [FixtureSetUp]
        public void _FixtureSetUp()
        {
            DatabaseFixtureSetUp();
            Trace.WriteLine("DERIVED: FixtureSetUp");
        }

        [FixtureTearDown]
        public void _FixtureTearDown()
        {
            //other stuff you also need to do goes here!
            DatabaseFixtureTearDown();
            Trace.WriteLine("DERIVED: FixtureTearDown");
        }

        [Test]
        public void ImATest2()
        {
            Trace.WriteLine("DERIVED: Test2");
        }

        [Test]
        public void ImATest()
        {

            Trace.WriteLine("DERIVED: Test1");
        }

        [SetUp]
        public void _SetUp()
        {
            DatabaseSetUp();
            Trace.WriteLine("DERIVED: TestSetUp");
        }

        [TearDown]
        public void _TearDown()
        {
            DatabaseTearDown();
            Trace.WriteLine("DERIVED: TestTearDown");
        }

    }
}
