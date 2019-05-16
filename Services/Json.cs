using System.Collections.Generic;
using Newtonsoft.Json;

namespace Services
{
    public class Json
    {
        public Dictionary<string, string> From(string str)
        {
            return JsonConvert.DeserializeObject<Dictionary<string, string>>(str);
        }

        public List<T> From1<T>(string str) where T : class
        {
            return JsonConvert.DeserializeObject<List<T>>(str);
        }

        public string To(object obj)
        {
            return JsonConvert.SerializeObject(obj, Formatting.None,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
        }
    }
}