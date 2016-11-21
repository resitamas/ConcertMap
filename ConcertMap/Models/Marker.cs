using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConcertMap.Models
{
    public class Marker
    {

        [JsonIgnore]
        public double Lat { get; set; }

        [JsonIgnore]
        public double Long { get; set; }

        [JsonProperty("latLng")]
        public List<double> Position
        {
            get
            {
                return new List<double>() { Lat, Long};
            }
        }

        [JsonProperty("name")]
        public City Name { get; set; }

        [JsonProperty("style")]
        public MarkerStyle Style { get; set; }

    }
}