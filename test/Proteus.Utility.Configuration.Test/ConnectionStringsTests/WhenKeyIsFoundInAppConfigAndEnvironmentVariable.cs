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
            Environment.SetEnvironmentVariable(TestConstants.ConnectionStringTestKey, TestConstants.ConnectionStringValueOfEnvironmentVariable, EnvironmentVariableTarget.Process);
        }

        [TearDown]
        public void TestTearDown()
        {
            Environment.SetEnvironmentVariable(TestConstants.ConnectionStringTestKey, string.Empty, EnvironmentVariableTarget.Process);
        }

        [Test]
        public void ValueIsReadFromEnvironment()
        {
            Assume.That(System.Configuration.ConfigurationManager.ConnectionStrings[TestConstants.ConnectionStringTestKey], Is.Not.Null, $"The value in app.config file for {TestConstants.ConnectionStringTestKey} must not be {TestConstants.AppSettingsValueFromEnvironmentVariable} when this test runs.");

            var value = EnvironmentAwareConfigurationManager.ConnectionStrings(TestConstants.ConnectionStringTestKey);
            Assert.That(value.ConnectionString, Is.EqualTo(TestConstants.ConnectionStringValueFromEnvironmentVariable));
        }
    }
}
