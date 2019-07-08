using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MES
{
    public class JsonTool
    {
        private JObject jObject = null;
        public JsonTool(string path)
        {
            jObject = new JObject();
            using (FileStream fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + path, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader streamReader = new StreamReader(fs, System.Text.Encoding.Default))
                {
                    using (var reader = new JsonTextReader(streamReader))
                    {
                        jObject = JObject.Load(reader);
                    }
                };
            };
        }
        public List<T> GetValueList<T>(string key) where T : class
        {
            return JsonConvert.DeserializeObject<List<T>>(jObject.SelectToken(key).ToString());
        }
        public T GetValue<T>(string key) where T : class
        {
            return JsonConvert.DeserializeObject<T>(jObject.SelectToken(key).ToString());
        }
    }
}