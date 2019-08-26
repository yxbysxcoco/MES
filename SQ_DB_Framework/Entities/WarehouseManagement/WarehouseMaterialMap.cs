
using System;
using System.ComponentModel.DataAnnotations.Schema;
using DisplayAttribute = SQ_DB_Framework.Attributes.DisplayAttribute;
namespace SQ_DB_Framework.Entities.PlanManagement
{
    public class WarehouseMaterialMap
    {
        [Display("物料编号")]
        public int MaterialId { get; set; }
        [ForeignKey("MaterialId")]
        public Material Material { get; set; }
        [Display("仓库编号")]
        public int WarehouseId { get; set; }
        [ForeignKey("WarehouseId")]
        public Warehouse Warehouse { get; set; }
        [Display("数量")]
        public int Count { get; set; }
    }
}
