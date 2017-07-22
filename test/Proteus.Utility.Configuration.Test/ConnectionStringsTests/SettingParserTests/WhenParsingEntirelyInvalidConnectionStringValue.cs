using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Proteus.Utility.Configuration.Test.ConnectionStringsTests.SettingParserTests
{
    [TestFixture]
    public class WhenParsingEntirelyInvalidConnectionStringValue
    {
        [Test]
        public void CanReturnNullInstance()
        {
            var connectionStringSettingString = "this-is-a-completely-invalid-value-for-a-connection-string-setting-string";
            var setting = ConnectionStringSettingParser.Parse(connectionStringSettingString);

            Assert.That(setting, Is.Null);
        }
    }
}