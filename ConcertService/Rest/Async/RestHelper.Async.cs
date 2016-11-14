using ConcertService.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertService.Rest
{
    public partial class RestHelper : HelperRoot
    {
        

        public async Task<List<Event>> GetEventsAsync(string artistName, DateTime? fromDate = null, DateTime? toDate = null)
        {

            RestResponse response = await client.ExecuteRequestAsync(Uri.EscapeUriString("artists/" + artistName + "/events.json"),Method.GET, null, CreateQueryParams(fromDate, toDate)) as RestResponse;

            return Newtonsoft.Json.JsonConvert.DeserializeObject<List<Event>>(response.Content);
        }

        
    }
}
