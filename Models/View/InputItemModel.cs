﻿using MES.Const;
using SQ_DB_Framework.Entities;
using SQ_DB_Framework.SQDBContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web;

namespace MES.Models
{
    public class InputItemModel
    {
        //查询框类型
        public SQInputType InputType { get; set; }
        //查询框id
        public string Id { get; set; }
        //参数类型
        public string PropertyType { get; set; }
        //查询框别名
        public string Alias { get; set; }
        //获取参数的Url
        public string ParamUrl { get; set; }

        public Dictionary<string, string> DataDictionary { get; set; }

        public bool IsRequired { get; set; }
        public InputItemModel()
        {
        }
        public  InputItemModel SetSelect( PropertyInfo property1)
        {
            InputType = SQInputType.Select;
            ParamUrl = "";
            Type dbSet = Utils.GetSQDbSetTypeByType(property1.PropertyType);
            var entities = (IEnumerable<EntityBase>)dbSet.InvokeMember("GetAllEntities", BindingFlags.InvokeMethod, null, dbSet.GetObject(), new object[] { });
            var dataDictionary = new Dictionary<string, string>();
            foreach (var item in entities)
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
            DataDictionary = dataDictionary;
            return this;
        }

    }
}