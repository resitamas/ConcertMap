using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConcertMap.App_Code.Helper
{
    public class Position
    {
        public double latitude { get; set; }
        public double longitude { get; set; }

        public Position(double lat=0, double lon=0)
        {
            this.latitude = lat;
            this.longitude = lon;
        }
    }
}