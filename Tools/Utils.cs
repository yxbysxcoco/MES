using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
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
    //判断属性是否含有该特性
    public static bool HaveAttribute<T>(PropertyInfo p)
        {
            if (p.GetCustomAttribute(typeof(T)) != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //扩展ToDictionary方法
        public static Dictionary<TSource, int> MyDictionary<TSource>(this List<TSource> list)
        {
            int i = 0;
            return list.ToDictionary(_ => _, _ => i++);
        }
        public static IEnumerable<PropertyInfo> GetPropertysWhereAttr<T>(this PropertyInfo[] propertyInfos) => propertyInfos.Where(_ => HaveAttribute<T>(_));


    }
}
