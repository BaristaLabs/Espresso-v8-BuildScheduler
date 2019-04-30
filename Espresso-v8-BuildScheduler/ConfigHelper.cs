namespace Espresso_v8_BuildScheduler
{
    using Microsoft.Azure.WebJobs;
    using Microsoft.Extensions.Configuration;
    using System;

    /// <summary>
    /// Represents a helper class to obtain configuration settings from environment variables.
    /// </summary>
    public static class ConfigHelper
    {
        public const string AppServiceSlotName = "WEBSITE_SLOT_NAME";
        public const string DevOpsPersonalAccessToken = "DEVOPS_PERSONAL_ACCESS_TOKEN";
        public const string DevOpsCollectionUri = "DEVOPS_COLLECTION_URI";
        public const string DevOpsProject = "DEVOPS_PROJECT";
        public const string DevOpsBuildDefinitionId = "DEVOPS_BUILD_DEFINITION_ID";
        
        private static readonly object s_obj = new object();
        private static IConfigurationRoot s_config;

        public static IConfigurationRoot GetConfig(ExecutionContext context)
        {
            if (s_config == null)
            {
                lock (s_obj)
                {
                    if (s_config == null)
                    {
                        var slotName = Environment.GetEnvironmentVariable(AppServiceSlotName);

                        var configBuilder = new ConfigurationBuilder();

                        // If the slot name is specified, allow using a settings file named as the slot name.
                        if (!string.IsNullOrWhiteSpace(slotName))
                        {
                            configBuilder
                                .SetBasePath(context.FunctionAppDirectory)
                                .AddJsonFile($"{slotName}.settings.json", optional: true, reloadOnChange: true);
                        }

                        configBuilder.AddEnvironmentVariables();
                        s_config = configBuilder.Build();
                    }
                }
            }

            return s_config;
        }
    }
}
