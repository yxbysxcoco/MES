using MES.Const;
using MES.Models;
using SQ_DB_Framework;
using SQ_DB_Framework.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Reflection;

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
                if (property.PropertyType.Equals(typeof(System.DateTime)))
                {
                    SearchModel searchModeStart = new SearchModel()
                    {

                        id = "Start"+entity.GetType().Name + property.Name,
                        Alias = "开始"+property.GetCustomAttribute<DisplayAttribute>().Name,
                        SearchType = SearchType.DatePicker,
                        PropertyType = property.PropertyType.Name,
                        
                };
                    SearchModel searchModelEnd = new SearchModel()
                    {
                        id = "End" + entity.GetType().Name + property.Name,
                        Alias = "结束" + property.GetCustomAttribute<DisplayAttribute>().Name,
                        SearchType = SearchType.DatePicker,
                        PropertyType = property.PropertyType.Name,
                    };
                    searchModels.Add(searchModeStart);
                    searchModels.Add(searchModelEnd);
                    continue;
                }
                SearchModel searchModel = new SearchModel()
                {
                    Alias = property.GetCustomAttribute<DisplayAttribute>().Name,
                    id= entity.GetType().Name+"-"+property.Name,
                    PropertyType=property.PropertyType.Name,
                };
                foreach (var property1 in entity.GetType().GetProperties().GetPropertysWhereAttr<ForeignKeyAttribute>())
                {
                    if (property.Name.Equals(property1.GetCustomAttribute<ForeignKeyAttribute>().Name))
                    {
                        searchModel.SearchType = SearchType.Select;
                        searchModel.ParamUrl = "";
                        var type = property1.PropertyType;
                        var dbSet = typeof(SQDbSet<>).MakeGenericType(new Type[] { type });
                        object o = Activator.CreateInstance(dbSet);
                        searchModel.DataList = dbSet.InvokeMember("GetAllEntities", BindingFlags.InvokeMethod, null, o, new object[] { });
                        break;
                    }
                }
                searchModel.SearchType = SearchType.InputText;
                searchModels.Add(searchModel);
            }
            return View();
        }
    }
}