using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertService.Rest
{
    public partial class ConcertRESTClient : ConcertRestClientRoot
    {

        public ConcertRESTClient(string url, string appid, string apiVersion) : base(url, appid, apiVersion)
        {
        }

        public async Task<IRestResponse> ExecuteRequestAsync(string path, Method method, Dictionary<string, string> urlParams = null, Dictionary<string, string> queryParams = null, object body = null, Dictionary<string, string> headers = null)
        {
            RestResponse response;
            
            request = new RestRequest(path, method);

            AddUrlParams(request, urlParams);

            AddHeaders(request, headers);


            switch (method)
            {
                case Method.GET:
                    queryParams.Add("api_version", apiVersion);
                    queryParams.Add("app_id", appId);
                    request = AddQueryParams(request, queryParams);
                    var uri = BuildUri(request);
                    response = await ExecuteGetTaskAsync(request) as RestResponse;
                    //response = await Task.Run(() => ExecuteGET()) as RestResponse;
                    break;
                case Method.POST:
                    response = await ExecutePostTaskAsync(request) as RestResponse;
                    //response = await Task.Run(() => ExecutePOST()) as RestResponse;
                    break;
                case Method.PUT:
                    response = await ExecuteTaskAsync(request) as RestResponse;
                    //response = await Task.Run(() => ExecutePUT()) as RestResponse;
                    break;
                case Method.DELETE:
                    response = await ExecuteTaskAsync(request) as RestResponse;
                    //response = await Task.Run(() => ExecuteDELETE()) as RestResponse;
                    break;
                default:
                    return null;
            }

            if (response.ErrorException != null)
            {
                throw response.ErrorException;
            }
            else
            {
                if (response.StatusCode.ToString()[0] == '4')
                {
                    throw new Exception(response.Content);
                }
            }

            return response;
        }
    }
}
