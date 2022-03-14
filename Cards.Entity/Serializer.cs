using Newtonsoft.Json;

namespace Cards.Entity
{
    public class Serializer
    {
        public T Deserialize<T>(string content) where T : class
        {
            return JsonConvert.DeserializeObject<T>(content);
        }

        public string Serialize<T>(T model)
        {
            return JsonConvert.SerializeObject(model);
        }
    }
}
