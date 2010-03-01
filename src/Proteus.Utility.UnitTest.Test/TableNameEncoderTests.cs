using System;
using System.Collections.Generic;
using System.Text;
using MbUnit.Framework;

namespace Proteus.Utility.UnitTest.Test
{
    public class TableNameEncoderTests
    {
        [TestFixture]
        public class When_Input_Doesnt_Contain_Dot_Delimiter
        {
            [Test]
            [MultipleAsserts]
            public void Encode_Can_Return_Escaped_String()
            {
                var input = "TheTable";
                var output = new TableNameEncoder().Encode(input);
                Assert.StartsWith(output, "[");
                Assert.EndsWith(output, "]");
            }
        }

        [TestFixture]
        public class When_Input_Contains_A_Single_Dot_Delimiter
        {
            [Test]
            public void Can_Return_Escaped_String()
            {
                var input = "TheSchema.TheTable";
                var output = new TableNameEncoder().Encode(input);
                Assert.AreEqual("[TheSchema].[TheTable]", output);

            }
        }

    }
}
