/*
 *
 * Proteus
 * Copyright (C) 2008, 2009, 2010
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
using System.Text;
using NUnit.Framework;
using Proteus.Utility.UnitTest;
using System.Diagnostics;

namespace UnitTestTest
{
    [TestFixture]
    [Category(UnitTestType.DatabaseDependent)]
    public class DatabaseUnitTestBaseHarness : DatabaseUnitTestBase
    {
        [TestFixtureSetUp]
        public void _TestFixtureSetUp()
        {
            DatabaseTestFixtureSetUp();
            Trace.WriteLine("DERIVED: TestFixtureSetUp");
        }

        [TestFixtureTearDown]
        public void _TestFixtureTearDown()
        {
            //other stuff you also need to do goes here!
            DatabaseTestFixtureTearDown();
            Trace.WriteLine("DERIVED: TestFixtureTearDown");
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
