using System;
using System.Collections.Generic;
using System.Text;
using MbUnit.Framework;

namespace Proteus.Utility.UnitTest.Test
{
    public class ValidateSchemaOnlyTestHarness : DatabaseUnitTestBase
    {
        protected override void DatabaseFixtureSetUp()
        {
            
        }
        protected override void DatabaseFixtureTearDown()
        {
            
        }
        protected override void DatabaseSetUp()
        {
            
        }
        protected override void DatabaseTearDown()
        {
            
        }

        [Test]
        public void ValidateSchema()
        {
            ValidateSchemaAgainstDatabase(TestSchemaFilename);
        }


    }
}
