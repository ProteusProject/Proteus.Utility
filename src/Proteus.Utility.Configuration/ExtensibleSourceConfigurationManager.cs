using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Proteus.Utility.Configuration
{
    public static class ExtensibleSourceConfigurationManager
    {
        public static IList<Func<string, string>> AppSettingReaders = new List<Func<string, string>> { EnvironmentVariableConfigurationReader.GetAppSetting, AppConfigConfigurationReader.GetAppSetting };
        public static IList<Func<string, ConnectionStringSettings>> ConnectionStringReaders = new List<Func<string, ConnectionStringSettings>> { EnvironmentVariableConfigurationReader.GetConnectionString, AppConfigConfigurationReader.GetConnectionString };


        public static string AppSettings(string key)
        {
            string value = null;

            foreach (var reader in AppSettingReaders)
            {
                value = reader(key);
                if (null == value) continue;
                break;
            }

            if (null == value)
            {
                ThrowOnValueNotFound(key);
                ThrowOnValueNotFound(key);
            }

            return value;
        }

        public static ConnectionStringSettings ConnectionStrings(string key)
        {
            ConnectionStringSettings value = null;

            foreach (var reader in ConnectionStringReaders)
            {
                value = reader(key);
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
            throw new ConfigurationErrorsException($"Key: {key} not found in app.config or environment variables.");
        }


    }
}
