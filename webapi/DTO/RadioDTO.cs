using System;
using Newtonsoft.Json;

namespace webapi.DTO
{   
    public class RadioDTO
    {
        [JsonProperty("location")]
        public string location { get; set; } = "undefined";
    }
}
