using System;
using NUnit.Framework;

namespace Proteus.Utility.Configuration.Test.AppSettingsTests
{
    [TestFixture]
    public class WhenKeyIsFoundInAppConfigAndNotEnvironmentVariable
    {

        
        private const string TheTestkey = "theTestKey";

        [Test]
        public void ValueIsReadFromAppConfigFile()
        {
            Assume.That(Environment.GetEnvironmentVariable(TheTestkey, EnvironmentVariableTarget.Process), Is.Null, $"Environment variable {TheTestkey} must be cleared before this test can run.");

            var value = ExtensibleSourceConfigurationManager.AppSettings(TheTestkey);
            Assert.That(value, Is.EqualTo(AppSettingTests.AppConfigValue));
        }
    }
}