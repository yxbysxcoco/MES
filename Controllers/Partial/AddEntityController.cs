using MES.Const;
using MES.Models;
using SQ_DB_Framework.Entities;
using SQ_DB_Framework.SQDBContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;

namespace MES.Controllers
{
    public class AddEntityController : Controller
    {
        //id前缀名
        private static readonly string prefix = "Add_";

        public int Insert([FromBody] Dictionary<string, string> entityInfoDic)
        {
            object entity = Utils.GetEntiyByFullName("SQ_DB_Framework", entityInfoDic["entityTypeName"]);
            entityInfoDic.Remove("entityTypeName");
            foreach (var prop in entity.GetType().GetProperties().Where(prop => prop.IsDefined(typeof(ColumnAttribute))))
            {
                var value = entityInfoDic[prefix + prop.Name];
                prop.SetValue(entity, prop.Convert(value));
            }
            Type dbSet = Utils.GetSQDbSetTypeByType(entity.GetType());
            object objectDbSet = dbSet.GetObject();
            int isSuccess = (int)dbSet.InvokeMember("Add", BindingFlags.InvokeMethod, null, objectDbSet, new object[] { entity });
            return isSuccess;
        }
        [ChildActionOnly]
        public ActionResult Add(EntityBase entity)
        {
            var propertys = entity.GetType().GetProperties().Where(prop => prop.IsDefined(typeof(ColumnAttribute)));
            var addItemsModel = new InputItemsModel();
            foreach (var property in propertys)
            {
                if (property.PropertyType.Equals(typeof(DateTime)))
                {
                    //添加时间框
                    addItemsModel = addItemsModel.AddDateFrame(property, prefix);
                    continue;
                }
                addItemsModel = addItemsModel.AddSelectOrInputFrame(entity, property, prefix);
            }
            //添加一个button框
            addItemsModel.Add(new InputItemModel
            {
                Id = "AddSubmit",
                Alias = "添加",
                ParamUrl = "http://localhost:51847/AddEntity/Insert",
                InputType = SQInputType.Button
            });
            ViewBag.entityTypeName = entity.GetType().FullName;
            return PartialView(addItemsModel);
        }
    }
}