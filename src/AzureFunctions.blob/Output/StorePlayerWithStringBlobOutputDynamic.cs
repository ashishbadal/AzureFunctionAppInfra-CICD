using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using AB.Demo.Models;
using AB.Demo;
using Microsoft.AspNetCore.Http;

namespace AB.Demo.Output
{
    public static class StorePlayerWithStringBlobOutputDynamic
    {
        [FunctionName(nameof(StorePlayerWithStringBlobOutputDynamic))]
        public static async Task<IActionResult> Run(
            [HttpTrigger(
                AuthorizationLevel.Function,
                "post",
                Route = null)] Player player,
            IBinder binder )

        {
            //playerBlob = default;
            IActionResult result;

            if (player == null)
            {
                result = new BadRequestObjectResult("No player data in request.");
            }
            else
            {
                // playerBlob = JsonConvert.SerializeObject(player, Formatting.Indented);
                var blobAttribute1 = new BlobAttribute($"players/out/dynamic-{player.Id}");
                var blobAttribute = new BlobAttribute($"players/out/dynamic-{player.Id}.json");
                using (var output = await binder.BindAsync<TextWriter>(blobAttribute))
                {
                    await output.WriteAsync(JsonConvert.SerializeObject(player));
                }

                result = new AcceptedResult();
            }

            return result;
        }
    }
}