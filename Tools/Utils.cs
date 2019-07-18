
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
        public static string ObjectToJson(this object obj)
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

            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            return JsonConvert.SerializeObject(o, settings);
        }

        public static double GetIntervalTime( this TimeSpan timeSpan, TimeSpan timeSpan1)
        {
            return (timeSpan - timeSpan1).TotalMilliseconds;
        }
        public static int SetLength(this PropertyInfo propertyInfo)
        {
            if (propertyInfo.PropertyType.Equals(typeof(System.Int32)))
            {
                return 10;
            }
            if (propertyInfo.PropertyType.Equals(typeof(System.Double)))
            {
                return 10;
            }
            if (propertyInfo.PropertyType.Equals(typeof(System.DateTime)))
            {
                return 15;
            }
            if (propertyInfo.PropertyType.Equals(typeof(System.Single)))
            {
                return 10;
            }
            return 64;
        }

    }
}
