
using Newtonsoft.Json;
using SQ_DB_Framework.Attributes;
using SQ_DB_Framework.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Reflection;
   public static class Tools
    {

    //判断属性是否含有该特性
    public static bool HaveAttribute<T>(this PropertyInfo p)
    => p.IsDefined(typeof(T));

    //扩展ToDictionary方法
    public static Dictionary<TSource, int> MyDictionary<TSource>(this List<TSource> list)
        {
            int i = 0;
            return list.ToDictionary(_ => _, _ => i++);
        }
    public static object Convert(this PropertyInfo propertyInfo, string value)
        {
            if (propertyInfo.PropertyType.Equals(typeof(System.Int32)))
            {
                return int.Parse(value);
            }
            if (propertyInfo.PropertyType.Equals(typeof(System.Double)))
            {
                return double.Parse(value);
            }
            if (propertyInfo.PropertyType.Equals(typeof(System.DateTime)))
            {
                return DateTime.Parse(value);
            }
            if (propertyInfo.PropertyType.Equals(typeof(System.Single)))
            {
                return float.Parse(value);
            }
            return value;
        }

   public static IEnumerable<PropertyInfo> GetPropertysWhereAttr<T>(this PropertyInfo[] propertyInfos) =>
            propertyInfos.Where(_ => HaveAttribute<T>(_));
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
