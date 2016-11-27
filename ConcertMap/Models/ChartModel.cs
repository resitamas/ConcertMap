using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConcertMap.Models
{
    public class ChartModel
    {
        [JsonProperty("y")]
        public int Y { get; set; }

        [JsonProperty("indexLabel")]
        public string IndexLabel
        {
            get
            {
                return Y.ToString();
            }
        }


        [JsonProperty("label")]
        public string Label { get; set; }

    }
}