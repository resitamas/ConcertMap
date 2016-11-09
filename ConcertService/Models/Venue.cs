using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertService.Models
{
    public class Venue
    {

        public string Id { get; set; }

        public string CityId { get; set; }

        public double Long { get; set; }

        public double Lat { get; set; }

        public string Name { get; set; }


    }
}
