
using SQ_DB_Framework.DataModel;
using SQ_DB_Framework.Entities;
using SQ_DB_Framework.SQDBContext;
using SQ_Render.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.IO;
using System.Web.Http;
using System.Web.Mvc;

namespace SQ_Render.Controllers
{
    public class ToolEquipmentController : Controller
    {

        private const string operation = "ToolOperation";
        private const string operationName = "操作";
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
            var pageHelper = sQDbSet.GetEntitiesByCondition(pageIndex ?? 1, pageSize ?? 10, entityInfoDic, "");
           
            TimeSpan timeSpan1 = sw.Elapsed;
            Debug.WriteLine("FindUpcomingDinners()执行时间：" + timeSpan1.TotalMilliseconds + " 毫秒");

            return pageHelper.ToJSON();
        }
        
        public string GetData(int? pageIndex, int? pageSize,  Dictionary<string, string> entityInfoDic)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            var sQDbSet = new SQDbSet<ToolEquipment>();
            
            //var entities = sQDbSet.GetEntitiesByContion(entityInfoDic);
            var pageHelper = sQDbSet.GetEntitiesByCondition(pageIndex ?? 1, pageSize ?? 10, entityInfoDic, "");

           

            DataTable dataTable = new DataTable();

            //dataTable.BuildRepalceDataTable(pageHelper.AllList, t =>t.Name ,t => t.Weight, t => DataTable.Repalce(t.TypeId,t.ToolEquipmentType.Name),t=>DataTable.Repalce(t.MoneyUnitId,t.MoneyUnit.Name));

            dataTable.SetColumn<ToolEquipment>(t => DataTable.Multistage(t.Code,2), t => DataTable.Multistage(t.Name,2,"1"), 
                t => DataTable.NewOperation(operation, operationName,2));
            dataTable.SetColumn<ToolEquipment>(t => t.Weight,t => t.Mark);
            dataTable.SetRow(pageHelper.AllList, t => t.Code, t => t.Weight, t => t.Mark);

            dataTable.PageIndex = pageIndex ?? 1;
            dataTable.PageSize = pageSize ?? 10;
            dataTable.TotalCount = pageHelper.TotalCount;
            dataTable.Limits =new int[3]{ 10,15, 20};
            dataTable.TableName="工装表";


            //dataTable.BuildRepalceDataTable(entities, t => t.Name, t => DataTable.Repalce(t.TypeId, t.ToolEquipmentType.Name));

            TimeSpan timeSpan1 = sw.Elapsed;
            Debug.WriteLine("FindUpcomingDinners()执行时间：" + timeSpan1.TotalMilliseconds + " 毫秒");

            return dataTable.ToJSON();
        }
    }
}