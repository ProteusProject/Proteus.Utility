using System.Configuration;
using NUnit.Framework;

namespace Proteus.Utility.Configuration.Test.ConnectionStringsTests
{
    [TestFixture]
    public class WhenParsingCompleteConnectionStringValue
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
            Assert.That(_connectionStringSetting.Name, Is.EqualTo(TestConstants.ConnectionStringProviderNameValueFromEnvironmentVariable));
        }
    }
}