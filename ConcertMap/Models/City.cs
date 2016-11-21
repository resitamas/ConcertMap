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

        [JsonProperty("concerts")]
        public List<string> Concerts { get; set; }
    }
}