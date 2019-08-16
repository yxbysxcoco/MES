using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DisplayAttribute = SQ_DB_Framework.Attributes.DisplayAttribute;

namespace SQ_DB_Framework.Entities.PlanManagement
{
    public class ProductionDemandAnalysis
    {
        [Key]
        [Display("结果编号")]
        public string Code { get; set; }
        [Display("方案编号")]
        public string SchemeCode { get; set; }
        [Display("结果名称")]
        public string Name { get; set; }
        public ProductionDemandScheme ProductionDemandScheme { get; set; }

    }
}
