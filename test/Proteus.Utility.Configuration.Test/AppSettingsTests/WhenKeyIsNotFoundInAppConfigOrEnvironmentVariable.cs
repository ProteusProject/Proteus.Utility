using System.Configuration;
using NUnit.Framework;

namespace Proteus.Utility.Configuration.Test
{
    [TestFixture]
    public class WhenKeyIsNotFoundInAppConfigOrEnvironmentVariable
    {
        [Test]
        public void ThrowsException()
        {
            Assert.Throws<ConfigurationErrorsException>(
                () => EnvironmentAwareConfigurationManager.AppSettings("some-key-that-doesnt-exist-in-env-or-app.config"));
        }
    }
}