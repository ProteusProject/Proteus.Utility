#region License

/*
*
*  Copyright (c) 2020 Stephen A. Bohlen
*
*  Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"),
*  to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense,
*  and/or sell copies of the Software, and to permit persons to whom the Software is *furnished to do so, subject to the following conditions:
* 
*  The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
*
*  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
*  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
*  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS
*  IN THE SOFTWARE.
*
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