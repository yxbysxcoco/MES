using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DisplayAttribute = SQ_DB_Framework.Attributes.DisplayAttribute;

namespace SQ_DB_Framework.Entities.PlanManagement
{
    public class DemandParameterMeterailMap : EntityBase
    {
        [Display("需求参数")]
        public int DemandParameterId { get; set; }
        public DemandParameter DemandParameter { get; set; }
        [Display("物料")]
        public int materialId { get; set; }
        public Material Material { get; set; }
        [Display("来源类型")]
        public string SourceType { get; set; }
        [Display("来源单据")]
        public string SourceBill { get; set; }

        
    }
}
