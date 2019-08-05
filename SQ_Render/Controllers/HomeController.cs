using SQ_Render.Models.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SQ_Render.Const;
using SQ_Render.Models.View.Components;
using SQ_Render.Models.View.Containers;
using SQ_DB_Framework.DataModel;
using SQ_DB_Framework.Entities;
using SQ_DB_Framework.SQDBContext;

namespace SQ_Render.Controllers
{
    public class HomeController : Controller
    {
        private const string operation = "ToolOperation";
        private const string operationName = "操作";
        public ActionResult Index()
        {
            var button = new Button("按钮")
            {
                ConfigurableStyle = new ConfigurableStyle()
                {
                    Float = "left",
                }
            };
            return View();

        }
        public ActionResult Tem()
        {
            return View();
        }
        public ActionResult Login()
        {
            var userName = new TextInput("UserName", "用户名");
            var passWord = new PasswordInput("PassWord","密码");
            var button = new FormButton("https://localhost:44317/User/Login");
            var form = new Form("LoginForm");
            var grid = new Grid()
            {
                HasContainerStyle = true
            }; 
            form.AddChildElement(userName);
            form.AddChildElement(passWord);
            form.AddChildElement(button);
            grid.AddChildElement(form);
            return View(form);
        }


        public ActionResult GatesTest()
        {
            var textInput = new TextInput("userName", "请输入姓名");

            return View(textInput);
        }

        public ActionResult Table()
        {
            Dictionary<string, string> entityInfoDic = new Dictionary<string, string>();

            var sQDbSet = new SQDbSet<ToolEquipment>();
            var pageHelper = sQDbSet.GetEntitiesByCondition(1, 10, entityInfoDic, "");
            
            DataTable dataTable = new DataTable();

            dataTable.SetColumn<ToolEquipment>(
                t => DataTable.Multistage(t.Code, 2), 
                t => DataTable.Multistage(t.Name, 2, "1"),
                t => DataTable.NewOperation(operation, operationName, 2));
            dataTable.SetColumn<ToolEquipment>(t => t.Weight, t => t.Mark);
            dataTable.SetRow(pageHelper.AllList, t => t.Code, t => t.Weight, t => t.Mark);

            dataTable.PageIndex = 1;
            dataTable.PageSize = 10;
            dataTable.TotalCount = pageHelper.TotalCount;
            dataTable.Limits = new int[3] { 10, 15, 20 };
            dataTable.TableName = "工装表";

            var table = new Table("t1", dataTable);
            var tableHandle = new TableHandle()
            {
                HandleItems = new List<HandleItem>()
                    {
                        new HandleItem(){
                            Alias = "编辑",
                            Url = @"https://www.baidu.com",
                            EventName = "handleEdit"
                        },
                        new HandleItem(){
                            Alias = "删除",
                            Url = @"https://www.baidu.com",
                            EventName = "handleDel",
                            BtnColor = "danger"
                        }
                    },
                Id = operation,
            };
            var div = new Container();

            div.AddChildElement(tableHandle);
            div.AddChildElement(table);
            return View(div);
        }
    }
}