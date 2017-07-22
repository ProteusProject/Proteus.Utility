using System;
using NUnit.Framework;
using Proteus.Utility.Configuration.Test.ConnectionStringsTests;

namespace Proteus.Utility.Configuration.Test.AppSettingsTests
{
    [TestFixture]
    public class WhenKeyIsFoundInAppConfigAndEnvironmentVariable
    {
        [SetUp]
        public void TestSetUp()
        {
            Environment.SetEnvironmentVariable(TestConstants.TheTestkey, TestConstants.ValueFromAppEnvironmentVariable, EnvironmentVariableTarget.Process);
        }

        [TearDown]
        public void TestTearDown()
        {
            Environment.SetEnvironmentVariable(TestConstants.TheTestkey,string.Empty, EnvironmentVariableTarget.Process);

        }

        [Test]
        public void ValueIsReadFromEnvironment()
        {
            Assume.That(System.Configuration.ConfigurationManager.AppSettings[TestConstants.TheTestkey], Is.Not.EqualTo(TestConstants.ValueFromAppEnvironmentVariable), $"The value in app.config file for {TestConstants.TheTestkey} must not be {TestConstants.ValueFromAppEnvironmentVariable} when this test runs.");

            var value = EnvironmentAwareConfigurationManager.AppSettings(TestConstants.TheTestkey);
            Assert.That(value, Is.EqualTo(TestConstants.ValueFromAppEnvironmentVariable));
        }
    }
}