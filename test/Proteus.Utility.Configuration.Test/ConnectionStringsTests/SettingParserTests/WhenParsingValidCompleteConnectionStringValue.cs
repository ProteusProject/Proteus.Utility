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
            _connectionStringSetting = ConnectionStringSettingParser.Parse(TestConstants.ConnectionStringValueOfEnvironmentVariable);
        }

        [Test]
        public void CanParseConnectionName()
        {
            Assert.That(_connectionStringSetting.Name, Is.EqualTo(TestConstants.ConnectionStringTestKey));
        }

        [Test]
        public void CanParseConnectionString()
        {
            Assert.That(_connectionStringSetting.ConnectionString, Is.EqualTo(TestConstants.ConnectionStringValueFromEnvironmentVariable));
        }

        [Test]
        public void CanParseProviderName()
        {
            Assert.That(_connectionStringSetting.ProviderName, Is.EqualTo(TestConstants.ConnectionStringProviderNameValueFromEnvironmentVariable));
        }


        [Test]
        public void OrderOfValuesDoesNotAffectResults()
        {
            var connectionSettingString = $"providerName=\"{TestConstants.ConnectionStringProviderNameValueFromEnvironmentVariable}\" name=\"{TestConstants.ConnectionStringTestKey}\" connectionString=\"{TestConstants.ConnectionStringValueFromEnvironmentVariable}\"";
            _connectionStringSetting = ConnectionStringSettingParser.Parse(connectionSettingString);

            CanParseConnectionName();
            CanParseConnectionString();
            CanParseProviderName();
        }
    }
}