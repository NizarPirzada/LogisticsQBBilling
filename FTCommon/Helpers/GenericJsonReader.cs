using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTCommon.Helpers
{
    public static class GenericJsonReader
    {
        public static List<T> ConvertJsonToObject<T>(this string path)
        {
            using (StreamReader file = new StreamReader(path))
            {
                try
                {
                    string json = file.ReadToEnd();
                    var serializerSettings = new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    };
                    return JsonConvert.DeserializeObject<List<T>>(json, serializerSettings);
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
    }
}
