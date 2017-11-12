#region License

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

using System.Configuration;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Proteus.Utility.Configuration
{
    public static class LocalSettingsJsonReader
    {
        private const string Filename = "local.settings.json";

        public static string GetAppSetting(string key)
        {
            string result;

            if (!File.Exists(Filename))
            {
                return null;
            }
            
            using (var jsonFile = File.OpenText(Filename))
            {
                var json = JsonConvert.DeserializeObject<JObject>(jsonFile.ReadToEnd());
                result = json[key]?.Value<string>();
            }

            return result;
        }

        public static ConnectionStringSettings GetConnectionString(string key)
        {
            var value = GetAppSetting(key);
            return string.IsNullOrEmpty(value) ? null : ConnectionStringSettingString.Parse(value);
        }

    }
}