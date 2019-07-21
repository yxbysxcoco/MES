using MES.Const;
using MES.Models;
using MES.Tools;
using SQ_DB_Framework;
using SQ_DB_Framework.Attributes;
using SQ_DB_Framework.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace MES.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
        public ActionResult Form(EntityBase entity)
        {
            var propertys = entity.GetType().GetProperties().Where(prop => prop.IsDefined(typeof(IndexAttribute)) || prop.IsDefined(typeof(KeyAttribute)));
            InputItemsModel searchModels = new InputItemsModel();
            //根据主键和索引生成查询框
            foreach (var property in propertys)
            {
                if (property.PropertyType.Equals(typeof(System.DateTime)))
                {
                    //添加时间框
                    searchModels = searchModels.AddDateFrame(property);
                    continue;
                }
                searchModels = searchModels.AddSelectOrInputFrame(entity, property);
            }
            //添加一个button框
            searchModels=searchModels.AddButtonFrame();
            return PartialView(searchModels);
        }

        public ActionResult AddForm(EntityBase entity)
        {
            var propertys = entity.GetType().GetProperties().Where(prop => prop.IsDefined(typeof(IndexAttribute)) || prop.IsDefined(typeof(KeyAttribute)));
            var searchModels = new InputItemsModel();
            //根据主键和索引生成查询框
            foreach (var property in propertys)
            {
                if (property.PropertyType.Equals(typeof(System.DateTime)))
                {
                    //添加时间框
                    searchModels = searchModels.AddDateFrame(property);
                    continue;
                }
                searchModels = searchModels.AddSelectOrInputFrame(entity, property);
            }
            //添加一个button框
            searchModels = searchModels.AddButtonFrame();
            return View(searchModels);
        }

    }
}