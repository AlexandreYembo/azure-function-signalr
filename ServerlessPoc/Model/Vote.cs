using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ServerlessPoc.Function.Model
{
    public class Vote
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("value")]
        public int Value { get; set; }
        [JsonProperty("filter")]
        public string Filter { get; set; }
    }
}