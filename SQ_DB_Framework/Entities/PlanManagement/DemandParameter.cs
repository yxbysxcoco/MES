using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DisplayAttribute = SQ_DB_Framework.Attributes.DisplayAttribute;

namespace SQ_DB_Framework.Entities.PlanManagement
{
    public class DemandParameter : EntityBase
    {
        [Key]
        [Display("需求参数")]
        public int DemandParameterId { get; set; }
        [Display("需求来源")]
        public int DemandSourceId { get; set; }
        public DemandSource DemandSource { get; set; }
        [Display("计算范围")]
        public int CalculationRangeId { get; set; }
        public CalculationRange CalculationRange { get; set; }

        public List<DemandParameterMeterailMap> DemandParameterMeterailMaps { get; set; }

        public List<DemandParameterSalesOrderMap> DemandParameterSalesOrderMaps { get; set; }

        public DemandParameter()
        {

        }
    }
    
    
}
