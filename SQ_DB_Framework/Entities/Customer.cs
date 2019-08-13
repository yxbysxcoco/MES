using SQ_DB_Framework.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DisplayAttribute = SQ_DB_Framework.Attributes.DisplayAttribute;

namespace SQ_DB_Framework.Entities
{
   public class Customer : EntityBase
    {
        [Key,Display("客户")]
        public int CustomerId { get; set; }
        [Display("客户名称")]
        public string Name { get; set; }

        public List<SalesOrder> SalesOrders { get; set; }
    }
}
