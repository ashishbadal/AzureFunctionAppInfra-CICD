using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Newtonsoft.Json;
using Microsoft.Azure.Storage.Blob;
using System.Threading.Tasks;
using AB.Demo.Models;
using Microsoft.AspNetCore.Http;

namespace AB.Demo.Output
{
    public static class StorePlayerWithContainerBlobOutput
    {
        [FunctionName(nameof(StorePlayerWithContainerBlobOutput))]
        public static async Task<IActionResult> Run(
            [HttpTrigger(
                AuthorizationLevel.Function,
                nameof(HttpMethods.Post),
                Route = null)] Player player,
            [Blob(
                "players",
                FileAccess.Write)] CloudBlobContainer cloudBlobContainer)
        {
            IActionResult result;
            if (player == null)
            {
                result = new BadRequestObjectResult("No player data in request.");
            }
            else
            {
                var blob = cloudBlobContainer.GetBlockBlobReference($"out/cloudblob-{player.NickName}.json");
                var playerBlob = JsonConvert.SerializeObject(player);
                await blob.UploadTextAsync(playerBlob);
                result = new AcceptedResult();
            }

            return result;
        }
    }
}