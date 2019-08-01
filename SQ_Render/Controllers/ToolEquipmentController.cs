﻿
using SQ_DB_Framework.DataModel;
using SQ_DB_Framework.Entities;
using SQ_DB_Framework.SQDBContext;
using SQ_Render.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Web.Http;
using System.Web.Mvc;

namespace SQ_Render.Controllers
{
    public class ToolEquipmentController : Controller
    {

        public string GetTableHeader()
        {
            ToolEquipment toolEquipment = new ToolEquipment();

            TableHeader fields = new TableHeader(toolEquipment.GetType().GetProperties().GetPropertysWhereAttr<ColumnAttribute>())
            {
                GetDataUrl = "http://localhost:44317/ToolEquipment/GetDataByField"
            };
            ViewBag.entityTypeName = toolEquipment.GetType().FullName;

            return fields.ToJSON();
        }

        public string GetDataByField(int? pageIndex, int? pageSize, [FromBody] Dictionary<string, string> entityInfoDic)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
           
            var sQDbSet = new SQDbSet<ToolEquipment>();
            var pageHelper = sQDbSet.GetEntitiesByContion(pageIndex ?? 1, pageSize ?? 10, entityInfoDic, "");

            TimeSpan timeSpan1 = sw.Elapsed;
            Debug.WriteLine("FindUpcomingDinners()执行时间：" + timeSpan1.TotalMilliseconds + " 毫秒");

            return pageHelper.ToJSON();
        }
        public string GetData()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            var dt = DataTable.BuildDataTable<ToolEquipment>();
           // var dt = DataTable.BuildReplaceDataTable<ToolEquipment>( t=>t.Name ,t => DataTable.Repalce(t.TypeId,t.ToolEquipmentType.Name));

            TimeSpan timeSpan1 = sw.Elapsed;
            Debug.WriteLine("FindUpcomingDinners()执行时间：" + timeSpan1.TotalMilliseconds + " 毫秒");
            return dt.ToJSON();
        }
    }
}