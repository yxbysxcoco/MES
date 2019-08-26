using SQ_DB_Framework.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using DisplayAttribute = SQ_DB_Framework.Attributes.DisplayAttribute;

namespace SQ_DB_Framework.Entities
{
    public class Warehouse : EntityBase
    {
        [Key, Display("仓库编号")]
        public int WarehouseId { get; set; }
        [Display("仓库名")]
        public string WarehouseName { get; set; }
        [Display("地址")]
        public string Address { get; set; }
        [Display("最大库存量")]
        public int MaxInventory { get; set; }
    }
}
