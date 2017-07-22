using NUnit.Framework;

namespace Proteus.Utility.Configuration.Test.ConnectionStringsTests.SettingParserTests
{
    [TestFixture]
    public class WhenParsingValidPartialConnectionStringValue
    {
        [Test]
        public void CanReturnOnlyValuesPresentToParse()
        {
            var connectionStringSettingString = $"name=\"{TestConstants.ConnectionStringTestKey}\" connectionString=\"{TestConstants.ConnectionStringValueFromEnvironmentVariable}\"";
            var connectionStringSetting = ConnectionStringSettingParser.Parse(connectionStringSettingString);

            Assert.That(connectionStringSetting.Name, Is.EqualTo(TestConstants.ConnectionStringTestKey));
            Assert.That(connectionStringSetting.ConnectionString, Is.EqualTo(TestConstants.ConnectionStringValueFromEnvironmentVariable));
            Assert.That(connectionStringSetting.ProviderName, Is.Null);
        }
    }
}