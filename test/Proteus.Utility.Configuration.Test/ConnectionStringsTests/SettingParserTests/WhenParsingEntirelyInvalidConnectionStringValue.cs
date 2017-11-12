#region License

/*
 * Copyright © 2009-2017 the original author or authors.
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * 
 *      http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

#endregion

using NUnit.Framework;

namespace Proteus.Utility.Configuration.Test.ConnectionStringsTests.SettingParserTests
{
    [TestFixture]
    public class WhenParsingEntirelyInvalidConnectionStringValue
    {
        [Test]
        public void CanReturnNullInstanceIfAttributeNotFound()
        {
            var connectionStringSettingString = "this-is-a-completely-invalid-value-for-a-connection-string-setting-string";
            var setting = ConnectionStringSettingString.Parse(connectionStringSettingString);

            Assert.That(setting, Is.Null);
        }

        [Test]
        public void CanReturnNullInstanceIfAttributeFoundButNoOpeningQuote()
        {
            var connectionStringSettingString = "name=this-is-a-completely-invalid-value-for-a-connection-string-setting-string";
            var setting = ConnectionStringSettingString.Parse(connectionStringSettingString);

            Assert.That(setting, Is.Null);
        }

        [Test]
        public void CanReturnNullInstanceIfAttributeAndOpeningQuoteFoundButNoClosingQuote()
        {
            var connectionStringSettingString = "name=\"this-is-a-completely-invalid-value-for-a-connection-string-setting-string";
            var setting = ConnectionStringSettingString.Parse(connectionStringSettingString);

            Assert.That(setting, Is.Null);
        }
    }
}