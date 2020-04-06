using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ServerlessPoc.Function.Model
{
    public class Resquest
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("user")]
        public string User { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("candidate")]
        public string Candidate { get; set; }
        [JsonProperty("votes")]
        public List<Vote> Votes { get; set; }
    }
}