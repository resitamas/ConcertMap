using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConcertMap.Models
{
    public class City
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("venue")]
        public Dictionary<string,List<string>> VenueDictionary { get; set; }
    }
}