using System.Configuration;

namespace Proteus.Utility.Configuration
{
    public static class AppConfigReader
    {
        public static string GetAppSetting(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        public static ConnectionStringSettings GetConnectionString(string key)
        {
            return ConfigurationManager.ConnectionStrings[key];
        }
    }
}