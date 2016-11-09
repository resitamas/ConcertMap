using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertService.Rest
{
    class ConcertRESTClient : RestClient
    {

        private RestRequest request;
        private string appid;
        private string apiVersion;


        public ConcertRESTClient(string url, string appid, string apiVersion) : base(url)
        {
            this.appid = appid;
            this.apiVersion = apiVersion;
        }

        public async Task<IRestResponse> ExecuteRequest(string path, Method method, Dictionary<string, string> urlParams = null, Dictionary<string, string> queryParams = null, object body = null, Dictionary<string, string> headers = null)
        {
            RestResponse response;

            request = new RestRequest(path, method);
            if (urlParams != null)
            {
                foreach (var urlParam in urlParams)
                {
                    request.AddUrlSegment(urlParam.Key, urlParam.Value);
                }
            }
            if (headers != null)
            {
                foreach (var header in headers)
                {
                    request.AddHeader(header.Key, header.Value);
                }
            }

            switch (method)
            {
                case Method.GET:
                    queryParams.Add("api_version", apiVersion);
                    queryParams.Add("app_id", appid);
                    response = await Task.Run(() => ExecuteGET(queryParams)) as RestResponse;
                    break;
                case Method.POST:
                    response = await Task.Run(() => ExecutePOST(body)) as RestResponse;
                    break;
                case Method.PUT:
                    response = await Task.Run(() => ExecutePUT()) as RestResponse;
                    break;
                case Method.DELETE:
                    response = await Task.Run(() => ExecuteDELETE()) as RestResponse;
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

        private IRestResponse ExecuteGET(Dictionary<string, string> queryParams)
        {
            if (queryParams != null)
            {
                foreach (var queryParam in queryParams)
                {
                    request.AddQueryParameter(queryParam.Key, queryParam.Value);
                }
            }

            return ExecuteAsGet(request, "GET");
        }

        private IRestResponse ExecutePOST(object body)
        {
            if (body != null)
            {
                //request.RequestFormat = DataFormat.Json;
                request.AddJsonBody(body);
            }

            return ExecuteAsPost(request, "POST");
        }

        private IRestResponse ExecuteDELETE()
        {
            return Execute(request);
        }

        private IRestResponse ExecutePUT()
        {
            return Execute(request);
        }
    }
}
