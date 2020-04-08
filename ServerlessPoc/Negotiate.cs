using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ServerlessPoc.Function {
    public static class NegotiateFunc {
        [FunctionName ("negotiate")]
        public static SignalRConnectionInfo Negotiate (
            [HttpTrigger (AuthorizationLevel.Anonymous)] HttpRequest req, [SignalRConnectionInfo (HubName = "chat")] SignalRConnectionInfo connectionInfo) {
            return connectionInfo;
        }
    }
}