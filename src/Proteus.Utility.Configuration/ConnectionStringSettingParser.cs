using System;
using System.Configuration;

namespace Proteus.Utility.Configuration
{
    public static class ConnectionStringSettingParser
    {
        public static ConnectionStringSettings Parse(string connectionSettingString)
        {
            var name = ParseNameFrom(connectionSettingString);
            var connectionString = ParseConnectionStringFrom(connectionSettingString);
            var providerName = ParseProviderNameFrom(connectionSettingString);

            //if all three values are NULL then the input string contained NO elements that could be identified
            // and the expected behavior is to return NULL
            if (null == name && null == connectionString && null == providerName)
            {
                return null;
            }

            //otherwise, return a new object using the parsed values
            return new ConnectionStringSettings(name, connectionString, providerName);
        }

        private static string ParseProviderNameFrom(string connectionSettingString)
        {
            return ParseAttributeValueFrom(connectionSettingString, "providerName=");
        }

        private static string ParseConnectionStringFrom(string connectionSettingString)
        {
            return ParseAttributeValueFrom(connectionSettingString, "connectionString=");
        }

        private static string ParseNameFrom(string connectionSettingString)
        {
            return ParseAttributeValueFrom(connectionSettingString, "name=");
        }

        private static string ParseAttributeValueFrom(string connectionSettingString, string attributeName)
        {
            var nameStartPos = connectionSettingString.IndexOf(attributeName, StringComparison.Ordinal);
            if (nameStartPos < 0)
            {
                return null;
            }

            var startQuotePos = connectionSettingString.IndexOf("\"", nameStartPos, StringComparison.Ordinal);
            if (startQuotePos < 0)
            {
                return null;
            }

            var endQuotePos = connectionSettingString.IndexOf("\"", startQuotePos + 1, StringComparison.Ordinal);
            if (endQuotePos < 0)
            {
                return null;
            }

            var value = connectionSettingString.Substring(startQuotePos + 1, endQuotePos - startQuotePos - 1);
            return value;
        }
    }
}