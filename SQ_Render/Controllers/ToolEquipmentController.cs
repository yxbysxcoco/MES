
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using SQ_DB_Framework.DataModel;
using SQ_DB_Framework.EFDbAccess;
using SQ_DB_Framework.Entities;
using SQ_DB_Framework.SQDBContext;
using SQ_Render.Models.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
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

        
        [HttpPost]
        public string GetDataByField([FromBody] Dictionary<string, string> entityInfoDic)
        {
            

            DataTable dataTable = new DataTable();

            var entities = dataTable.GetEntities<ToolEquipment>();
            dataTable.BuildRepalceDataTable(entities, t => DataTable.Repalce(t.TypeId, t.ToolEquipmentType.Name));

          

            return dataTable.ToJSON();
        }

        //分页获取数据
        [HttpPost]
        public string GetData(int? pageIndex, int? pageSize,  Dictionary<string, string> entityInfoDic)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            DataTable dataTable = new DataTable();

            dataTable.BuildRepalceDataTable(dataTable.GetEntities<ToolEquipment>(), t => DataTable.AppointPro(t.ToolEquipmentType.Name)  ,t=>DataTable.Repalce(t.MoneyUnitId,t.MoneyUnit.Name));

            /*dataTable.AddLayerLColumns<ToolEquipment>(t => DataTable.Multistage(t.Code,2), t => DataTable.Multistage(t.Name,2,"1"), 
                t => DataTable.NewOperation(operation, operationName,2));
            dataTable.AddLayerLColumns<ToolEquipment>(t => t.Weight,t => t.Mark);

            var entities = dataTable.GetEntities<ToolEquipment>(pageIndex,pageSize,entityInfoDic);

            dataTable.AddRow(entities, t => t.Code, t => t.Weight, t => t.Mark);

            dataTable.PageIndex = pageIndex ?? 1;
            dataTable.PageSize = pageSize ?? 10;
            dataTable.TotalCount = entities.Count;
            dataTable.TableName="工装表";*/

            var dbSet = new SQDbSet<ToolEquipment>();

            var entities = dbSet.GetAllEntities();

            

            /*dataTable.BuildReduceDataTable(entities, t => new { t.Code, t.RepairCycle },
                l => l.Sum(t => t.Weight),
                l => l.Average(t => t.Univalence),
                l => l.Max(t => t.Edition));*/



            TimeSpan timeSpan1 = sw.Elapsed;
            Debug.WriteLine("FindUpcomingDinners()执行时间：" + timeSpan1.TotalMilliseconds + " 毫秒");

            return dataTable.ToJSON();
        }


        //删除
        [HttpDelete]
        public string Delete(List<object> idList,string entityName)
        {
            if (idList==null||idList.Count==0)
            {
                return "失败";
            }
            
            var sQDbSet = Tools.GetSQDbSetByName(entityName);

            var propertyKey = sQDbSet.Item3.GetType().GetProperties().Where(t => t.IsDefined(typeof(KeyAttribute))).Single();

          
            foreach (var id in idList)
            {
                 propertyKey.Convert(id.ToString());               
            }

            string sql = Tools.GetDeleteSql("ToolEquipment", propertyKey.Name, idList).ToString();

            var result = sQDbSet.Item2.InvokeMember("DeleteEntitiesByKeys", BindingFlags.InvokeMethod, null, sQDbSet.Item1,
          new object[] { sql });
      
          
            return result.ToString();
        }


        //修改
        [HttpPut]
        public string Update(object id,  object entityInfoDic)
        {
            //如果请求中的FromBody未包含用户定义的数据，默认FromBody为url端口后的参数值
            /*if (id == null)
            {
                return "失败";
            }

            var sQDbSet = Tools.GetSQDbSetByName("ToolEquipment");

            var propertyKey= sQDbSet.Item3.GetType().GetProperties().Where(t => t.IsDefined(typeof(KeyAttribute))).Single();

            id = propertyKey.Convert(id.ToString());

             var entity = sQDbSet.Item2.InvokeMember("FindByEntity", BindingFlags.InvokeMethod, null, sQDbSet.Item1,
               new object[] { id });

            if (entity == null)
            {
                return "失败";
            }

            entity = entity.SetPropertyValue(entityInfoDic);

            var result = sQDbSet.Item2.InvokeMember("Update", BindingFlags.InvokeMethod, null, sQDbSet.Item1,
              new object[] { entity });*/

             id = "TG0600067";
            var sQDbSet = new SQDbSet<ToolEquipment>();
            ToolEquipment entity = sQDbSet.FindByEntity(id);

            entity.HighestStock--;

            var result = sQDbSet.Update(entity);
            Debug.WriteLine(result);

            return result.ToString();
        }

        //新增
        [HttpPost]
        public string Insert(string entityName,[FromBody] Dictionary<string, string> entityInfoDic)
        {

            var sQDbSet = Tools.GetSQDbSetByName("Department");

            var entity= sQDbSet.Item3.SetPropertyValue("Department", entityInfoDic);

            var result = sQDbSet.Item2.InvokeMember("Add", BindingFlags.InvokeMethod, null, sQDbSet.Item1,
              new object[] { entity });

            return result.ToString();
        }

    }
}