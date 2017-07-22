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
            Environment.SetEnvironmentVariable(TestConstants.AppSettingTestKey, TestConstants.AppSettingsValueFromEnvironmentVariable, EnvironmentVariableTarget.Process);
        }

        [TearDown]
        public void TestTearDown()
        {
            Environment.SetEnvironmentVariable(TestConstants.AppSettingTestKey,string.Empty, EnvironmentVariableTarget.Process);

        }

        [Test]
        public void ValueIsReadFromEnvironment()
        {
            Assume.That(System.Configuration.ConfigurationManager.AppSettings[TestConstants.AppSettingTestKey], Is.Not.EqualTo(TestConstants.AppSettingsValueFromEnvironmentVariable), $"The value in app.config file for {TestConstants.AppSettingTestKey} must not be {TestConstants.AppSettingsValueFromEnvironmentVariable} when this test runs.");

            var value = EnvironmentAwareConfigurationManager.AppSettings(TestConstants.AppSettingTestKey);
            Assert.That(value, Is.EqualTo(TestConstants.AppSettingsValueFromEnvironmentVariable));
        }
    }
}