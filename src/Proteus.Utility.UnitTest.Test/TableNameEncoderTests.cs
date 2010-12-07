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

namespace Proteus.Utility.UnitTest.Test
{
    public class TableNameEncoderTests
    {
        [TestFixture]
        public class When_Input_Doesnt_Contain_Dot_Delimiter
        {
            [Test]
            public void Encode_Can_Return_Escaped_String()
            {
                var input = "TheTable";
                var output = new TableNameEncoder().Encode(input);
                Assert.That(output, Is.StringStarting("["));
                Assert.That(output, Is.StringEnding("]"));
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
                Assert.That(output, Is.EqualTo("[TheSchema].[TheTable]"));

            }
        }

    }
}
