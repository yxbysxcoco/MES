﻿using MES.Config;
using SQ_DB_Framework;
using SQ_DB_Framework.DataModel;
using SQ_DB_Framework.Entities;
using SQ_DB_Framework.SQDBContext;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;

namespace MES.Controllers
{
    public class ToolEquipmentController : Controller
    {
        //id前缀名
        private static string prefix = "Search_";

        /* public int SaveData()
         {
             Stopwatch sw = new Stopwatch();
             sw.Start();
             MyDataTable dataTables = new MyDataTable(new List<Column>(), new List<Row>(), new List<Row>());
             DBTable dBtable = new ToolEquipment();
             NewDataRows(dataTables);
             dBtable.SaveData(dataTables);
             return 0;
         }*/
        /* public ActionResult Index(int? page)
         {
             Stopwatch sw = new Stopwatch();
             sw.Start();
             DBTable dBtable = new ToolEquipment();
             IEnumerable<ToolEquipment> toolEquipments = dBtable.GetAllData<ToolEquipment>();
             TimeSpan timeSpan1 = sw.Elapsed; //  获取总时间
             Debug.WriteLine("执行时间1：" + timeSpan1.TotalMilliseconds + " 毫秒");
             PageHelper<ToolEquipment> pageHelper = new PageHelper<ToolEquipment>(toolEquipments, page - 1 ?? 0, pageSize);
             sw.Stop();
             return Json(pageHelper);
         }*/
        /*private static DataTable NewDataRows(DataTable dataTables)
        {
            for (int i = 0; i < 100; i++)
            {
                Row row = new Row
                {
                    "TG06000" + i,
                    "1.0" ,
                    "刀具"+i,
                    "螺丝刀" + i,
                    "20*30"+i,
                    "钢"+i,
                    "2",
                    "ctoo",
                    "使用",
                    "个",
                    "元",
                    "6",
                    "10",
                    "15",
                    "20",
                    "库房"+i,
                    "制造商"+i,
                    DateTime.Now.ToString(),
                    "A001",
                    "检验单位"+i,
                    "4000",
                    "20",
                    "5",
                    "供应商"+i
                };
                dataTables.Add(row);
            }
            return dataTables;
        }*/

        public string GetDataByField(int? pageIndex, int? pageSize,[FromBody] Dictionary<string, string> entityInfoDic)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
           /* int? pageIndex = entityInfoDic["pageIndex"];
            int? pageSize = entityInfoDic["pageSize"];*/
            var assembly = Assembly.Load("SQ_DB_Framework");
            var entity = assembly.CreateInstance(entityInfoDic["entityTypeName"]);
            entityInfoDic.Remove("entityTypeName");
            var dbSet = typeof(SQDbSet<>).MakeGenericType(new Type[] { entity.GetType() });
            object o = Activator.CreateInstance(dbSet);
            var entities1 =dbSet.InvokeMember("GetAllEntities", BindingFlags.InvokeMethod, null, o, new object[] { });
            var entities2 = dbSet.InvokeMember("SelectByWhere", BindingFlags.InvokeMethod, null, o, new object[] { entities1, entityInfoDic ?? new Dictionary<string, string>(), prefix });
            var pageHelper = dbSet.InvokeMember("GetEntities", BindingFlags.InvokeMethod, null, o, new object[] { pageIndex ?? 1, pageSize ?? 10, entities2 });

            TimeSpan timeSpan1 = sw.Elapsed; //  获取总时间+
            Debug.WriteLine("FindUpcomingDinners()执行时间：" + timeSpan1.TotalMilliseconds + " 毫秒");

            return pageHelper.ToJSON1();
        }
    }
    
}