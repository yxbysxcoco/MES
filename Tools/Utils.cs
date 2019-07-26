using MES.Const;
using MES.Models;
using SQ_DB_Framework.Entities;
using SQ_DB_Framework.SQDBContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Reflection;
   public static class Utils
    {
  
    public static double GetIntervalTime(this TimeSpan timeSpan, TimeSpan timeSpan1) => (timeSpan - timeSpan1).TotalMilliseconds;
    public static object GetEntiyByFullName(string dllName, string fullName) => Assembly.Load(dllName).CreateInstance(fullName);
    public static object GetObject(this Type type) => Activator.CreateInstance(type);

    public static Type GetSQDbSetTypeByType(Type type) => typeof(SQDbSet<>).MakeGenericType(new Type[] { type });

    public static void SetPropertyValue(this object entity, Dictionary<string, string> entityInfoDic)
    {
        foreach (var param in entityInfoDic)
        {
            foreach (var property in entity.GetType().GetProperties().GetPropertysWhereAttr<ColumnAttribute>())
            {
                if (param.Key.Equals(property.Name))
                {
                    property.SetValue(entity, property.Convert(param.Value));
                }
            }
        }
    }
}
