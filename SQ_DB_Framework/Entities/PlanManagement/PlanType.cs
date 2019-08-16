using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DisplayAttribute = SQ_DB_Framework.Attributes.DisplayAttribute;

namespace SQ_DB_Framework.Entities.PlanManagement
{
    public class PlanType
    {
        [Key]
        [Display("计划类型")]
        public int Id { get; set; }
        [Display("计划类型名称")]
        public string Name { get; set; }
    }

  
}
