using Newtonsoft.Json;

namespace Cards.Entity
{
    public static class Serializer
    {
        public static T Deserialize<T>(string content) where T : class
        {
            return JsonConvert.DeserializeObject<T>(content);
        }
    }
}
