using System.Configuration;

namespace Proteus.Utility.Configuration
{
    public static class ConnectionStringSettingParser
    {
        public static ConnectionStringSettings Parse(string connectionSettingString)
        {
            return new ConnectionStringSettings(
                ParseNameFrom(connectionSettingString),
                ParseConnectionStringFrom(connectionSettingString),
                ParseProviderNameFrom(connectionSettingString)
                );
        }

        private static string ParseProviderNameFrom(string connectionSettingString)
        {
            throw new System.NotImplementedException();
        }

        private static string ParseConnectionStringFrom(string connectionSettingString)
        {
            throw new System.NotImplementedException();
        }

        private static string ParseNameFrom(string connectionSettingString)
        {
            throw new System.NotImplementedException();
        }
    }
}