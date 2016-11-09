using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertService.Models
{
    public class Event
    {

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("datetime")]
        public DateTime Date { get; set; }

        [JsonProperty("venue")]
        public Venue Venue { get; set; }

    }
}
