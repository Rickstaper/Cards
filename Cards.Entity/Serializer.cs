using Newtonsoft.Json;

namespace Cards.Entity
{
    /// <summary>
    /// Class for serialization by json.
    /// </summary>
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
