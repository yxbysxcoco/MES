﻿using MES.Models;
using MES.Tools;
using SQ_DB_Framework.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace MES.Controllers
{
    public class TableController: Controller
    {
        // GET: TablePartialView
        [ChildActionOnly]
        public ActionResult Table(EntityBase entity)
        {
            SchemaModel schemaModel= new SchemaModel(); 
            foreach (var property in entity.GetType().GetProperties().GetPropertysWhereAttr<ColumnAttribute>())
            {
                FieldModel fieldModel = new FieldModel()
                {
                    Alias = property.GetCustomAttribute<DisplayAttribute>().Name,
                    Length = property.SetLength()
                };
                schemaModel.Add(fieldModel);
            }
            schemaModel.RequestUrl = "http://localhost:51847/PageHelp/ToolEquipment/GetDataByField";
            return PartialView(schemaModel);
        }
    }
}