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
            Environment.SetEnvironmentVariable(AppSettingTests.TestKey, AppSettingTests.EnvironmentVariableValue, EnvironmentVariableTarget.Process);
            ExtensibleSourceConfigurationManager.AppSettingReaders.Add(EnvironmentVariableReader.GetAppSetting);
        }

        [TearDown]
        public void TestTearDown()
        {
            Environment.SetEnvironmentVariable(AppSettingTests.TestKey,string.Empty, EnvironmentVariableTarget.Process);

        }

        [Test]
        public void ValueIsReadFromEnvironment()
        {
            Assume.That(System.Configuration.ConfigurationManager.AppSettings[AppSettingTests.TestKey], Is.Not.EqualTo(AppSettingTests.EnvironmentVariableValue), $"The value in app.config file for {AppSettingTests.TestKey} must not be {AppSettingTests.EnvironmentVariableValue} when this test runs.");

            var value = ExtensibleSourceConfigurationManager.AppSettings(AppSettingTests.TestKey);
            Assert.That(value, Is.EqualTo(AppSettingTests.EnvironmentVariableValue));
        }
    }
}