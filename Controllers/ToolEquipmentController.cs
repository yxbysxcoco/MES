
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
        private static readonly string prefix = "Search_";
        public string GetDataByField(int? pageIndex, int? pageSize,[FromBody] Dictionary<string, string> entityInfoDic)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

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