using System;
using NUnit.Framework;

namespace Proteus.Utility.Configuration.Test
{
    [TestFixture]
    public class WhenKeyIsFoundInAppConfigAndEnvironmentVariable
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
        public void ValueIsReadFromEnvironment()
        {
            Assume.That(System.Configuration.ConfigurationManager.AppSettings[TheTestkey], Is.Not.EqualTo(ValueFromAppEnvironmentVariable), $"The value in app.config file for {TheTestkey} must not be {ValueFromAppEnvironmentVariable} when this test runs.");

            var value = EnvironmentAwareConfigurationManager.AppSettings(TheTestkey);
            Assert.That(value, Is.EqualTo(ValueFromAppEnvironmentVariable));
        }
    }
}