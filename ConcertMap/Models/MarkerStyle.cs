using Newtonsoft.Json;

namespace ConcertMap.Models
{
    public class MarkerStyle
    {

        [JsonProperty("fill")]
        public string Color { get; set; }


        [JsonProperty("r")]
        public double Sugar { get; set; }

        [JsonProperty("stroke")]
        public string Border { get; set; }

    }
}