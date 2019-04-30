namespace Espresso_v8_BuildScheduler.Actors
{
    using Microsoft.Azure.WebJobs;
    using Microsoft.Extensions.Logging;
    using Microsoft.TeamFoundation.Build.WebApi;
    using Microsoft.VisualStudio.Services.Common;
    using Microsoft.VisualStudio.Services.WebApi;
    using System;
    using System.Threading.Tasks;

    public static class BuildManager
    {
        public static async Task<Build> QueueBuild(ExecutionContext context, ILogger log)
        {
            var config = ConfigHelper.GetConfig(context);
            var pat = config[ConfigHelper.DevOpsPersonalAccessToken];
            var collectionUri = config[ConfigHelper.DevOpsCollectionUri];
            var project = config[ConfigHelper.DevOpsProject];
            if (!int.TryParse(config[ConfigHelper.DevOpsBuildDefinitionId], out int buildDefinitionId))
            {
                var message = $"Could not parse {ConfigHelper.DevOpsBuildDefinitionId} to an int.";
                log.LogCritical(message);
                throw new InvalidOperationException(message);
            }

            using (var devOpsConnection = new VssConnection(new Uri(collectionUri), new VssBasicCredential(string.Empty, pat)))
            {
                var buildClient = await devOpsConnection.GetClientAsync<BuildHttpClient>();
                var buildDefinition = await buildClient.GetDefinitionAsync(project, buildDefinitionId);
                if (buildDefinition == null)
                {
                    var message = $"A build definition with the value {ConfigHelper.DevOpsBuildDefinitionId} was not found.";
                    log.LogCritical(message);
                    throw new InvalidOperationException(message);
                }

                var build = await buildClient.QueueBuildAsync(new Build
                {
                    Definition = new DefinitionReference
                    {
                        Id = buildDefinition.Id
                    },
                    Project = buildDefinition.Project
                });

                return build;
            }
        }
    }
}
