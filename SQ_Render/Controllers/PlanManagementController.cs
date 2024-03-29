﻿using SQ_DB_Framework.DataModel;
using SQ_DB_Framework.Entities;
using SQ_DB_Framework.Entities.PlanManagement;
using SQ_DB_Framework.SQDBContext;
using SQ_Render.Models.View;
using SQ_Render.Models.View.Components;
using SQ_Render.Models.View.Containers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using HttpDeleteAttribute = System.Web.Mvc.HttpDeleteAttribute;
using HttpPostAttribute = System.Web.Mvc.HttpPostAttribute;

namespace SQ_Render.Controllers
{
    public class PlanManagementController : Controller
    {
        
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
                            Url = @"https://localhost:44317/Home/Delete",
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

            var schemeCode = new TextInput();
            schemeCode.SetIdAndText<ProductionDemandScheme>(p => p.Code);
            var name = new TextInput();
            name.SetIdAndText<ProductionDemandScheme>(p => p.Name);
            var remark = new TextInput();
            remark.SetIdAndText<ProductionDemandScheme>(p => p.Remark);
            var formRow = new FormRow();
            formRow.AddChildElement(schemeCode).AddChildElement(name).AddChildElement(remark);

            var card = new Card
            {
                Col = new Col(Position.zero, Position.threeFourths)
            };
            card.AddChildElement(cardContextBase).AddChildElement(formRow);


            var selectDemandSource = new Select("需求来源")
            {
                Options = dataTable.GetEntities<DemandSource>().ToDictionary(so => so.DemandSourceId.ToString(), so => so.Name.ToString())
            };
            selectDemandSource.SetId<DemandSource>(ds => ds.DemandSourceId);
            var selectCalculationRange = new Select("计算范围")
            {
                Options = dataTable.GetEntities<CalculationRange>().ToDictionary(so => so.CalculationRangeId.ToString(), so => so.TypeName.ToString())
            };
            selectCalculationRange.SetId<CalculationRange>(ds => ds.CalculationRangeId);
            var formRow1 = new FormRow();
            formRow1.AddChildElement(selectDemandSource).AddChildElement(selectCalculationRange);

            var card1 = new Card
            {
                Col = new Col(Position.zero, Position.threeFourths)
            };
            var selectMaterielBtn = new Button("+从销售订单中选择");
            selectMaterielBtn.AddEventMethod("click", "selectSalesOrder('DemandParameterSalesOrderMap')");
            var selectMaterielBtn1 = new Button("+从销售预测中选择");
            selectMaterielBtn1.AddEventMethod("click", "selectSalesOrder('DemandParameterSalesOrderMap')");

            dataTable.BuildSubmitDataTable<SalesOrder>(null,
                 omm => DataTable.Repalce(omm.SalesPersonId, omm.Employee.Name),
                 omm => DataTable.Repalce(omm.CustomerId, omm.Customer.Name),
                  omm => DataTable.Repalce(omm.DepartmentId, omm.Department.Name)
                 );
            dataTable.Columns[0][0].SetHasQRCode(true)
                .SetIsSortable(true);

            var table = new Table(dataTable)
            {
                Col = new Col(Position.zero, Position.threeFourths)
            };
            table.SetId<DemandParameter>(d => d.DemandParameterSalesOrderMap);
            card1.AddChildElement(cardContextDemand).AddChildElement(formRow1).AddChildElement(selectMaterielBtn).AddChildElement(selectMaterielBtn1).AddChildElement(table);
            var form1 = new Form("DemandParameter");
            form1.AddChildElement(card1);

            var cycle = new TextInput();
            cycle.SetIdAndText<CalculationParameter>(t => t.Cycle);
            var cycleCount = new TextInput();
            cycleCount.SetIdAndText<CalculationParameter>(c => c.CycleCount);
            var attrition = new CheckBoxInput();
            attrition.SetIdAndText<CalculationParameter>(c => c.Attrition);
            var yield = new CheckBoxInput();
            yield.SetIdAndText<CalculationParameter>(c => c.Yield);
            var safetyStock = new CheckBoxInput();
            safetyStock.SetIdAndText<CalculationParameter>(c => c.SafetyStock);
            var occupancy = new CheckBoxInput();
            occupancy.SetIdAndText<CalculationParameter>(c => c.Occupancy);
            var invName = new CheckBoxInput();
            invName.SetIdAndText<CalculationParameter>(c => c.InvName);
            var minBatch = new CheckBoxInput();
            minBatch.SetIdAndText<CalculationParameter>(c => c.MinBatch);
            var delivergoodsed = new CheckBoxInput();
            delivergoodsed.SetIdAndText<CalculationParameter>(c => c.Delivergoodsed);
            var nowStock = new CheckBoxInput();
            nowStock.SetIdAndText<CalculationParameter>(c => c.NowStock);
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
            var form2 = new Form("CalculationParameter");
            form2.AddChildElement(card2);

