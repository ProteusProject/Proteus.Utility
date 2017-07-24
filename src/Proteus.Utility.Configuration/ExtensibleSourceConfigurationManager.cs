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
        public static IList<Expression<Func<string, string>>> AppSettingReaders = new List<Expression<Func<string, string>>> { key => AppConfigReader.GetAppSetting(key) };
        public static IList<Expression<Func<string, ConnectionStringSettings>>> ConnectionStringReaders = new List<Expression<Func<string, ConnectionStringSettings>>> { key => AppConfigReader.GetConnectionString(key) };

        public static Action<string> Logger { get; set; } = msg => Debug.WriteLine(msg);

        public static string AppSettings(string key)
        {
            string value = null;

            foreach (var reader in AppSettingReaders.Reverse())
            {
                var readerName = MethodCallNameFormatter.GetFormattedName(reader);

                Logger($"{nameof(ExtensibleSourceConfigurationManager)} attempting to read setting: '{key}' using reader method: {readerName}");
                value = reader.Compile().Invoke(key);
                if (null == value)
                {
                    Logger($"Key: '{key}' not found using reader method: {readerName}");
                    continue;
                }

                Logger($"Key: '{key}' value: '{value}' found using reader method {readerName}");
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
                value = reader.Compile().Invoke(key);
                if (null == value) continue;
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
                throw new ConfigurationErrorsException($"Unable to convert string value: {stringValue} to requested type: {typeof(TReturn)}", ex);
            }
        }

        private static void ThrowOnValueNotFound(string key)
        {
            var message = $"Unable to be find key: {key} using provided configuration source(s).";
            Logger(message);

            throw new ConfigurationErrorsException(message);
        }


    }
}
