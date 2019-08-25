using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DisplayAttribute = SQ_DB_Framework.Attributes.DisplayAttribute;

namespace SQ_DB_Framework.Entities.PlanManagement
{
    public class CalculationRange : EntityBase
    {
        [Key]
        [Display("计算范围")]
        public int CalculationRangeId { get; set; }
        [Display("类型名称")]
        public string TypeName { get; set; }
        
    }
    
}
