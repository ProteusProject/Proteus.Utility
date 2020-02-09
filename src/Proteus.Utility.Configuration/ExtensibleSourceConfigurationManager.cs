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
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Linq;

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
