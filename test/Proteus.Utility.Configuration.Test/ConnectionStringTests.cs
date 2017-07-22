namespace Proteus.Utility.Configuration.Test
{
    public static class ConnectionStringTests
    {
        public const string TestKey = "theTestKey";
        public const string AppConfigConnectionStringValue = "the-connection-string-from-app.config-file";
        public const string AppConfigProviderNameValue = "the-provider-name-from-app.config-file";

        public static readonly string EnvironmentVariableSetting = $"name=\"{TestKey}\" connectionString=\"{EnvironmentVariableConnectionStringValue}\" providerName=\"{EnvironmentVariableProviderNameValue}\"";
        public const string EnvironmentVariableProviderNameValue = "the-provider-name-from-environment";
        public const string EnvironmentVariableConnectionStringValue = "the-connection-string-from-environment";

        

    }

   
}