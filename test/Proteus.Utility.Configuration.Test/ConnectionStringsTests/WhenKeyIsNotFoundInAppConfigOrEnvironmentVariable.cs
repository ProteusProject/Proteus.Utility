using System.Configuration;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Proteus.Utility.Configuration.Test.ConnectionStringsTests
{
    [TestFixture]
    public class WhenKeyIsNotFoundInAppConfigOrEnvironmentVariable
    {
        [Test]
        public void ThrowsException()
        {
            Assert.Throws<ConfigurationErrorsException>(
                () => EnvironmentAwareConfigurationManager.ConnectionStrings("some-key-that-doesnt-exist-in-env-or-app.config"));
        }
    }
}