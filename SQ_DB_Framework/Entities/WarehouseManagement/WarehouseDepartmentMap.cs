
using System;
using System.ComponentModel.DataAnnotations.Schema;
using DisplayAttribute = SQ_DB_Framework.Attributes.DisplayAttribute;
namespace SQ_DB_Framework.Entities.PlanManagement
{
    public class WarehouseDepartmentMap
    {
        [Display("部门编号")]
        public int DepartmentId { get; set; }
        [ForeignKey("DepartmentId")]
        public Department Department { get; set; }
        [Display("仓库编号")]
        public int WarehouseId { get; set; }
        [ForeignKey("WarehouseId")]
        public Warehouse Warehouse { get; set; }
        [Display("时限")]
        public DateTime DateLimit { get; set; }
    }
}
