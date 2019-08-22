using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DisplayAttribute = SQ_DB_Framework.Attributes.DisplayAttribute;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace SQ_DB_Framework.Entities
{
    public class OrderMaterialMap : EntityBase
    { 
        [Display("物料")]
        public int MaterialId { get; set; }
        [Display("订单编号")]
        public string OrderCode { get; set; }
        [Display("交付时间")]
        public DateTime DeliveryTime { get; set; }
        [Display("单价")]
        public double UnitPrice { get; set; }
        [Display("数量")]
        public double Count { get; set; }
        [Display("折扣")]
        public double Discount { get; set; }
        [Display("总价")]
        public double TotalPrice { get => UnitPrice * Count; }
        [Display("备注")]
        public string Remarks { get; set; }

        [ForeignKey("MaterialId")]
        public Material Material { get; set; }
    }
}
