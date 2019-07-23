using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SQ_DB_Framework.Entities
{
    public class ReturnMoney
    {
        [Key, Column]
        public int ReturnMoneyId { get; set; }
        [Display(Name = "回款时间"), Column]
        public DateTime ReturnTime { get; set; }
        [Display(Name = "回款金额"), Column]
        public double Money { get; set; }
        [Display(Name = "回款方式"), Column]
        public string Mode { get; set; }
        [Display(Name = "结算单位"), Column]
        public string Unit { get; set; }
        [Display(Name = "备注"), Column]
        public string Remark { get; set; }
        [Display(Name = "订单编号"), Column]
        public string OrderCode { get; set; }
        [ForeignKey("OrderCode")]
        public Order Order { get; set; }
    }
}
