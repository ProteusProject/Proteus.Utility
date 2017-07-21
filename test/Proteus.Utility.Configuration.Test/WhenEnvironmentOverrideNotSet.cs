using System;
using NUnit.Framework;

namespace Proteus.Utility.Configuration.Test
{
    [TestFixture]
    public class WhenEnvironmentOverrideNotSet
    {

        private const string ValueFromAppConfigFile = "value-read-from-app.config-file";
        private const string TheTestkey = "theTestKey";

        [Test]
        public void CanReadValueFromAppConfigFile()
        {
            Assume.That(Environment.GetEnvironmentVariable(TheTestkey, EnvironmentVariableTarget.Process), Is.Null, $"Environment variable {TheTestkey} must be cleared before this test can run.");

            var value = ConfigurationManager.AppSettings(TheTestkey);
            Assert.That(value, Is.EqualTo(ValueFromAppConfigFile));
        }
    }

    [TestFixture]
    public class WhenEnvironmentOverrideIsSet
    {

        private const string ValueFromAppEnvironmentVariable = "value-read-from-environment";
        private const string TheTestkey = "theTestKey";

        [SetUp]
        public void TestSetUp()
        {
            Environment.SetEnvironmentVariable(TheTestkey, ValueFromAppEnvironmentVariable, EnvironmentVariableTarget.Process);
        }

        [TearDown]
        public void TestTearDown()
        {
            Environment.SetEnvironmentVariable(TheTestkey,string.Empty, EnvironmentVariableTarget.Process);

        }

        [Test]
        public void CanReadValueFromEnvironment()
        {
            Assume.That(System.Configuration.ConfigurationManager.AppSettings[TheTestkey], Is.Not.EqualTo(ValueFromAppEnvironmentVariable), $"The value in app.config file for {TheTestkey} must not be {ValueFromAppEnvironmentVariable} when this test runs.");

            var value = ConfigurationManager.AppSettings(TheTestkey);
            Assert.That(value, Is.EqualTo(ValueFromAppEnvironmentVariable));
        }
    }
}