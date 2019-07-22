using MES.Const;
using MES.Models;
using SQ_DB_Framework.Entities;
using SQ_DB_Framework.SQDBContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Reflection;
   public static class Tools
    {
    public static InputItemModel SetSelect(this InputItemModel searchModel, PropertyInfo property1)
    {
        searchModel.InputType = SQInputType.Select;
        searchModel.ParamUrl = "";
        var type = property1.PropertyType;
        var dbSet = typeof(SQDbSet<>).MakeGenericType(new Type[] { type });
        object o = Activator.CreateInstance(dbSet);
        var DataList = (IEnumerable<EntityBase>)dbSet.InvokeMember("GetAllEntities", BindingFlags.InvokeMethod, null, o, new object[] { });
        var dataDictionary = new Dictionary<string, string>();
        foreach (var item in DataList)
        {
            var idProperty = item.GetType().GetProperties().Where(_ => _.IsDefined(typeof(KeyAttribute))).Single();
            var nameProperty = item.GetType().GetProperty("Name");
            if (nameProperty != null)
            {
                dataDictionary.Add(idProperty.GetValue(item).ToString(), nameProperty.GetValue(item).ToString());
                continue;
            }
            dataDictionary.Add(idProperty.GetValue(item).ToString(), idProperty.GetValue(item).ToString());
        }
        searchModel.DataDictionary = dataDictionary;
        return searchModel;
    }
    public static double GetIntervalTime(this TimeSpan timeSpan, TimeSpan timeSpan1)
    {
        return (timeSpan - timeSpan1).TotalMilliseconds;
    }
}
