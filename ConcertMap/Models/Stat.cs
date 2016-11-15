using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConcertMap.Models
{
    public class Stat
    {

        public List<ChartModel> CityStat { get; set; }

        public List<ChartModel> CountryStat { get; set; }

        public List<ChartModel> RegionStat { get; set; }

    }
}