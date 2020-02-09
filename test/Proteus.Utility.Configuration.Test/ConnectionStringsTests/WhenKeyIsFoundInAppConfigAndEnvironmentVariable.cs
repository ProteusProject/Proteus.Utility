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

using System;
 using NUnit.Framework;

namespace Proteus.Utility.Configuration.Test.ConnectionStringsTests
{
    [TestFixture]
    public class WhenKeyIsFoundInAppConfigAndEnvironmentVariable
    {
        [SetUp]
        public void TestSetUp()
        {
            Environment.SetEnvironmentVariable(ConnectionStringTests.TestKey, ConnectionStringTests.EnvironmentVariableSetting, EnvironmentVariableTarget.Process);
            ExtensibleSourceConfigurationManager.ConnectionStringReaders.Add(EnvironmentVariableReader.GetConnectionString);
        }

        [TearDown]
        public void TestTearDown()
        {
            Environment.SetEnvironmentVariable(ConnectionStringTests.TestKey, string.Empty, EnvironmentVariableTarget.Process);
        }

        [Test]
        public void ValueIsReadFromEnvironment()
        {
            Assume.That(System.Configuration.ConfigurationManager.ConnectionStrings[ConnectionStringTests.TestKey], Is.Not.Null, $"The value in app.config file for {ConnectionStringTests.TestKey} must not be {AppSettingTests.EnvironmentVariableValue} when this test runs.");

            var value = ExtensibleSourceConfigurationManager.ConnectionStrings(ConnectionStringTests.TestKey);
            Assert.That(value.ConnectionString, Is.EqualTo(ConnectionStringTests.EnvironmentVariableConnectionStringValue));
        }
    }
}
