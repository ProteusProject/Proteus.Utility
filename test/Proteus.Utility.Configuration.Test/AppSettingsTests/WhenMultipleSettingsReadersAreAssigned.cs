using System;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Proteus.Utility.Configuration.Test.AppSettingsTests
{
    [TestFixture]
    public class WhenMultipleSettingsReadersAreAssigned
    {
        [SetUp]
        public void TestSetUp()
        {
            Environment.SetEnvironmentVariable(AppSettingTests.TestKey, AppSettingTests.EnvironmentVariableValue, EnvironmentVariableTarget.Process);
            Environment.SetEnvironmentVariable(AppSettingTests.EnvironmentVariableOnlyTestKey, AppSettingTests.EnvironmentVariableValue, EnvironmentVariableTarget.Process);
            ExtensibleSourceConfigurationManager.AppSettingReaders.Add(LocalSettingsJsonReader.GetAppSetting);
            ExtensibleSourceConfigurationManager.AppSettingReaders.Add(EnvironmentVariableReader.GetAppSetting);
        }

        [TearDown]
        public void TestTearDown()
        {
            Environment.SetEnvironmentVariable(AppSettingTests.TestKey, string.Empty, EnvironmentVariableTarget.Process);
            Environment.SetEnvironmentVariable(AppSettingTests.EnvironmentVariableOnlyTestKey, string.Empty, EnvironmentVariableTarget.Process);
        }

        [Test]
        public void CanRespectOrderOfValueResolution()
        {
            var testValue = ExtensibleSourceConfigurationManager.AppSettings(AppSettingTests.TestKey);
            var environmentOnlyValue = ExtensibleSourceConfigurationManager.AppSettings(AppSettingTests.EnvironmentVariableOnlyTestKey);
            var localSettingsJsonOnlyValue = ExtensibleSourceConfigurationManager.AppSettings(AppSettingTests.LocalSettingsJsonOnlyTestKey);

            Assert.That(testValue, Is.EqualTo(AppSettingTests.EnvironmentVariableValue));
            Assert.That(environmentOnlyValue, Is.EqualTo(AppSettingTests.EnvironmentVariableValue));
            Assert.That(localSettingsJsonOnlyValue, Is.EqualTo(AppSettingTests.LocalSettingsJsonValue));
        }
    }
}