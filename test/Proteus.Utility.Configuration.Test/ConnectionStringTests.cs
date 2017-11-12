﻿#region License

/*
 * Copyright © 2009-2017 the original author or authors.
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * 
 *      http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
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