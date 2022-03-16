using System;
using System.Collections.Generic;
using System.Text;
using Cards.Client.Models;
using RestSharp;

namespace Cards.Client.Utils.Api
{
    public static class ApiHandler
    {
        private static RestClient client;
        private static RestRequest request;

        public static IRestResponse SendGetRequest(string url, string endpoint)
        {
            client = new RestClient(url);
            request = new RestRequest(endpoint, Method.GET);

            request.AddHeader("Accept", "application/json");
            return client.Execute(request);
        }

        public static IRestResponse SendPostRequest(string url, string endpoint, Card card)
        {
            client = new RestClient(url);
            request = new RestRequest(endpoint, Method.POST);

            request.AddHeader("Accept", "application/json");
            request.AddJsonBody(card);

            return client.Execute(request);
        }
    }
}
