using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SQ_DB_Framework.Entities
{
    public class Order: EntityBase
    {
        [Key,Column]
        [Display(Name = "订单编号")]
        public string OrderCode { get; set; }
        [Display(Name = "订单名称")]
        public string Name { get; set; }
        [Display(Name = "客户")]
        public int CustomerId { get; set; }
        [Display(Name = "销售")]
        public string SalesPerson { get; set; }
        [Display(Name = "交付时间")]
        public DateTime DeliverTime { get; set; }
        [Display(Name = "订单金额")]
        public double Money { get; set; }
        [Display(Name = "状态")]
        public int Status { get; set; }

        //[ForeignKey("CustomerId")]
        public Customer customer { get; set; }

        public List<ReturnMoney> ReturnMoneys { get; set; } 
    }
}
