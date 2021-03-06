using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Newtonsoft.Json;
using AB.Demo.Models;
using AB.Demo;
using Microsoft.AspNetCore.Http;

namespace AB.Demo.Output
{
    public static class StorePlayerWithStringBlobOutput
    {
        [FunctionName(nameof(StorePlayerWithStringBlobOutput))]
        public static IActionResult Run(
            [HttpTrigger(
                AuthorizationLevel.Function,
                "post",
                Route = null)] Player player,
            [Blob(
                "players/out/string-{rand-guid}.json",
                FileAccess.Write)] out string playerBlob)

        {
            playerBlob = default;
            IActionResult result;

            if (player == null)
            {
                result = new BadRequestObjectResult("No player data in request.");
            }
            else
            {
                playerBlob = JsonConvert.SerializeObject(player, Formatting.Indented);
                result = new AcceptedResult();
            }

            return result;
        }
    }
}