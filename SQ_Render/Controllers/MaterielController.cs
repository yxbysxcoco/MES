using SQ_DB_Framework.DataModel;
using SQ_DB_Framework.Entities;
using SQ_Render.Const;
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
    public class MaterielController : Controller
    {
        // GET: Materiel
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SelectMateriel()
        {

            DataTable dataTable = new DataTable();
            var entities = dataTable.GetEntities<ToolEquipment>();
            dataTable.BuildRepalceDataTable(entities, t => DataTable.Repalce(t.TypeId, t.ToolEquipmentType.Name), t => DataTable.Repalce(t.MoneyUnitId, t.MoneyUnit.Name));
            dataTable.Columns[0][0].SetHasQRCode(true)
                .SetIsSortable(true);

            dataTable.PageIndex = 1;
            dataTable.PageSize = 10;
            dataTable.Limits = new int[3] { 10, 15, 20 };
            dataTable.TableName = "工装";
            var typeName = new TextInput("ToolEquipmentType_Name", "类型名称");
            var material = new TextInput("ToolEquipment_MaterialId", "物料编号");
            var datePicker = new DatePicker("ToolEquipment_DateAdded", "生产日期")
            {
                IsRange = true
            };
            var select = new Select("单位")
            {
                Id = "ToolEquipment_MeterageUnit",
                Options = entities.Select(te => te.MeterageUnit).Distinct()
                .ToDictionary(mu => mu.MeterageUnitId.ToString(), mu => mu.Name)
            };

            var button = new SubmitBtn("SearchForm");
            var resetBtn = new Button("重置");
            resetBtn.AddEventMethod("click", "lemon.resetForm('SearchForm')");

            var form = new TableForm("SearchForm", "t1");
            var formRow = new FormRow();
            var hiddenPanel = new HiddenPanel("SearchForm");
            var formRow1 = new FormRow();


            formRow.AddChildElement(material);

            formRow1.AddChildElement(typeName).AddChildElement(datePicker).AddChildElement(select);

            hiddenPanel.AddChildElement(formRow1);

            formRow.AddChildElement(button).AddChildElement(resetBtn);

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
            var tableHandle = new TableHandle("ToolOperation")
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
            table.Col = new Col(Position.quarter, Position.threeFourths);
            var tree = new TableSelectorTree<Department>("t1", "table_test", GetTreeTest(), dep => dep.Name)
            {
                Col = new Col(Position.zero, Position.quarter)
            };
            var grid = new Grid();
            var submitBtn = new Button("批量增加");
            submitBtn.AddEventMethod("click", "saveParamAndCloseLayer()");

            grid.AddChildElement(tree).AddChildElement(table);

            var div = new Container();

            div.AddChildElement(form).AddChildElement(submitBtn).AddChildElement(grid);

            return View(div);
        }
        public List<TreeNode> GetTreeTest()
        {
            var department = new Department()
            {
                Id = 0,
                Name = "公司",
                SubsidiaryDepartments = new List<Department>()
                {
                    new Department()
                    {
                        Id = 1,
                        Name = "研发部",
                        SubsidiaryDepartments = new List<Department>()
                        {
                            new Department()
                            {
                                Id = 2,
                                Name = "前端"
                            },
                            new Department()
                            {
                                Id = 3,
                                Name = "后端",
                                SubsidiaryDepartments = new List<Department>()
                                {
                                    new Department()
                                    {
                                        Id = 4,
                                        Name = "Web端"
                                    },
                                    new Department()
                                    {
                                        Id = 5,
                                        Name = "大数据"
                                    }
                                }
                            },
                            new Department()
                            {
                                Id = 6,
                                Name = "IOT"
                            }
                        }
                    },
                    new Department()
                    {
                        Id = 7,
                        Name = "财务部",
                        SubsidiaryDepartments = new List<Department>()
                        {
                            new Department()
                            {
                                Id = 8,
                                Name = "回款部门"
                            },
                            new Department()
                            {
                                Id = 9,
                                Name = "会计部门"
                            }
                        }
                    }

                }
            };
            return TreeNode.GetTreeList(new List<Department>() { department }, d => d.SubsidiaryDepartments, d => d.Name, d => d.Id.ToString());
        }
    }
}