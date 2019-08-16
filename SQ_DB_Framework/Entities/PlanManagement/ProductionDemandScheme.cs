
using System;
using System.ComponentModel.DataAnnotations;
using DisplayAttribute = SQ_DB_Framework.Attributes.DisplayAttribute;

namespace SQ_DB_Framework.Entities.PlanManagement
{
    public class ProductionDemandScheme
    {
        [Key,Display("方案编号")]
        public string Code { get; set; }
        [Display("方案名称")]
        public string Name { get; set; }
     
        [Display("需求参数")]
        public int DemandParameterId { get; set; }
        public DemandParameter DemandParameter { get; set; }
        [Display("创建人")]
        public int DepartmentId { get; set; }
        [Display("创建日期")]
        public DateTime CreateDate { get; set; }
        [Display("备注")]
        public string Remark { get; set; }
        [Display("运行参数")]
        public int RunParameterId { get; set; }
        public RunParameter RunParameter { get; set; }
        public Employee Employee { get; set; }

    }
    
}
