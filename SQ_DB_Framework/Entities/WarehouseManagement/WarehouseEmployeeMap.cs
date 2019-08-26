
using System;
using System.ComponentModel.DataAnnotations.Schema;
using DisplayAttribute = SQ_DB_Framework.Attributes.DisplayAttribute;
namespace SQ_DB_Framework.Entities.PlanManagement
{
    public class WarehouseEmployeeMap
    {
        [Display("管理员编号")]
        public int EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }
        [Display("仓库编号")]
        public int WarehouseId { get; set; }
        [ForeignKey("WarehouseId")]
        public Warehouse Warehouse { get; set; }
        [Display("时限")]
        public DateTime DateLimit { get; set; }
        [Display("盘点周期")]
        public string InventoryPeriod { get; set; }
        [Display("管理员排期")]
        public string Schedule { get; set; }
    }
}
