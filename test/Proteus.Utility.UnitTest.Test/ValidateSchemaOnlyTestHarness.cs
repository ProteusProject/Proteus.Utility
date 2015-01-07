/*
 *
 * Proteus
 * Copyright (c) 2008 - 2011
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

namespace Proteus.Utility.UnitTest.Test
{

    public class ValidateSchemaUsingSchemaPrefixedTablenames : DatabaseUnitTestBase
    {
        protected override string TestDataFilename
        {
            get { return @"TestData\DataFileWithSchemaPrefixes.xml"; }
        }

        protected override string TestSchemaFilename
        {
            get { return @"TestData\SchemaWithSchemaPrefixes.xsd"; }
        }

        [Test]
        public void ValidateSchema()
        {
            ValidateSchemaAgainstDatabase(TestSchemaFilename);
        }

        protected override void DatabaseTestFixtureSetUp() { }

        protected override void DatabaseTestFixtureTearDown() { }

        protected override void DatabaseSetUp() { }

        protected override void DatabaseTearDown() { }

    }

    public class ValidateSchemaUsingNonSchemaPrefixedTablenames : DatabaseUnitTestBase
    {
        [Test]
        public void ValidateSchema()
        {
            ValidateSchemaAgainstDatabase(TestSchemaFilename);
        }

        protected override void DatabaseTestFixtureSetUp() { }

        protected override void DatabaseTestFixtureTearDown() { }

        protected override void DatabaseSetUp() { }

        protected override void DatabaseTearDown() { }

    }
}
