using MES.Const;
using MES.Models;
using SQ_DB_Framework;
using SQ_DB_Framework.Entities;
using SQ_DB_Framework.SQDBContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace MES.Controllers
{
    public class AddEntityController : Controller
    {
        // GET: Add
        public int Insert([FromBody] Dictionary<string, string> entityInfoDic)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var entity = assembly.CreateInstance(entityInfoDic["entityTypeName"]);

            entityInfoDic.Remove("entityTypeName");
            foreach(var prop in entity.GetType().GetProperties().Where(prop => prop.IsDefined(typeof(ColumnAttribute))))
            {
                var value = entityInfoDic[prop.Name];
                prop.SetValue(entity, prop.Convert(value));
            }
            var dbSet = typeof(SQDbSet<>).MakeGenericType(new Type[] { entity.GetType() });
            object o = Activator.CreateInstance(dbSet);
            int isSuccess = (int)dbSet.InvokeMember("Add", BindingFlags.InvokeMethod, null, o, new object[] { entity });
            return isSuccess;
        }
        public ActionResult Add(EntityBase entity)
        {
            var propertys = entity.GetType().GetProperties().Where(prop => prop.IsDefined(typeof(ColumnAttribute)));
            var addItemsModel = new InputItemsModel();
            foreach (var property in propertys)
            {
                if (property.PropertyType.Equals(typeof(DateTime)))
                {
                    //添加时间框
                    addItemsModel = addItemsModel.AddDateFrame(property);
                    continue;
                }
                addItemsModel = addItemsModel.AddSelectOrInputFrame(entity, property);
            }
            //添加一个button框
            addItemsModel.Add(new InputItemModel
            {
                Id = "addSubmit",
                Alias = "添加",
                ParamUrl = "http://localhost:51847/AddEntity/Insert",
                InputType = SQInputType.Button
            });
            ViewBag.entityTypeName = entity.GetType().FullName;
            return PartialView(addItemsModel);
        }
    }
}