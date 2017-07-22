using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Proteus.Utility.Configuration.Test.ConnectionStringsTests
{
    [TestFixture]
    public class Class1
    {
        [Test]
        public void CanReadConnectionString()
        {
            var value = EnvironmentAwareConfigurationManager.ConnectionStrings(TestConstants.ConnectionStringTestKey);

            Assert.That(value.ConnectionString, Is.EqualTo(TestConstants.ConnectionStringFromAppConfig));
            Assert.That(value.ProviderName, Is.EqualTo(TestConstants.ProviderNameFromAppConfig));
        }
    }
}