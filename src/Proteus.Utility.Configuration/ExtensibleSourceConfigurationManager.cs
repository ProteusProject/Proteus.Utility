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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Proteus.Utility.Configuration
{
    public static class ExtensibleSourceConfigurationManager
    {
        public static IList<Func<string, string>> AppSettingReaders = new List<Func<string, string>> { AppConfigReader.GetAppSetting };
        public static IList<Func<string, ConnectionStringSettings>> ConnectionStringReaders = new List<Func<string, ConnectionStringSettings>> { AppConfigReader.GetConnectionString };

        public static Action<string> Logger { get; set; } = msg => { Debug.WriteLine(msg); };

        public static string AppSettings(string key)
        {
            string value = null;

            foreach (var reader in AppSettingReaders.Reverse())
            {
                var readerMethod = MethodNameFormatter.GetFormattedName(reader);

                Logger($"{nameof(ExtensibleSourceConfigurationManager)} attempting to read setting: [{key}] using reader: [{readerMethod}]");
                value = reader(key);
                if (null == value)
                {
                    Logger($"Key: [{key}] not found using reader: [{readerMethod}]");
                    continue;
                }

                Logger($"Key: [{key}] value: [{value}] found using reader: [{readerMethod}]");
                break;
            }

            if (null == value)
            {
                ThrowOnValueNotFound(key);
            }

            return value;
        }

        public static ConnectionStringSettings ConnectionStrings(string key)
        {
            ConnectionStringSettings value = null;

            foreach (var reader in ConnectionStringReaders.Reverse())
            {
                var readerMethod = MethodNameFormatter.GetFormattedName(reader);

                Logger($"{nameof(ExtensibleSourceConfigurationManager)} attempting to read named connection string: [{key}] using reader: [{readerMethod}]");
                value = reader(key);
                if (null == value)
                {
                    Logger($"Named connection string: [{key}] not found using reader: [{readerMethod}]");
                    continue;
                }

                Logger($"Named connection string: [{key}] value: [{value}] found using reader: [{readerMethod}'");
                break;
            }

            if (null == value)
            {
                ThrowOnValueNotFound(key);
            }

            return value;
        }

        public static TReturn AppSettings<TReturn>(string key)
        {
            var stringValue = AppSettings(key);

            var converter = TypeDescriptor.GetConverter(typeof(TReturn));

            try
            {
                return (TReturn)converter.ConvertFromInvariantString(stringValue);
            }
            catch (FormatException ex)
            {
                throw new ConfigurationErrorsException($"Unable to convert string value: [{stringValue}] to requested type: [{typeof(TReturn)}]", ex);
            }
        }

        private static void ThrowOnValueNotFound(string key)
        {
            var message = $"Unable to be find key: [{key}] using any provided configuration source(s).";
            Logger(message);

            throw new ConfigurationErrorsException(message);
        }


    }
}
