﻿
using MES.Const;
using MES.Models;
using Newtonsoft.Json;
using SQ_DB_Framework;
using SQ_DB_Framework.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        public static SearchModels AddDateFrame( this SearchModels searchModels, PropertyInfo property)
        {
            SearchModel searchModeStart = new SearchModel()
            {

                Id = "Start" + property.Name,
                Alias = "开始" + property.GetCustomAttribute<DisplayAttribute>().Name,
                SearchType = SearchType.DatePicker,
                PropertyType = property.PropertyType.Name,

            };
            SearchModel searchModelEnd = new SearchModel()
            {
                Id = "End" + property.Name,
                Alias = "结束" + property.GetCustomAttribute<DisplayAttribute>().Name,
                SearchType = SearchType.DatePicker,
                PropertyType = property.PropertyType.Name,
            };
            searchModels.Add(searchModeStart);
            searchModels.Add(searchModelEnd);
            return searchModels;
        }

        public static SearchModel SetSelect(this SearchModel searchModel, PropertyInfo property1)
        {
            searchModel.SearchType = SearchType.Select;
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
        public static SearchModels AddSelectOrInput(this SearchModels searchModels, EntityBase entity,  PropertyInfo property)
        {
            SearchModel searchModel = new SearchModel()
            {
                Alias = property.GetCustomAttribute<DisplayAttribute>().Name,
                Id = property.Name,
                PropertyType = property.PropertyType.Name,
            };
            foreach (var property1 in entity.GetType().GetProperties().GetPropertysWhereAttr<ForeignKeyAttribute>())
            {
                if (property.Name.Equals(property1.GetCustomAttribute<ForeignKeyAttribute>().Name))
                {
                    searchModel = searchModel.SetSelect(property1);
                    break;
                }
                searchModel.SearchType = SearchType.InputText;
            }
            searchModels.Add(searchModel);
            return searchModels;
        }
    }
}
