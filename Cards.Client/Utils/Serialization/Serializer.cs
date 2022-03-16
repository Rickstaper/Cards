using Newtonsoft.Json;

namespace Cards.Client.Utils.Serialization
{
    public static class Serializer
    {
        public static T Deserialize<T>(string content) where T : class
        {
            return JsonConvert.DeserializeObject<T>(content);
        }

        public static string Serialize<T>(T model)
        {
            return JsonConvert.SerializeObject(model);
        }
    }
}
