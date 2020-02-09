#region License

/*
*
*  Copyright (c) 2020 Stephen A. Bohlen
*
*  Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"),
*  to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense,
*  and/or sell copies of the Software, and to permit persons to whom the Software is *furnished to do so, subject to the following conditions:
* 
*  The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
*
*  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
*  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
*  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS
*  IN THE SOFTWARE.
*
*/

#endregion

using System;
using System.Configuration;

namespace Proteus.Utility.Configuration
{
    public static class ConnectionStringSettingString
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