using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System;

namespace SerializeManager
{
    public static class SerializeManager
    {

        static public void SaveToFile<T>(T list, string path)
        {
            if (!File.Exists(Environment.SystemDirectory + path))
            {
                FileStream fileStream = File.Create(Environment.SystemDirectory + "/" + path);
                fileStream.Dispose();
            }

            string json = JsonConvert.SerializeObject(list);
            File.WriteAllText(Environment.SystemDirectory + "/" + path, json);
        }

        static public List<T> ReadFromFile<T>(string path)
        {
            List<T> result;
            if (!File.Exists(Environment.SystemDirectory + "/" + path))
            {
                FileStream fileStream = File.Create(Environment.SystemDirectory + "/" + path);
                fileStream.Dispose();
            }

            string resultInfo = File.ReadAllText(Environment.SystemDirectory + "/" + path);
            result = JsonConvert.DeserializeObject<List<T>>(resultInfo);

            return result;
        }
        public static string ConvertModelToJson<T>(T model)
        {
            return JsonConvert.SerializeObject(model);
        }

        public static T ConvertJsonToModel<T>(string jsonString)
        {
            return JsonConvert.DeserializeObject<T>(jsonString);
        }

    }
}