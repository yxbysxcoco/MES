
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace MES.Tools
{
    static class Utils
    {
        public static string ToJSON(object obj)
          {
             StringBuilder sb = new StringBuilder();
             JavaScriptSerializer json = new JavaScriptSerializer();
             json.Serialize(obj, sb);
             return sb.ToString();
         }
        public static string ObjectToJson(object obj)
          {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
            MemoryStream stream = new MemoryStream();
            serializer.WriteObject(stream, obj);
            byte[] dataBytes = new byte[stream.Length];
            stream.Position = 0;
            stream.Read(dataBytes, 0, (int) stream.Length);
            return Encoding.UTF8.GetString(dataBytes);
         }
        public static string ToJSON1(this object o)
        {
            if (o == null)
            {
                return null;
            }
            return JsonConvert.SerializeObject(o);
        }
        
    }
}
