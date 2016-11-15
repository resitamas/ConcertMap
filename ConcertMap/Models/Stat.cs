using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConcertMap.Models
{
    public class Stat
    {

        public Dictionary<string,int> CityStat { get; set; }

        public Dictionary<string,int> CountryStat { get; set; }

        public Dictionary<string,int> RegionStat { get; set; }

    }
}