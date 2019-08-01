
using MES.Models;
using SQ_DB_Framework.Attributes;
using SQ_DB_Framework.Entities;
using SQ_DB_Framework.SQDBContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace MES.Controllers.Partial
{
    public class SearchController : Controller
    {
        //id前缀名
        private static readonly string prefix = "Search_";
        // GET: Search
        [ChildActionOnly]
        public ActionResult PartialForm(EntityBase entity)
        {
            var propertys = entity.GetType().GetProperties().
                Where(prop => prop.IsDefined(typeof(IndexAttribute)) || 
                prop.IsDefined(typeof(KeyAttribute)));

            InputItemsModel searchModels = new InputItemsModel();
            //根据主键和索引生成查询框
            foreach (var property in propertys)
            {
                if (property.PropertyType.Equals(typeof(System.DateTime)))
                {
                    //添加时间框
                    searchModels = searchModels.AddSearchDateFrame(property, prefix);
                    continue;
                }
                searchModels = searchModels.AddSelectOrInputFrame(entity, property, prefix);
            }

            //添加一个button框
            searchModels=searchModels.AddButtonFrame();

            ViewBag.entityTypeName = entity.GetType().FullName;

            return PartialView(searchModels);
        }

        public string GetDataByField(int? pageIndex, int? pageSize, [FromBody] Dictionary<string, string> entityInfoDic)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            var entity = Utils.GetEntiyByFullName("SQ_DB_Framework", entityInfoDic["entityTypeName"]);
            entityInfoDic.Remove("entityTypeName");

            entity.SetPropertyValue(entityInfoDic);

            Type dbSet = Utils.GetSQDbSetTypeByType(entity.GetType());
            object objectDbSet = dbSet.GetObject();

            var pageHelper = dbSet.InvokeMember("GetEntities", BindingFlags.InvokeMethod, null, objectDbSet,
                new object[] { pageIndex ?? 1, pageSize ?? 10, entityInfoDic, prefix });
            /*var pageHelper = dbSet.InvokeMember("Remove", BindingFlags.InvokeMethod, null, objectDbSet,
                new object[] { entity });*/

            TimeSpan timeSpan1 = sw.Elapsed;
            Debug.WriteLine("FindUpcomingDinners()执行时间：" + timeSpan1.TotalMilliseconds + " 毫秒");

            return pageHelper.ToJSON();
        }


    }
}