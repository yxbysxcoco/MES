using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SQ_DB_Framework.Entities
{
   public class Customer
    {
        [Key, Column]
        public int CustomerId { get; set; }
        [Display(Name = "客户名称"), Column]
        public string Name { get; set; }

        public List<Order> Orders { get; set; }
    }
}
