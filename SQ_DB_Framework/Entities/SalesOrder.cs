using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SQ_DB_Framework.Attributes;
using System.Linq;
using System.Text;
using DisplayAttribute = SQ_DB_Framework.Attributes.DisplayAttribute;
using SQ_DB_Framework.Entities.PlanManagement;

namespace SQ_DB_Framework.Entities
{
    public class SalesOrder : EntityBase
    {
        [Key, Display("订单编号")]
        public string OrderCode { get; set; }
        [Display("订单名称")]
        public string Name { get; set; }
        [Display("客户")]
        public int CustomerId { get; set; }
        [Display("交付时间")]
        public DateTime DeliverTime { get; set; }
        [Display("订单金额")]
        public double Money { get => OrderMaterialMaps?.Select(omm => omm.TotalPrice).Sum() ?? 0; }
        [Display("状态")]
        public int Status { get; set; }
        [Display("收货地址")]
        public string ReceivingAddress { get; set; }
        [Display("销售人员")]
        public int SalesPersonId { get; set; }
        [Display("部门")]
        public int DepartmentId { get; set; }

        [ForeignKey("SalesPersonId")]
        public Employee Employee { get; set; }
        [ForeignKey("DepartmentId")]
        public Department Department { get; set; }
        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }

        [Include]
        public List<ReturnMoney> ReturnMoneys { get; set; } 
        [Include]
        public List<OrderMaterialMap> OrderMaterialMaps { get; set; }
        [Include]
        public List<DemandParameterSalesOrderMap> DemandParameterSalesOrderMaps { get; set; }
    }
}
