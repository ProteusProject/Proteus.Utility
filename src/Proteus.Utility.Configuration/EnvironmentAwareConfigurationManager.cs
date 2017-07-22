using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Proteus.Utility.Configuration
{
    public static class EnvironmentAwareConfigurationManager
    {

        public static string AppSettings(string key)
        {
            var value = GetAppSettingFromEnvironmentVariable(key) ?? GetAppSettingFromAppConfigFile(key);

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

        public static ConnectionStringSettings ConnectionStrings(string key)
        {
            var value = GetConnectionStringFromEnvironmentVariable(key) ?? GetConnectionStringFromAppConfig(key);

            if (null == value)
            {
                ThrowOnValueNotFound(key);
            }

            return value;
        }

        private static void ThrowOnValueNotFound(string key)
        {
            throw new ConfigurationErrorsException($"Key: {key} not found in app.config or environment variables.");
        }

        private static string GetAppSettingFromAppConfigFile(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        private static string GetAppSettingFromEnvironmentVariable(string key)
        {
            return Environment.GetEnvironmentVariable(key, EnvironmentVariableTarget.Process);
        }

        private static ConnectionStringSettings GetConnectionStringFromAppConfig(string key)
        {
            return ConfigurationManager.ConnectionStrings[key];
        }

        private static ConnectionStringSettings GetConnectionStringFromEnvironmentVariable(string key)
        {
            var environmentValue = Environment.GetEnvironmentVariable(key, EnvironmentVariableTarget.Process);
            return string.IsNullOrEmpty(environmentValue) ? null : ConnectionStringSettingParser.Parse(environmentValue);
        }
    }
}
