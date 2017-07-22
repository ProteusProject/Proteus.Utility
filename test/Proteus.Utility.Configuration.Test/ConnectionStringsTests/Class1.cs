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


    public class TestConstants
    {
        public const string ConnectionStringTestKey = "theTestKey";
        public const string ConnectionStringFromAppConfig = "the-connection-string-from-app.config-file";
        public const string ProviderNameFromAppConfig = "the-provider-name-from-app.config-file";

        public const string AppSettingsValueFromAppEnvironmentVariable = "value-read-from-environment";
        public const string AppSettingTestKey = "theTestKey";
        public const string AppSettingsValueFromAppConfigFile = "value-read-from-app.config-file";
    }
}