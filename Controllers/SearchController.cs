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
            SearchModels searchModels = new SearchModels();
            //根据主键和索引生成查询框
            foreach (var property in propertys)
            {
                if (property.PropertyType.Equals(typeof(System.DateTime)))
                {
                    searchModels = searchModels.AddDateFrame(property);
                    continue;
                }
                searchModels=searchModels.AddSelectOrInput(entity,property);
            }
            //生成一个button框
            SearchModel searchModeButton = new SearchModel()
            {
                Id = "Submit",
                Alias = "查询",
                SearchType = SearchType.Button,
                ParamUrl= "http://localhost:51847/PageHelp/ToolEquipment/GetDataByField"
            };
            searchModels.Add(searchModeButton);
            return View(searchModels);
        }
    }
}