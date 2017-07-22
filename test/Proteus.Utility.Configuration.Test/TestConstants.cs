namespace Proteus.Utility.Configuration.Test
{
    public static class TestConstants
    {
        public const string ConnectionStringTestKey = "theTestKey";
        public const string ConnectionStringValueFromAppConfig = "the-connection-string-from-app.config-file";
        public const string ConnectionStringProviderNameFromAppConfig = "the-provider-name-from-app.config-file";

        public const string AppSettingTestKey = "theTestKey";
        public const string AppSettingsValueFromEnvironmentVariable = "value-read-from-environment";
        public const string AppSettingsValueFromAppConfig = "value-read-from-app.config-file";

        public static readonly string ConnectionStringValueOfEnvironmentVariable = $"name=\"{ConnectionStringTestKey}\" connectionString=\"{ConnectionStringValueFromEnvironmentVariable}\" providerName=\"{ConnectionStringProviderNameValueFromEnvironmentVariable}\"";
        public const string ConnectionStringProviderNameValueFromEnvironmentVariable = "the-provider-name-from-environment";
        public const string ConnectionStringValueFromEnvironmentVariable = "the-connection-string-from-environment";
    }
}