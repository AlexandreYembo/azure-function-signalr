using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;

namespace ServerlessPoc.Function
{
    public static class ServerlessRealtime
    {
        [FunctionName("SendMessage")]
        public static async Task<ActionResult> SendMessage(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequest req,
            [SignalR(HubName = "chat")]IAsyncCollector<SignalRMessage> signalRMessages,
            ILogger log)
        {

            var message = await req.ReadAsStringAsync();

            if (string.IsNullOrEmpty(message))
            {
                return new BadRequestObjectResult("Please pass a payload to broadcast in the request body.");
            }

            await signalRMessages.AddAsync(
            new SignalRMessage
            {
                Target = "ChatNotify",
                Arguments = new[] { message }
            });

            return new NoContentResult();
        }
    }
}
