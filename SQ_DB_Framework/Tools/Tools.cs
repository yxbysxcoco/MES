
using Newtonsoft.Json;
using SQ_DB_Framework.Attributes;
using SQ_DB_Framework.DataModel;
using SQ_DB_Framework.Entities;
using SQ_DB_Framework.SQDBContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web.Script.Serialization;

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

            if (propertyInfo.PropertyType.Equals(typeof(int)))
            {
                return int.Parse(value);
            }
            if (propertyInfo.PropertyType.Equals(typeof(double)))
            {
                return double.Parse(value);
            }
            if (propertyInfo.PropertyType.Equals(typeof(DateTime)))
            {
                return DateTime.Parse(value);
            }
            if (propertyInfo.PropertyType.Equals(typeof(float)))
            {
                return float.Parse(value);
            }
            return value;
        }

   public static IEnumerable<PropertyInfo> GetPropertysWhereAttr<T>(this PropertyInfo[] propertyInfos) =>
            propertyInfos.Where(_ => HaveAttribute<T>(_));
    public static string ToJSON(this object o)
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
    public static int Width(this MemberInfo propertyInfo)
    {
        if (propertyInfo.MemberType.Equals(typeof(int)))
        {
            return 10*5;
        }
        if (propertyInfo.MemberType.Equals(typeof(double)))
        {
            return 10 * 5;
        }
        if (propertyInfo.MemberType.Equals(typeof(System.DateTime)))
        {
            return 15*5;
        }
        if (propertyInfo.MemberType.Equals(typeof(float)))
        {
            return 10*5;
        }
        return 64*5;
    }

    public static ColumnType GetColumnType(this MemberInfo member)
    {
        if (member.MemberType.Equals(typeof(int)))
        {
            return ColumnType.Int;
        }
        if (member.MemberType.Equals(typeof(double)))
        {
            return ColumnType.Double;
        }
        if (member.MemberType.Equals(typeof(DateTime)))
        {
            return ColumnType.DataTime;
        }
        if (member.MemberType.Equals(typeof(float)))
        {
            return ColumnType.Float;
        }
        return ColumnType.String;
    }

    public static object GetEntiyByFullName(string dllName, string fullName) => Assembly.Load(dllName).CreateInstance(fullName);

    public static object GetObject(this Type type) => Activator.CreateInstance(type);

    public static Type GetSQDbSetTypeByType(Type type) => typeof(SQDbSet<>).MakeGenericType(new Type[] { type });

    public static Tuple<object, Type, object> GetSQDbSetByName(string entityName)
    {
        
        Assembly assembly = Assembly.Load("SQ_DB_Framework");
        foreach (var entityType in assembly.GetExportedTypes().Where(t => t.BaseType == typeof(EntityBase)))
        {
            if (entityType.Name.Equals(entityName))
            {
                string proType = default;
                if (entityType.IsDefined(typeof(KeyAttribute)))
                {
                    proType = entityType.MemberType.ToString();
                }
                Type dataTableType = GetSQDbSetTypeByType(entityType);
               
                return new Tuple<object, Type,object>( dataTableType.GetObject(), dataTableType, Activator.CreateInstance(entityType));
            }
        }
        return null;
    }

    public static StringBuilder GetFindSql(string tableName, string primary, IEnumerable<object> column)
    {
        object[] arrayColumn = column.ToArray();
        StringBuilder sql = new StringBuilder("SELECT  * FROM  \"C##SXCQ_V1\".\"" + tableName + "\"  WHERE  \"" + primary + "\" in ( ");
        for (int i = 0; i < arrayColumn.Length; i++)
        {
            if (i == arrayColumn.Length - 1)
            {
                sql.Append(" \'" + arrayColumn[i] + "\' ) ");
            }
            else if (arrayColumn.Length % 1000 == 0 && i != arrayColumn.Length - 1)
            {
                sql.Append(" \'" + arrayColumn[i] + "\' )  or \"" + primary + "\" in (");
            }
            else
            {
                sql.Append(" \'" + arrayColumn[i] + "\', ");
            }
        }
        return sql;
    }

    public static StringBuilder GetDeleteSql(string tableName, string primary, IEnumerable<object> column)
    {
        object[] arrayColumn = column.ToArray();
        StringBuilder sql = new StringBuilder("DELETE FROM  \"C##SXCQ_V1\".\"" + tableName + "\"  WHERE  \"" + primary + "\" in ( ");
        for (int i = 0; i < arrayColumn.Length; i++)
        {
            if (i == arrayColumn.Length - 1)
            {
                sql.Append(" \'" + arrayColumn[i] + "\' ) ");
            }
            else if (arrayColumn.Length % 1000 == 0 && i != arrayColumn.Length - 1)
            {
                sql.Append(" \'" + arrayColumn[i] + "\' )  or \"" + primary + "\" in (");
            }
            else
            {
                sql.Append(" \'" + arrayColumn[i] + "\', ");
            }
        }
        return sql;
    }

    public static object SetPropertyValue(this object ob, Dictionary<string, string> entityInfoDic)
    {
        foreach (var pro in ob.GetType().GetProperties())
        {
            if (entityInfoDic.ContainsKey(pro.Name))
            {
                pro.SetValue(ob, entityInfoDic[pro.Name]);
            }
        }
        return ob;
    }
   

}
