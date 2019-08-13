using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DisplayAttribute = SQ_DB_Framework.Attributes.DisplayAttribute;

namespace SQ_DB_Framework.Entities
{
    public class ReturnMoney
    {
        [Key]
        public int ReturnMoneyId { get; set; }
        [Display("回款时间")]
        public DateTime ReturnTime { get; set; }
        [Display("回款金额")]
        public double Money { get; set; }
        [Display( "回款方式")]
        public string Mode { get; set; }
        [Display("结算单位")]
        public string Unit { get; set; }
        [Display("备注")]
        public string Remark { get; set; }
        [Display("订单编号")]
        public string OrderCode { get; set; }
        [ForeignKey("OrderCode")]
        public Order Order { get; set; }
    }
}
