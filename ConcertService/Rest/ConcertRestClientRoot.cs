using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertService.Rest
{
    public class ConcertRestClientRoot : RestClient
    {

        protected RestRequest request;
        protected string appId;
        protected string apiVersion;

        public ConcertRestClientRoot(string url, string appId, string apiVersion) : base(url)
        {
            this.appId = appId;
            this.apiVersion = apiVersion;
        }

        protected RestRequest AddUrlParams(RestRequest request, Dictionary<string, string> urlParams)
        {
            if (urlParams != null)
            {
                foreach (var urlParam in urlParams)
                {
                    request.AddUrlSegment(urlParam.Key, urlParam.Value);
                }
            }

            return request;
        }

        protected RestRequest AddHeaders(RestRequest request, Dictionary<string, string> headers = null)
        {
            if (headers != null)
            {
                foreach (var header in headers)
                {
                    request.AddHeader(header.Key, header.Value);
                }
            }

            return request;
        }

        protected RestRequest AddQueryParams(RestRequest request, Dictionary<string, string> queryParams = null)
        {
            if (queryParams != null)
            {
                foreach (var queryParam in queryParams)
                {
                    request.AddQueryParameter(queryParam.Key, queryParam.Value);
                }
            }

            return request;
        }

        protected RestRequest AddBody(RestRequest request, object body)
        {
            if (body != null)
            {
                request.AddJsonBody(body);
            }

            return request;
        }

    }
}
