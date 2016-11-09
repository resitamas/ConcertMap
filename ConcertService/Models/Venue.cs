using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertService.Models
{
    public class Venue
    {

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("longitude")]
        public double Long { get; set; }

        [JsonProperty("latitude")]
        public double Lat { get; set; }

        [JsonProperty("country")]
        public string Counttry { get; set; }


    }
}
