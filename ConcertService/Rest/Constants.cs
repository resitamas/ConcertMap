using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertService.Rest
{
    public class Constants
    {
        public static readonly string AppID = "ConcertMap";
        public static readonly string URL = "http://api.bandsintown.com/";
        public static readonly string ApiVersion = "2.0";

        public enum ConcertType
        {

            Past, Upcoming, All

        }

    }
}
