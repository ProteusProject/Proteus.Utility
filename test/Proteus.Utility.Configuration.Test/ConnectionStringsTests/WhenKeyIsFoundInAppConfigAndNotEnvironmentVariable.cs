using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Proteus.Utility.Configuration.Test.ConnectionStringsTests
{
    [TestFixture]
    public class WhenKeyIsFoundInAppConfigAndNotEnvironmentVariable
    {
        [Test]
        public void ValueIsReadFromAppConfig()
        {
            var value = ExtensibleSourceConfigurationManager.ConnectionStrings(ConnectionStringTests.TestKey);

            Assert.That(value.ConnectionString, Is.EqualTo(ConnectionStringTests.AppConfigConnectionStringValue));
            Assert.That(value.ProviderName, Is.EqualTo(ConnectionStringTests.AppConfigProviderNameValue));
        }
    }
}