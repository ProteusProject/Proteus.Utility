using System;
using System.Collections.Generic;
using System.Text;
using MbUnit.Framework;

namespace Proteus.Utility.UnitTest.Test
{

    public class ValidateSchemaUsingSchemaPrefixedTablenames : DatabaseUnitTestBase
    {
        protected override string TestDataFilename
        {
            get { return @"..\..\TestData\DataFileWithSchemaPrefixes.xml"; }
        }

        protected override string TestSchemaFilename
        {
            get { return @"..\..\TestData\SchemaWithSchemaPrefixes.xsd"; }
        }

        [Test]
        public void ValidateSchema()
        {
            ValidateSchemaAgainstDatabase(TestSchemaFilename);
        }

        protected override void DatabaseFixtureSetUp() { }

        protected override void DatabaseFixtureTearDown() { }

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

        protected override void DatabaseFixtureSetUp() { }

        protected override void DatabaseFixtureTearDown() { }

        protected override void DatabaseSetUp() { }

        protected override void DatabaseTearDown() { }

    }
}
