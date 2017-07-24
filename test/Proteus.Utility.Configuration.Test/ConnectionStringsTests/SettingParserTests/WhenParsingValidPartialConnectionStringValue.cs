using NUnit.Framework;

namespace Proteus.Utility.Configuration.Test.ConnectionStringsTests.SettingParserTests
{
    [TestFixture]
    public class WhenParsingValidPartialConnectionStringValue
    {
        [Test]
        public void CanReturnOnlyValuesPresentToParse()
        {
            var connectionStringSettingString = $"name=\"{ConnectionStringTests.TestKey}\" connectionString=\"{ConnectionStringTests.EnvironmentVariableConnectionStringValue}\"";
            var connectionStringSetting = ConnectionStringSettingString.Parse(connectionStringSettingString);

            Assert.That(connectionStringSetting.Name, Is.EqualTo(ConnectionStringTests.TestKey));
            Assert.That(connectionStringSetting.ConnectionString, Is.EqualTo(ConnectionStringTests.EnvironmentVariableConnectionStringValue));
            Assert.That(connectionStringSetting.ProviderName, Is.Null);
        }
    }
}