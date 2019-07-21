﻿using MES.Const;
using SQ_DB_Framework.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Web;

namespace MES.Models
{
    public class InputItemsModel : List<InputItemModel>
    {
        public InputItemsModel AddSearchDateFrame(PropertyInfo property)
        {
            InputItemModel searchModeStart = new InputItemModel()
            {

                Id = "Start" + property.Name,
                Alias = "开始" + property.GetCustomAttribute<DisplayAttribute>().Name,
                InputType = SQInputType.DatePicker,
                PropertyType = property.PropertyType.Name,

            };
            InputItemModel searchModelEnd = new InputItemModel()
            {
                Id = "End" + property.Name,
                Alias = "结束" + property.GetCustomAttribute<DisplayAttribute>().Name,
                InputType = SQInputType.DatePicker,
                PropertyType = property.PropertyType.Name,
            };
            this.Add(searchModeStart);
            this.Add(searchModelEnd);
            return this;
        }
        public InputItemsModel AddDateFrame(PropertyInfo property)
        {
            InputItemModel searchMode = new InputItemModel()
            {
                Id =  property.Name,
                Alias = property.GetCustomAttribute<DisplayAttribute>().Name,
                InputType = SQInputType.DatePicker,
                PropertyType = property.PropertyType.Name,
            };
            this.Add(searchMode);
            return this;
        }
        public InputItemsModel AddSelectOrInputFrame(EntityBase entity, PropertyInfo property)
        {
            InputItemModel searchModel = new InputItemModel()
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
                searchModel.InputType = SQInputType.InputText;
            }
            this.Add(searchModel);
            return this;
        }
        public InputItemsModel AddButtonFrame()
        {
            InputItemModel searchModeButton = new InputItemModel()
            {
                Id = "Submit",
                Alias = "查询",
                InputType = SQInputType.Button,
                ParamUrl = "http://localhost:51847/PageHelp/ToolEquipment/GetDataByField"
            };
            this.Add(searchModeButton);
            return this;
        }
    }
       
    
}