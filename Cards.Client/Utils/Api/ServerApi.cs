using Cards.Client.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cards.Client.Utils.Api
{
    public static class ServerApi
    {
        public static IRestResponse GetAllCards()
        {
            return ApiHandler.SendGetRequest(@"https://localhost:5001/api/", "cards");
        }

        public static IRestResponse GetCardById(string id)
        {
            return ApiHandler.SendGetRequest(@"https://localhost:5001/api/", $"cards/{id}");
        }

        public static IRestResponse CreateCard(Card card)
        {
            return ApiHandler.SendPostRequest(@"https://localhost:5001/api/", "cards", card);
        }

        public static IRestResponse DeleteCard(string id)
        {
            return ApiHandler.SendDeleteRequest(@"https://localhost:5001/api/", $"cards/{id}");
        }

        public static IRestResponse UpdateCard(string id, CardForUpdateDto card)
        {
            return ApiHandler.SendPutRequest(@"https://localhost:5001/api/", $"cards/{id}", card);
        }
    }
}
