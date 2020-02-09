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

namespace Proteus.Utility.Configuration.Test
{
    public static class ConnectionStringTests
    {
        public const string TestKey = "theTestKey";
        public const string AppConfigConnectionStringValue = "the-connection-string-from-app.config-file";
        public const string AppConfigProviderNameValue = "the-provider-name-from-app.config-file";

        public static readonly string EnvironmentVariableSetting = $"name=\"{TestKey}\" connectionString=\"{EnvironmentVariableConnectionStringValue}\" providerName=\"{EnvironmentVariableProviderNameValue}\"";
        public const string EnvironmentVariableProviderNameValue = "the-provider-name-from-environment";
        public const string EnvironmentVariableConnectionStringValue = "the-connection-string-from-environment";

        public static readonly string LocalSettingsJsonSetting = $"name=\"{TestKey}\" connectionString=\"{LocalSettingsJsonConnectionStringValue}\" providerName=\"{LocalSettingsJsonProviderNameValue}\"";
        public const string LocalSettingsJsonProviderNameValue = "the-provider-name-from-local.settings.json-file";
        public const string LocalSettingsJsonConnectionStringValue = "the-connection-string-from-local.settings.json-file";

        

    }

   
}