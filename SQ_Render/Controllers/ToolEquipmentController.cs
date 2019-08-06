
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
using HttpDeleteAttribute = System.Web.Http.HttpDeleteAttribute;
using HttpGetAttribute = System.Web.Http.HttpGetAttribute;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;
using HttpPutAttribute = System.Web.Http.HttpPutAttribute;

namespace SQ_Render.Controllers
{
    public class ToolEquipmentController : Controller
    {

        private const string operation = "ToolOperation";
        private const string operationName = "操作";

        [HttpGet]
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

        [HttpPost]
        public string GetData([FromBody] Dictionary<string, string> entityInfoDic)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            DataTable dataTable = new DataTable();


            var entities = dataTable.GetEntities<ToolEquipment>(entityInfoDic);

            dataTable.BuildRepalceDataTable(entities, t => t.Name, t => t.Weight, t => DataTable.Repalce(t.TypeId, t.ToolEquipmentType.Name), t => DataTable.Repalce(t.MoneyUnitId, t.MoneyUnit.Name));

            //dataTable.AddRow(entities, t => t.Code, t => t.Weight, t => t.Mark);

           
            dataTable.TableName = "工装表";


            //dataTable.BuildRepalceDataTable(entityInfoDic, t => t.Name, t => DataTable.Repalce(t.TypeId, t.ToolEquipmentType.Name));

            TimeSpan timeSpan1 = sw.Elapsed;
            Debug.WriteLine("FindUpcomingDinners()执行时间：" + timeSpan1.TotalMilliseconds + " 毫秒");

            return dataTable.ToJSON();
        }

        [HttpPost]
        public string GetData(int? pageIndex, int? pageSize,  Dictionary<string, string> entityInfoDic)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            DataTable dataTable = new DataTable();

            //dataTable.BuildRepalceDataTable(pageHelper.AllList, t =>t.Name ,t => t.Weight, t => DataTable.Repalce(t.TypeId,t.ToolEquipmentType.Name),t=>DataTable.Repalce(t.MoneyUnitId,t.MoneyUnit.Name));

            dataTable.AddLayerLColumns<ToolEquipment>(t => DataTable.Multistage(t.Code,2), t => DataTable.Multistage(t.Name,2,"1"), 
                t => DataTable.NewOperation(operation, operationName,2));
            dataTable.AddLayerLColumns<ToolEquipment>(t => t.Weight,t => t.Mark);

            var entities = dataTable.GetEntities<ToolEquipment>(pageIndex,pageSize,entityInfoDic);

            dataTable.AddRow(entities, t => t.Code, t => t.Weight, t => t.Mark);

            dataTable.PageIndex = pageIndex ?? 1;
            dataTable.PageSize = pageSize ?? 10;
            dataTable.TotalCount = entities.Count;
            dataTable.TableName="工装表";


            //dataTable.BuildRepalceDataTable(entities, t => t.Name, t => DataTable.Repalce(t.TypeId, t.ToolEquipmentType.Name));

            TimeSpan timeSpan1 = sw.Elapsed;
            Debug.WriteLine("FindUpcomingDinners()执行时间：" + timeSpan1.TotalMilliseconds + " 毫秒");

            return dataTable.ToJSON();
        }

        [HttpDelete]
        public string Delete(object id)
        {
            if (id==null)
            {
                return "失败";
            }
            var sQDbSet = new SQDbSet<ToolEquipment>();
            var entity = sQDbSet.FindByEntity(id);
            return (sQDbSet.Remove(entity)).ToString();
        }

        [HttpPut]
        public string Update(object id, [FromBody] Dictionary<string, string> entityInfoDic)
        {
            //如果请求中的FromBody未包含用户定义的数据，默认FromBody为url端口后的参数值
            if (id == null)
            {
                return "失败";
            }

            var sQDbSet = new SQDbSet<ToolEquipment>();
            var entity = sQDbSet.FindByEntity(id);
            entity = (ToolEquipment)entity.SetPropertyValue(entityInfoDic);

            return sQDbSet.Update(entity).ToString();
        }

        [HttpPost]
        public string Insert([FromBody] Dictionary<string, string> entityInfoDic)
        {

            var sQDbSet = new SQDbSet<ToolEquipment>();
            var entity = new ToolEquipment();
            entity= (ToolEquipment)entity.SetPropertyValue(entityInfoDic);
            return sQDbSet.Add(entity).ToString();
        }
    }
}