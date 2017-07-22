using System;
using System.Configuration;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Proteus.Utility.Configuration.Test
{
    [TestFixture]
    public class WhenUsingGenericTypedMethodToRetrieveSetting
    {
        [TearDown]
        public void TestTearDown()
        {
            SetEnvironmentVariable(string.Empty);
        }


        [Test]
        public void CanRetrieveBooleanValue()
        {
            SetEnvironmentVariable("true");
            Assert.That(EnvironmentAwareConfigurationManager.AppSettings<bool>("test-key"), Is.True);
        }

        [Test]
        public void CanRetrieveNumericValue()
        {
            SetEnvironmentVariable("10");
            Assert.That(EnvironmentAwareConfigurationManager.AppSettings<int>("test-key"), Is.EqualTo(10));
        }

        [Test]
        public void CanRetrieveUriValue()
        {
            var uriString = "http://some/host/some/resource";
            SetEnvironmentVariable(uriString);
            var value = EnvironmentAwareConfigurationManager.AppSettings<Uri>("test-key");

            Assert.That(value, Is.TypeOf<Uri>());
            Assert.That(value.AbsoluteUri, Is.EqualTo(uriString));
        }

        [Test]
        public void ThrowsIfKeyNotFound()
        {
            Assert.Throws<ConfigurationErrorsException>(
                () => EnvironmentAwareConfigurationManager.AppSettings<object>("test-key"));
        }

        [Test]
        public void ThrowsIfInvalidTypeConversion()
        {
            SetEnvironmentVariable("10");
            
            try
            {
                EnvironmentAwareConfigurationManager.AppSettings<bool>("test-key");
            }
            catch (ConfigurationErrorsException ex)
            {
                Assert.That(ex, Is.TypeOf<ConfigurationErrorsException>());
                Assert.That(ex.InnerException, Is.TypeOf<FormatException>());
            }
        }

        private void SetEnvironmentVariable(string value)
        {
            Environment.SetEnvironmentVariable("test-key", value, EnvironmentVariableTarget.Process);
        }
    }
}