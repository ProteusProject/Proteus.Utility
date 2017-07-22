using System;
using System.Configuration;

namespace Proteus.Utility.Configuration
{
    public static class EnvironmentVariableConfigurationReader
    {
        public static string GetAppSetting(string key)
        {
            return Environment.GetEnvironmentVariable(key, EnvironmentVariableTarget.Process);
        }

        public static ConnectionStringSettings GetConnectionString(string key)
        {
            var environmentValue = Environment.GetEnvironmentVariable(key, EnvironmentVariableTarget.Process);
            return string.IsNullOrEmpty(environmentValue) ? null : ConnectionStringSettingParser.Parse(environmentValue);
        }

    }
}