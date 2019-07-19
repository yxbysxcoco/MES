using MES.Const;
using MES.Models;
using SQ_DB_Framework;
using SQ_DB_Framework.Attributes;
using SQ_DB_Framework.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
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
                    SearchModel searchModeStart = new SearchModel()
                    {

                        Id = "Start"+entity.GetType().Name + property.Name,
                        Alias = "开始"+property.GetCustomAttribute<DisplayAttribute>().Name,
                        SearchType = SearchType.DatePicker,
                        PropertyType = property.PropertyType.Name,
                        
                };
                    SearchModel searchModelEnd = new SearchModel()
                    {
                        Id = "End" + entity.GetType().Name + property.Name,
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
                    Id= entity.GetType().Name+"-"+property.Name,
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
                        var DataList = (IEnumerable<EntityBase>)dbSet.InvokeMember("GetAllEntities", BindingFlags.InvokeMethod, null, o, new object[] { });
                        var dataDictionary = new Dictionary<string, string>();
                        foreach (var item in DataList)
                        {
                            var idProperty = item.GetType().GetProperties().Where(_ => _.IsDefined(typeof(KeyAttribute))).Single();
                            var nameProperty = item.GetType().GetProperty("Name");
                            if (nameProperty != null)
                            {
                                dataDictionary.Add(idProperty.GetValue(item).ToString(),nameProperty.GetValue(item).ToString());
                                continue;
                            }
                            dataDictionary.Add(idProperty.GetValue(item).ToString(), idProperty.GetValue(item).ToString());
                        }
                        searchModel.DataDictionary = dataDictionary;
                        break;
                    }
                    searchModel.SearchType = SearchType.InputText;
                }
                searchModels.Add(searchModel);
            }
            //生成一个button框
            SearchModel searchModeButton = new SearchModel()
            {

                Id = "Submit",
                Alias = "查询",
                SearchType = SearchType.Button,
                ParamUrl= "http://localhost:51847/ToolEquipment/GetDataByField"
            };
            searchModels.Add(searchModeButton);
            return View(searchModels);
        }
    }
}