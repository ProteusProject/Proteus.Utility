using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework.Internal;
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
            ExtensibleSourceConfigurationManager.ConnectionStringReaders.Add(EnvironmentVariableConfigurationReader.GetConnectionString);
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
