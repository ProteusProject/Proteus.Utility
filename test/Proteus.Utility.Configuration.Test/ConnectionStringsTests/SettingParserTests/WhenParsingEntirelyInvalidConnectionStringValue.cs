using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Proteus.Utility.Configuration.Test.ConnectionStringsTests.SettingParserTests
{
    [TestFixture]
    public class WhenParsingEntirelyInvalidConnectionStringValue
    {
        [Test]
        public void CanReturnNullInstanceIfAttributeNotFound()
        {
            var connectionStringSettingString = "this-is-a-completely-invalid-value-for-a-connection-string-setting-string";
            var setting = ConnectionStringSettingString.Parse(connectionStringSettingString);

            Assert.That(setting, Is.Null);
        }

        [Test]
        public void CanReturnNullInstanceIfAttributeFoundButNoOpeningQuote()
        {
            var connectionStringSettingString = "name=this-is-a-completely-invalid-value-for-a-connection-string-setting-string";
            var setting = ConnectionStringSettingString.Parse(connectionStringSettingString);

            Assert.That(setting, Is.Null);
        }

        [Test]
        public void CanReturnNullInstanceIfAttributeAndOpeningQuoteFoundButNoClosingQuote()
        {
            var connectionStringSettingString = "name=\"this-is-a-completely-invalid-value-for-a-connection-string-setting-string";
            var setting = ConnectionStringSettingString.Parse(connectionStringSettingString);

            Assert.That(setting, Is.Null);
        }
    }
}