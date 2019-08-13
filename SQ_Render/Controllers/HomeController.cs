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
            var form = new Form("form1");
            var textInput = new TextInput("userName", "请输入姓名");
            var select = new Select("一个下拉")
            {
                Options = new Dictionary<string, string>
                {
                    {"1", "1" }
                }
            };
            form.AddChildElement(textInput);
            form.AddChildElement(select);
            return View(form);
        }

        public ActionResult Table()
        {
            Dictionary<string, string> entityInfoDic = new Dictionary<string, string>();

            var sQDbSet = new SQDbSet<ToolEquipment>();
            var pageHelper = sQDbSet.GetEntitiesByCondition(1, 10, entityInfoDic, "");
            
            DataTable dataTable = new DataTable();
            var entities = dataTable.GetEntities<ToolEquipment>();
            /* dataTable.AddColumnsLayer<ToolEquipment>(
                 t => DataTable.Multistage(t.Code, 2), 
                 t => DataTable.Multistage(t.Name, 2, "1"),
                 t => DataTable.NewOperation(operation, operationName, 2));
             dataTable.AddColumnsLayer<ToolEquipment>(t => t.Weight, t => t.Mark);
             dataTable.AddRow(pageHelper.AllList, t => t.Code, t => t.Weight, t => t.Mark);*/
            dataTable.BuildRepalceDataTable(entities, t => DataTable.Repalce(t.TypeId, t.ToolEquipmentType.Name), t => DataTable.Repalce(t.MoneyUnitId, t.MoneyUnit.Name));

            dataTable.Columns[0][0].SetHasQRCode(true)
                .SetIsSortable(true);

            dataTable.PageIndex = 1;
            dataTable.PageSize = 10;
            dataTable.TotalCount = pageHelper.TotalCount;
            dataTable.Limits = new int[3] { 10, 15, 20 };
            dataTable.TableName = "工装表";

            var typeName = new TextInput("ToolEquipmentType_Name", "类型名称");
            var material = new TextInput("MaterialId", "材料");
            var datePicker = new DatePicker("DateAdded", "生产日期");
            var select = new Select("代号")
            {
                Id = "Mark",
                Options = new Dictionary<string, string>
                {
                    {"ctoo", "ctoo" },
                    {"YYYY", "YYYY" },
                    {"ABOO", "ABOO" },
                }
            };

            var showBtn = new Button("展示/隐藏更多条件")
            {
                Id = "showBtn"
            };
            showBtn.AddEventMethod("click", "lemon.showHiddenPanel()");
            var button = new Button("查找");
            button.AddEventMethod("click", "lemon.fliterTable()");
            var resetBtn = new Button("重置");
            resetBtn.AddEventMethod("click", "lemon.resetForm()");

            var form = new Form("SearchForm");
            var formRow = new FormRow();
            var hiddenPanel = new HiddenPanel();
            var formRow1 = new FormRow();



            formRow.AddChildElement(typeName);

            formRow1.AddChildElement(material).AddChildElement(datePicker).AddChildElement(select);

            hiddenPanel.AddChildElement(formRow1);

            formRow.AddChildElement(showBtn).AddChildElement(button).AddChildElement(resetBtn);

            form.AddChildElement(formRow).AddChildElement(hiddenPanel);

            var table = new Table("t1", dataTable);

            var batchHandle = new TableHandle("batchOperation")
            {
                HandleItems = new List<HandleItem>()
                    {
                        new HandleItem(){
                            Alias = "批量删除",
                            Url = @"https://www.baidu.com",
                            EventName = "batchDel",
                            BtnColor = "danger"
                        }
                    },
            };
            var tableHandle = new TableHandle(operation)
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
            };
            var div = new Container();

            div.AddChildElement(form).AddChildElement(tableHandle).AddChildElement(batchHandle).AddChildElement(table);

            return View(div);
        }
    }
}