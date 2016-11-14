using ConcertService.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ConcertService.Rest
{
    public partial class RestHelper : HelperRoot
    {

        public async Task<List<Event>> GetEventsAsync(string artistName, DateTime? fromDate = null, DateTime? toDate = null)
        {

            try
            {
                RestResponse response = await client.ExecuteRequestAsync(Uri.EscapeUriString("artists/" + artistName + "/events.json"), Method.GET, null, CreateQueryParams(fromDate, toDate)) as RestResponse;

                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new ConcertException("Nincs ilyen előadó");
                }

                return Newtonsoft.Json.JsonConvert.DeserializeObject<List<Event>>(response.Content);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
