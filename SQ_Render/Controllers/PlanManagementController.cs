using SQ_DB_Framework.DataModel;
using SQ_DB_Framework.Entities;
using SQ_DB_Framework.Entities.PlanManagement;
using SQ_DB_Framework.SQDBContext;
using SQ_Render.Models.View;
using SQ_Render.Models.View.Components;
using SQ_Render.Models.View.Containers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace SQ_Render.Controllers
{
    public class PlanManagementController : Controller
    {
        public static string[] splitCondition = { "_" };
        public ActionResult Index()
        {
            DataTable dataTable = new DataTable();

            var entitiesScheme = dataTable.GetEntities<ProductionDemandScheme>();
            dataTable.BuildRepalceDataTable(entitiesScheme, so => DataTable.Repalce(so.EmployeeId, so.Employee.Name),so => DataTable.Without(so.DemandParameterId), 
                so => DataTable.Without(so.RunParameterId), so => DataTable.Without(so.CalculationParameterId));

            dataTable.Columns[0][0].SetHasQRCode(true)
                .SetIsSortable(true);

            var schemeCode = new TextInput("ProductionDemandScheme_Code", "方案编号");

            var name = new TextInput("ProductionDemandScheme_Name", "方案名称");
            var datePicker = new DatePicker("ProductionDemandScheme_CreateDate", "生产日期");
            var remark = new TextInput("ProductionDemandScheme_Remark", "备注");

            var select = new Select("创建人")
            {  
                Id = "Employee",
                Options = dataTable.GetEntities<Employee>().ToDictionary(so => so.EmployeeId.ToString(), so => so.Name.ToString())
            };

            var button = new SubmitBtn("SearchForm");
            var resetBtn = new Button("重置");
            resetBtn.AddEventMethod("click", "lemon.resetForm('SearchForm')");

            var formRow = new FormRow();
            formRow.AddChildElement(schemeCode).AddChildElement(button).AddChildElement(resetBtn);

            var formRow1 = new FormRow();
            formRow1.AddChildElement(name).AddChildElement(datePicker).AddChildElement(select).AddChildElement(remark);

            var hiddenPanel = new HiddenPanel("SearchForm");
            hiddenPanel.AddChildElement(formRow1);

            var form = new TableForm("SearchForm", "t1");
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
            div.AddChildElement(form).AddChildElement(tableHandle).AddChildElement(batchHandle).AddChildElement(table);

            return View(div);
        }
        public ActionResult AddScheme()
        {
            DataTable dataTable = new DataTable();

            var cardContextBase = new Context();
            var textBase = new Text("基本信息")
            {
                Size = 20,
                IsStrong = true
            };

            var cardContextDemand = new Context();
            var textDemand = new Text("需求参数")
            {
                Size = 20,
                IsStrong = true
            };

            var cardContextCal = new Context();
            var textCal = new Text("计算参数")
            {
                Size = 20,
                IsStrong = true
            };

            var cardContextRun = new Context();
            var textRun = new Text("运行参数")
            {
                Size = 20,
                IsStrong = true,
            };

            var hr = new Hr() { Color =Color.green};
            cardContextBase.AddChildElement(textBase).AddChildElement(hr);
            cardContextDemand.AddChildElement(textDemand).AddChildElement(hr);
            cardContextCal.AddChildElement(textCal).AddChildElement(hr);
            cardContextRun.AddChildElement(textRun).AddChildElement(hr);

            var schemeCode = new TextInput("ProductionDemandScheme_Code", "方案编号");
            var name = new TextInput("ProductionDemandScheme_Name", "方案名称");
            var remark = new TextInput("ProductionDemandScheme_Remark", "备注");
            var formRow = new FormRow();
            formRow.AddChildElement(schemeCode).AddChildElement(name).AddChildElement(remark);

            var card = new Card
            {
                Col = new Col(Position.zero, Position.threeFourths)
            };
            card.AddChildElement(cardContextBase).AddChildElement(formRow);


            var selectDemandSource = new Select("需求来源")
            {
                Id = "DemandSource",
                Options = dataTable.GetEntities<DemandSource>().ToDictionary(so => so.DemandSourceId.ToString(), so => so.Name.ToString())
            };
            var selectCalculationRange = new Select("计算范围")
            {
                Id = "CalculationRange",
                Options = dataTable.GetEntities<CalculationRange>().ToDictionary(so => so.CalculationRangeId.ToString(), so => so.TypeName.ToString())
            };
            var formRow1 = new FormRow();
            formRow1.AddChildElement(selectDemandSource).AddChildElement(selectCalculationRange);

            var card1 = new Card
            {
                Col = new Col(Position.zero, Position.threeFourths)
            };
            var selectMaterielBtn = new Button("+从销售订单中选择");
            selectMaterielBtn.AddEventMethod("click", "selectSalesOrder('SalesOrderTable')");
            var selectMaterielBtn1 = new Button("+从销售预测中选择");
            selectMaterielBtn1.AddEventMethod("click", "selectSalesOrder('SalesOrderTable')");

            dataTable.BuildRepalceDataTable<SalesOrder>(null,
                 omm => DataTable.Repalce(omm.SalesPersonId, omm.Employee.Name),
                 omm => DataTable.Repalce(omm.CustomerId, omm.Customer.Name),
                  omm => DataTable.Repalce(omm.DepartmentId, omm.Department.Name)
                 );
            dataTable.Columns[0][0].SetHasQRCode(true)
                .SetIsSortable(true);

            var table = new Table("SalesOrderTable", dataTable)
            {
                Col = new Col(Position.zero, Position.threeFourths)
            };
            card1.AddChildElement(cardContextDemand).AddChildElement(formRow1).AddChildElement(selectMaterielBtn).AddChildElement(selectMaterielBtn1).AddChildElement(table);

            var cycle = new TextInput("CalculationParameter_Cycle", "计划周期");
            var cycleCount = new TextInput("CalculationParameter_CycleCount", "计划周期数");
            var attrition = new CheckBoxInput("CalculationParameter_Attrition", "考虑损耗率");
            var yield = new CheckBoxInput("CalculationParameter_Yield", "考虑成品率");
            var safetyStock = new CheckBoxInput("CalculationParameter_SafetyStock", "考虑安全库存");
            var occupancy = new CheckBoxInput("CalculationParameter_Occupancy", "考虑已占用量");
            var invName = new CheckBoxInput("CalculationParameter_InvName", "考虑已计划量");
            var minBatch = new CheckBoxInput("CalculationParameter_MinBatch", "考虑最小批量");
            var delivergoodsed = new CheckBoxInput("CalculationParameter_Delivergoodsed", "考虑已发货量");
            var nowStock = new CheckBoxInput("CalculationParameter_NowStock", "考虑现有库存");
            var formRow2 = new FormRow();
            formRow2.AddChildElement(cycle).AddChildElement(cycleCount).AddChildElement(attrition);
            formRow2.AddChildElement(yield).AddChildElement(safetyStock).AddChildElement(occupancy);
            formRow2.AddChildElement(invName).AddChildElement(minBatch).AddChildElement(delivergoodsed);
            formRow2.AddChildElement(nowStock);

            var card2 = new Card
            {
                Col = new Col(Position.zero, Position.threeFourths)
            };
            card2.AddChildElement(cardContextCal).AddChildElement(formRow2);

            var automaticRun = new CheckBoxInput("RunParameter_AutomaticRun", "自动运行");
            var createPlan = new CheckBoxInput("RunParameter_CreatePlan ", "生成计划");
            var planPeriods = new TextInput("CalculationParameter_PlanPeriods", "计划周期");
            var formRow3 = new FormRow();
            formRow3.AddChildElement(automaticRun).AddChildElement(createPlan);
         
            var card3 = new Card
            {
                Col = new Col(Position.zero, Position.threeFourths)
            };
            card3.AddChildElement(cardContextRun).AddChildElement(formRow3);

            var card4 = new Card
            {
                Col = new Col(Position.zero, Position.threeFourths)
            };
            card4.AddChildElement(new Button("提交")
            {
                Col = new Col(Position.zero, Position.zero)
            });

            var form = new Form("AddForm");
            form.AddChildElement(card).AddChildElement(card1).AddChildElement(card2).AddChildElement(card3).AddChildElement(card4);

            var grid = new Grid();
            grid.AddChildElement(form);

            var div = new Container();
            div.AddChildElement(grid);

            return View(div);
        }

        public string Insert( Dictionary<string, string> entityInfoDic)
        {
            DemandParameter demandParameter = (DemandParameter)Tools.InsertEntity("DemandParameter", entityInfoDic);
            CalculationParameter calculationParameter = (CalculationParameter)Tools.InsertEntity("CalculationParameter", entityInfoDic);
            RunParameter runParameter = (RunParameter)Tools.InsertEntity("CalculationParameter", entityInfoDic);

            ProductionDemandScheme productionDemandScheme = new ProductionDemandScheme();
            productionDemandScheme = (ProductionDemandScheme)productionDemandScheme.SetPropertyValue("ProductionDemandScheme", entityInfoDic);
            productionDemandScheme.DemandParameterId = demandParameter.DemandParameterId;
            productionDemandScheme.RunParameterId = runParameter.RunParameterId;
            productionDemandScheme.CalculationParameterId = calculationParameter.Id;
            SQDbSet<ProductionDemandScheme> sQDbSet = new SQDbSet<ProductionDemandScheme>();
            sQDbSet.Add(productionDemandScheme);

            return productionDemandScheme.ToJSON();
        }
       
    }
}