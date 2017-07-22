using System.Configuration;
using NUnit.Framework;

namespace Proteus.Utility.Configuration.Test.ConnectionStringsTests.SettingParserTests
{
    [TestFixture]
    public class WhenParsingValidCompleteConnectionStringValue
    {
        private ConnectionStringSettings _connectionStringSetting;

        [SetUp]
        public void TestSetUp()
        {
            _connectionStringSetting = ConnectionStringSettingParser.Parse(ConnectionStringTests.EnvironmentVariableSetting);
        }

        [Test]
        public void CanParseConnectionName()
        {
            Assert.That(_connectionStringSetting.Name, Is.EqualTo(ConnectionStringTests.TestKey));
        }

        [Test]
        public void CanParseConnectionString()
        {
            Assert.That(_connectionStringSetting.ConnectionString, Is.EqualTo(ConnectionStringTests.EnvironmentVariableConnectionStringValue));
        }

        [Test]
        public void CanParseProviderName()
        {
            Assert.That(_connectionStringSetting.ProviderName, Is.EqualTo(ConnectionStringTests.EnvironmentVariableProviderNameValue));
        }


        [Test]
        public void OrderOfValuesDoesNotAffectResults()
        {
            var connectionSettingString = $"providerName=\"{ConnectionStringTests.EnvironmentVariableProviderNameValue}\" name=\"{ConnectionStringTests.TestKey}\" connectionString=\"{ConnectionStringTests.EnvironmentVariableConnectionStringValue}\"";
            _connectionStringSetting = ConnectionStringSettingParser.Parse(connectionSettingString);

            CanParseConnectionName();
            CanParseConnectionString();
            CanParseProviderName();
        }
    }
}