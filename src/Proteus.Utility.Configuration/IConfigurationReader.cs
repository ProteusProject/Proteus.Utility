using System.Configuration;

namespace Proteus.Utility.Configuration
{
    public interface IConfigurationReader
    {
        string GetAppSetting(string key);
        ConnectionStringSettings GetConnectionString(string key);
    }
}