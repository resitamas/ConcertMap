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

        public IRestResponse ExecuteRequest(string path, Method method, Dictionary<string, string> urlParams = null, Dictionary<string, string> queryParams = null, object body = null, Dictionary<string, string> headers = null)
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
                    response = ExecuteAsGet(request, "GET") as RestResponse;
                    break;
                case Method.POST:
                    response = ExecuteAsPost(request, "POST") as RestResponse;
                    break;
                case Method.PUT:
                    response = Execute(request) as RestResponse;
                    break;
                case Method.DELETE:
                    response = Execute(request) as RestResponse;
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
