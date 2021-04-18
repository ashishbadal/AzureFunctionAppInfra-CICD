using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;

namespace AB.Demo
{
    public static class PersonTypeHttpTrigger
    {
        [FunctionName(nameof(PersonTypeHttpTrigger))]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function,  nameof(HttpMethod.Post),Route = null)] Person person,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = person.name;

            if (string.IsNullOrEmpty(name))
            {
                return new BadRequestObjectResult("please provide the name in query string");
            }
            else
            {
                return new OkObjectResult($"Hello -- {name}");
            }

            //string name = req.Query["name"];

            // string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            // dynamic data = JsonConvert.DeserializeObject(requestBody);
            // name = name ?? data?.name;

            // string responseMessage = string.IsNullOrEmpty(name)
            //     ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
            //     : $"Hello, {name}. This HTTP triggered function executed successfully.";

            // return new OkObjectResult(responseMessage);
        }
    }
}
