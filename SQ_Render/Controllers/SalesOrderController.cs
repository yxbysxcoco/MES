using SQ_DB_Framework.DataModel;
using SQ_DB_Framework.Entities;
using SQ_Render.Models.View;
using SQ_Render.Models.View.Components;
using SQ_Render.Models.View.Containers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SQ_Render.Controllers
{
    public class SalesOrderController : Controller
    {
        public ActionResult OrderMaintenance()
        {
            var dataTable = new DataTable();
            var entities = dataTable.GetEntities<SalesOrder>();

            dataTable.BuildDataTable(entities);
            var typeName = new TextInput("OrderCode", "订单编号");

            var material = new TextInput("MaterialId", "材料");
            var datePicker = new DatePicker("DeliverTime", "交付日期");
            var select = new Select("人员")
            {
                Id = "SalesPeople",
                Options = entities.ToDictionary(so => so.SalesPersonId.ToString(), so => so.SalesPerson.Name.ToString())
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
            var formRow1 = new FormRow()
            {
                IsHiddenRow = true
            };



            formRow.AddChildElement(typeName);
            formRow1.AddChildElement(material);
            formRow1.AddChildElement(datePicker);
            formRow1.AddChildElement(select);

            formRow.AddChildElement(showBtn);
            formRow.AddChildElement(button);
            formRow.AddChildElement(resetBtn);

            form.AddChildElement(formRow);
            form.AddChildElement(formRow1);

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
                        },
                        new HandleItem(){
                            Alias = "打印",
                            EventName = "lemon.previewPrint(1)",
                            BtnColor = "success"
                        }
                    },
            };
            var tableHandle = new TableHandle("h")
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

            div.AddChildElement(form);
            div.AddChildElement(tableHandle);
            div.AddChildElement(batchHandle);
            div.AddChildElement(table);

            return View(div);
        }
        public ActionResult AddSalesOrder()
        {
            var card = new CardWithContext();
            var form = new Form("addSales");

            var formRow = new FormRow();

            var button = new Button("按钮")
            {
                Col = new Col(Position.quarter, Position.quarter)
            };

            card.AddChildElement(button);

            return View(card);
        }
    }
}