            var automaticRun = new CheckBoxInput();
            automaticRun.SetIdAndText<RunParameter>(r => r.AutomaticRun);
            var createPlan = new CheckBoxInput();
            createPlan.SetIdAndText<RunParameter>(r => r.CreatePlan);
            var planPeriods = new TextInput();
            planPeriods.SetIdAndText<RunParameter>(r => r.PlanPeriods);
            var formRow3 = new FormRow();
            formRow3.AddChildElement(automaticRun).AddChildElement(createPlan);
         
            var card3 = new Card
            {
                Col = new Col(Position.zero, Position.threeFourths)
            };
            card3.AddChildElement(cardContextRun).AddChildElement(formRow3);
            var form3 = new Form("RunParameter");
            form3.AddChildElement(card3);

            var card4 = new Card
            {
                Col = new Col(Position.zero, Position.threeFourths)
            };
            card4.AddChildElement(new FormButton("保存", "https://localhost:44317/PlanManagement/Insert")
            {
                Col = new Col(Position.zero, Position.zero)
            });

            var form = new Form("ProductionDemandScheme");
            form.AddChildElement(card).AddChildElement(form1).AddChildElement(form2).AddChildElement(form3).AddChildElement(card4);

            var grid = new Grid();
            grid.AddChildElement(form);

            var div = new Container();
            div.AddChildElement(grid);

            return View(div);
        }

        public string Insert(ProductionDemandScheme ProductionDemandScheme)
        {
            SQDbSet<ProductionDemandScheme> sQDbSet = new SQDbSet<ProductionDemandScheme>();
            
            ProductionDemandScheme productionDemandScheme = ProductionDemandScheme;

            SQDbSet<DemandParameterSalesOrderMap> sQDbSetMap = new SQDbSet<DemandParameterSalesOrderMap>();
            List<DemandParameterSalesOrderMap> demandParameterSalesOrderMaps = new List<DemandParameterSalesOrderMap>();
            foreach (var item in ProductionDemandScheme.DemandParameter.DemandParameterSalesOrderMap)
            {
                demandParameterSalesOrderMaps.Add(item);
                item.DemandParameter = ProductionDemandScheme.DemandParameter;
            }
            sQDbSetMap.AddRange(demandParameterSalesOrderMaps);
            productionDemandScheme.DemandParameterId = demandParameterSalesOrderMaps[0].DemandParameter.DemandParameterId;
            productionDemandScheme.DemandParameter = null;
            productionDemandScheme.EmployeeId = 1;
            sQDbSet.Add(productionDemandScheme);

            return productionDemandScheme.ToJSON();
        }

        [HttpPost]
        public string Insert1(Dictionary<string, string> entityInfoDic)
        {
            SQDbSet<DemandParameterSalesOrderMap> sQDbSet = new SQDbSet<DemandParameterSalesOrderMap>();
            DemandParameterSalesOrderMap demandParameterSalesOrderMap = new DemandParameterSalesOrderMap();
            demandParameterSalesOrderMap = Tools.SetPropertyValue(entityInfoDic, demandParameterSalesOrderMap, pds => pds.DemandParameter);
            sQDbSet.Add(demandParameterSalesOrderMap);
            return demandParameterSalesOrderMap.ToJSON();
        }
        //删除
        [HttpDelete]
        public string Delete(string id)
        {

            SQDbSet<ProductionDemandScheme> sQDbSet = new SQDbSet<ProductionDemandScheme>();
            ProductionDemandScheme productionDemandScheme = new ProductionDemandScheme
            {
                Code = id
            };
            return sQDbSet.Remove(productionDemandScheme).ToJSON();
        }
    }
}