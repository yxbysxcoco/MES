using MES.Config;
using MES.Table;
using MES.Tools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using WindowsFormsApp1.Modle;
using WindowsFormsApp1.Table;

namespace MES.Controllers
{
    public class ToolEquipmentController : Controller
    {
        private readonly ToolEquipmentRepository toolEquipment=new ToolEquipmentRepository();
        public int SaveData()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            MyDataTable dataTables = new MyDataTable(new List<Column>(), new List<Row>(), new List<Row>());
            DBTable dBtable = new ToolEquipment();
            NewDataRows(dataTables);
            dBtable.SaveData(dataTables);
            return 0;
        }
        private static MyDataTable NewDataRows(MyDataTable dataTables)
        {
            for (int i = 0; i < 12; i++)
            {
                Row row = new Row
                {
                    "TG06000" + i,
                    "1.0" ,
                    "刀具"+i,
                    "螺丝刀" + i,
                    "20*30"+i,
                    "钢"+i,
                    "2",
                    "ctoo",
                    "使用",
                    "个",
                    "元",
                    "6",
                    "10",
                    "15",
                    "20",
                    "库房"+i,
                    "制造商"+i,
                    DateTime.Now.ToString(),
                    "A001",
                    "检验单位"+i,
                    "4000",
                    "20",
                    "5",
                    "供应商"+i
                };
                dataTables.Add(row);
            }
            return dataTables;
        }
        [System.Web.Mvc.HttpGet]
        public string GetData(int? page)
        {
            Debug.WriteLine("page:" + page);
            const int pageSize = 10;
            DBTable dBtable = new ToolEquipment();
            IEnumerable<ToolEquipment> toolEquipment = dBtable.GetAllData<ToolEquipment>();
            PageHelper<ToolEquipment> pageHelper = new PageHelper<ToolEquipment>(toolEquipment, page ?? 0, pageSize);
            return Utils.ToJSON1(pageHelper);
        }
        [System.Web.Mvc.HttpGet]
        public string Index(int? page)
        {
            const int pageSize = 10;
            var upcomingDinners = toolEquipment.FindUpcomingDinners();
            var paginatedDinners = new PageHelper<Models.ToolEquipment>(upcomingDinners, page ?? 0, pageSize);
            return Utils.ToJSON1(paginatedDinners);
        }
    }
    public class ToolEquipmentRepository
    {
        private readonly DateBaseContext db = new DateBaseContext();
        public IQueryable<Models.ToolEquipment> FindUpcomingDinners()
        {
            return from toolEquipment in db.ToolEquipment
                   where toolEquipment.DateAdded < DateTime.Now
                   orderby toolEquipment.DateAdded
                   select toolEquipment;
        }
    }
}