#region License

/*
 * Copyright � 2009-2017 the original author or authors.
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

using System.Configuration;
using NUnit.Framework;

namespace Proteus.Utility.Configuration.Test.ConnectionStringsTests.SettingParserTests
{
    [TestFixture]
    public class WhenParsingValidCompleteConnectionStringValue
    {
        private ConnectionStringSettings _connectionStringSetting;

        [SetUp]
        public void TestSetUp()
        {
            _connectionStringSetting = ConnectionStringSettingString.Parse(ConnectionStringTests.EnvironmentVariableSetting);
        }

        [Test]
        public void CanParseConnectionName()
        {
            Assert.That(_connectionStringSetting.Name, Is.EqualTo(ConnectionStringTests.TestKey));
        }

        [Test]
        public void CanParseConnectionString()
        {
            Assert.That(_connectionStringSetting.ConnectionString, Is.EqualTo(ConnectionStringTests.EnvironmentVariableConnectionStringValue));
        }

        [Test]
        public void CanParseProviderName()
        {
            Assert.That(_connectionStringSetting.ProviderName, Is.EqualTo(ConnectionStringTests.EnvironmentVariableProviderNameValue));
        }


        [Test]
        public void OrderOfValuesDoesNotAffectResults()
        {
            var connectionSettingString = $"providerName=\"{ConnectionStringTests.EnvironmentVariableProviderNameValue}\" name=\"{ConnectionStringTests.TestKey}\" connectionString=\"{ConnectionStringTests.EnvironmentVariableConnectionStringValue}\"";
            _connectionStringSetting = ConnectionStringSettingString.Parse(connectionSettingString);

            CanParseConnectionName();
            CanParseConnectionString();
            CanParseProviderName();
        }
    }
}