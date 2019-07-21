using MES.Models;
using SQ_DB_Framework.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace MES.Controllers
{
    public class AddEntityController : Controller
    {
        // GET: Add
        public ActionResult Index()
        {
            return View();
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
            addItemsModel = addItemsModel.AddButtonFrame();
            return PartialView(addItemsModel);
        }
    }
}