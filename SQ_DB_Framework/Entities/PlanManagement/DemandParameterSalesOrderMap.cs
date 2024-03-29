﻿using System.ComponentModel.DataAnnotations.Schema;
using DisplayAttribute = SQ_DB_Framework.Attributes.DisplayAttribute;

namespace SQ_DB_Framework.Entities.PlanManagement
{
    public class DemandParameterSalesOrderMap : EntityBase
    { 
        [Display("需求参数")]
        public int DemandParameterId { get; set; }
        [ForeignKey("DemandParameterId")]
        public DemandParameter DemandParameter { get; set; }
        [Display("订单")]
        public string OrderCode { get; set; }
        [ForeignKey("OrderCode")]
        public SalesOrder SalesOrder { get; set; }
        [Display("类型")]
        public string Type { get; set; }

    }
}
