using System;
using NUnit.Framework;

namespace Proteus.Utility.Configuration.Test
{
    [TestFixture]
    public class WhenKeyIsFoundInAppConfigAndNotEnvironmentVariable
    {

        private const string ValueFromAppConfigFile = "value-read-from-app.config-file";
        private const string TheTestkey = "theTestKey";

        [Test]
        public void ValueIsReadFromAppConfigFile()
        {
            Assume.That(Environment.GetEnvironmentVariable(TheTestkey, EnvironmentVariableTarget.Process), Is.Null, $"Environment variable {TheTestkey} must be cleared before this test can run.");

            var value = EnvironmentAwareConfigurationManager.AppSettings(TheTestkey);
            Assert.That(value, Is.EqualTo(ValueFromAppConfigFile));
        }
    }
}