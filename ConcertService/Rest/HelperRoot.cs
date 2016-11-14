using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertService.Rest
{
    public class HelperRoot
    {

        protected ConcertRESTClient client;

        public static readonly string AppID = "ConcertMap";
        public static readonly string URL = "http://api.bandsintown.com/";
        public static readonly string ApiVersion = "2.0";


        public HelperRoot()
        {
            client = new ConcertRESTClient(URL, AppID, ApiVersion);
        }


        protected Dictionary<string, string> CreateQueryParams(DateTime? fromDate, DateTime? toDate)
        {

            Dictionary<string, string> dict = new Dictionary<string, string>();

            if (fromDate.HasValue && toDate.HasValue)
            {
                dict.Add("date", fromDate.Value.ToString("yyyy-mm-dd") + "," + toDate.Value.ToString("yyyy-mm-dd"));
            }

            return dict;

        }

    }
}
