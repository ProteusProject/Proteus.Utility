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

        public static Action<string> Logger { get; set; } = msg => { };

        public static string AppSettings(string key)
        {
            string value = null;

            foreach (var reader in AppSettingReaders.Reverse())
            {
                var readerName = reader.Method.DeclaringType?.FullName ?? "DYNAMIC_TYPE";

                Logger($"{nameof(ExtensibleSourceConfigurationManager)} attempting to read setting: [{key}] using reader: [{readerName}]");
                value = reader(key);
                if (null == value)
                {
                    Logger($"Key: [{key}] not found using reader: [{readerName}]");
                    continue;
                }

                Logger($"Key: [{key}] value: [{value}] found using reader: [{readerName}]");
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
                var readerName = reader.Method.DeclaringType?.FullName ?? "DYNAMIC_TYPE";

                Logger($"{nameof(ExtensibleSourceConfigurationManager)} attempting to read named connection string: [{key}] using reader: [{readerName}]");
                value = reader(key);
                if (null == value)
                {
                    Logger($"Named connection string: [{key}] not found using reader: [{readerName}]");
                    continue;
                }

                Logger($"Named connection string: [{key}] value: [{value}] found using reader: [{readerName}'");
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
