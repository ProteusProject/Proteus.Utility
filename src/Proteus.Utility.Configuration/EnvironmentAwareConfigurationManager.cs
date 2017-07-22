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
            var value = Environment.GetEnvironmentVariable(key, EnvironmentVariableTarget.Process) ?? ConfigurationManager.AppSettings[key];

            if (null == value)
            {
                throw new ConfigurationErrorsException($"Key: {key} not found in app.config or environment variables.");
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
            return ConfigurationManager.ConnectionStrings[key];
        }
    }
}
