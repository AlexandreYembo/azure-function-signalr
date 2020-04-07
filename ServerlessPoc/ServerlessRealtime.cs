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
using System.Collections.Generic;
using ServerlessPoc.Function.Model;
using System.Linq;

namespace ServerlessPoc.Function
{
    public static class ServerlessRealtime
    {
        private static List<Vote> _votes = new List<Vote> {
            new Vote { Name = "Jair Bolsonaro", Value = 0, Filter = "jair" },
            new Vote { Name = "Michel Temer", Value = 0, Filter = "michel" },
            new Vote { Name = "Lula", Value = 0, Filter = "lula" },
            new Vote { Name = "Dilma Rouseff", Value = 0, Filter = "dilma" },
            new Vote { Name = "Fernando Henrique", Value = 0, Filter = "fernandoH" },
            new Vote { Name = "Fernando Collor", Value = 0, Filter = "fernandoC" }
        };

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

            var model = JsonConvert.DeserializeObject<Resquest>(message);

            if (model.Type == "vote")
            {
                var candidate = _votes.FirstOrDefault(f => f.Filter == model.Candidate);

                if (candidate != null)
                {
                    candidate.Value++;
                }
            }

            model.Votes = _votes;

            await signalRMessages.AddAsync(
            new SignalRMessage
            {
                Target = "ChatNotify",
                Arguments = new[] { JsonConvert.SerializeObject(model) }
            });

            return new NoContentResult();
        }
    }
}
