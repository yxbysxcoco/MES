using SQ_DB_Framework.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using DisplayAttribute = SQ_DB_Framework.Attributes.DisplayAttribute;

namespace SQ_DB_Framework.Entities
{
    public class WarehouseType : EntityBase
    {
        [Key, Display("仓库类型编号")]
        public int WarehouseTypeId { get; set; }
        [Display("仓库类型名")]
        public string WarehouseTypeName { get; set; }
    }
}
