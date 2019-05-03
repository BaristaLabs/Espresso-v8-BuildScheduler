namespace Espresso_v8_BuildScheduler.Triggers
{
    using Espresso_v8_BuildScheduler.Actors;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Threading.Tasks;

    public static class QueueBuildTimer
    {
        [FunctionName("QueueBuildTimer")]
        public static async Task Run(
            [TimerTrigger("0 0 */8 * * *")]TimerInfo myTimer,
            ExecutionContext context,
            ILogger log
            )
        {
            if (myTimer.IsPastDue)
            {
                log.LogInformation("Timer is running late!");
            }

            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            await BuildManager.QueueBuild(context, log);
        }
    }
}
