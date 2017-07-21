using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Proteus.Utility.Configuration
{
    public static class ConfigurationManager
    {
        public static string AppSettings(string key)
        {
            return Environment.GetEnvironmentVariable(key, EnvironmentVariableTarget.Process) ?? System.Configuration.ConfigurationManager.AppSettings[key];
        }
    }
}
