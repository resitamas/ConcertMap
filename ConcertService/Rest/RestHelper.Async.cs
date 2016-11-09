using ConcertService.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertService.Rest
{
    public partial class RestHelper
    {
        ConcertRESTClient client;

        public RestHelper()
        {
            client = new ConcertRESTClient(Constants.URL, Constants.AppID, Constants.ApiVersion);
        }

        public async Task<List<Event>> GetEvents(string artistName, Constants.ConcertType type , DateTime? fromDate = null, DateTime? toDate = null)
        {

            RestResponse response = await client.ExecuteRequest(Uri.EscapeUriString("artist/" + artistName + "/events.json"),Method.GET, null, CreateQueryParams(type, fromDate, toDate)) as RestResponse;

            return Deserializer.Instance.Deserialize<List<Event>>(response);
        }

        private Dictionary<string,string> CreateQueryParams(Constants.ConcertType type, DateTime? fromDate, DateTime? toDate)
        {

            Dictionary<string, string> dict = new Dictionary<string, string>();

            switch (type)
            {
                case Constants.ConcertType.Past:
                    dict.Add("date", "1900-01-01,"+DateTime.Now.ToString("yyyy-mm-dd"));
                    break;
                case Constants.ConcertType.Upcoming:
                    dict.Add("date", "upcoming");
                    break;
                case Constants.ConcertType.All:
                    dict.Add("date", "all");
                    break;
                default:
                    if (fromDate.HasValue && toDate.HasValue)
                    {
                        dict.Add("date", fromDate.Value.ToString("yyyy-mm-dd") + "," + toDate.Value.ToString("yyyy-mm-dd"));
                    }

                    break;
            }

            return dict;

        }
    }
}
