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
    public static class Negotiate
    {
        [FunctionName("negotiate")]
        public static SignalRConnectionInfo NegotiateFunc(
        [HttpTrigger(AuthorizationLevel.Anonymous)]HttpRequest req,
        [SignalRConnectionInfo(HubName = "chat")]SignalRConnectionInfo connectionInfo)
        {
            return connectionInfo;
        }
    }
}
