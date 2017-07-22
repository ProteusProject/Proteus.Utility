using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Proteus.Utility.Configuration.Test.ConnectionStringsTests
{
    [TestFixture]
    public class Class1
    {
        private const string Key = "theTestKey";
        private const string ConnectionString = "the-connection-string-from-app.config-file";
        private const string ProviderName = "the-provider-name-from-app.config-file";



        [Test]
        public void CanReadConnectionString()
        {
            var value = EnvironmentAwareConfigurationManager.ConnectionStrings(Key);

            Assert.That(value.ConnectionString, Is.EqualTo(ConnectionString));
            Assert.That(value.ProviderName, Is.EqualTo(ProviderName));
        }
    }
}