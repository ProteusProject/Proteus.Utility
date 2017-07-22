using System;
using NUnit.Framework;
using Proteus.Utility.Configuration.Test.ConnectionStringsTests;

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

            var value = EnvironmentAwareConfigurationManager.AppSettings(TheTestkey);
            Assert.That(value, Is.EqualTo(AppSettingTests.AppConfigValue));
        }
    }
}