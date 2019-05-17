namespace Espresso_v8_BuildScheduler.Endpoints
{
    using System.IO;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Extensions.Http;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using Espresso_v8_BuildScheduler.Models;
    using Espresso_v8_BuildScheduler.Actors;

    public static class QueueBuild
    {
        [FunctionName("QueueBuild")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ExecutionContext context,
            ILogger log
            )
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            _ = JsonConvert.DeserializeObject<QueueBuildRequest>(requestBody);

            var build = await BuildManager.QueueBuild(context, log);
            return new OkObjectResult(build.Id);
        }
    }
}
