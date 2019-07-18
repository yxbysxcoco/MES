using MES.Const;
using MES.Models;
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
        public ActionResult Index(EntityBase entity)
        {
            SearchModels searchModels = new SearchModels();
            foreach (var property in entity.GetType().GetProperties().GetPropertysWhereAttr<ColumnAttribute>())
            {
                SearchModel searchModel = new SearchModel()
                {
                    Alias = property.GetCustomAttribute<DisplayAttribute>().Name,
                    id= entity.GetType().Name+"_"+property.Name,
                    PropertyType=property.PropertyType.Name,
                };
                foreach (var property1 in entity.GetType().GetProperties().GetPropertysWhereAttr<ForeignKeyAttribute>())
                {
                    if (property.Name.Equals(property1.GetCustomAttribute<ForeignKeyAttribute>().Name))
                    {
                        searchModel.SearchType = SearchType.Select;
                        searchModel.ParamUrl = "";
                    }
                }
                if (property.PropertyType.Equals(typeof(System.DateTime)))
                {
                    searchModel.SearchType = SearchType.DatePicker;
                }
                searchModels.Add(searchModel);
            }
            return View();
        }
    }
}