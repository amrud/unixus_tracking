using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using UnixusTracking.Helper.Unixus.Models;

namespace UnixusTracking.Helper.Unixus
{
    class UnixusHelper
    {
        private static UnixusHelper instance = null;
        private static readonly object padlock = new object();
        private static readonly string ServerURL = " http://api.unixus.com.my/Tracking/V2/Tracking.svc/json/";

        public UnixusHelper()
        {
        }

        public static UnixusHelper Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance is null)
                    {
                        instance = new UnixusHelper();
                    }

                    return instance;
                }
            }
        }

        private (RestClient, RestRequest) GetRequest(string endpoint, Method method)
        {
            var uri = new Uri($"{ServerURL}{endpoint}");
            var restClient = new RestClient(uri);
            var request = new RestRequest(method);
            request.AddHeader("Accept", "application/json");
            return (restClient, request);
        }

        public (IEnumerable<EventListModel>, string err) GetPackageStatus(string hawbNumber)
        {
            var (restClient, request) = GetRequest($"{hawbNumber}/Details", Method.GET);
            var response = restClient.Execute(request);
            HttpStatusCode statusCode = response.StatusCode;
            if (statusCode == HttpStatusCode.OK)
            {
                var content = response.Content;
                var data = JsonConvert.DeserializeObject<GetTrackingModel>(content);
                if (data.TrackDetailsResponse.ErrorCode.Equals("0"))
                {
                    return (data.TrackDetailsResponse?.EventList, null);
                }
                else
                {
                    return (null, data.TrackDetailsResponse.ErrorMessage);
                }
            }
            else
            {
                return (null, response.StatusDescription);
            }
        }
    }
}
