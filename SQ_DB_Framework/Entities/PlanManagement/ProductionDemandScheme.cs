
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DisplayAttribute = SQ_DB_Framework.Attributes.DisplayAttribute;

namespace SQ_DB_Framework.Entities.PlanManagement
{
    
    public class ProductionDemandScheme : EntityBase
    {
        
        [Key,Display("方案编号" , CharWidth=32)]
        public string Code { get; set; }
        [Display("方案名称", CharWidth = 0,ChineseWidth =6)]
        public string Name { get; set; }
     
        [Display("需求参数")]
        public int DemandParameterId { get; set; }
        [ForeignKey("DemandParameterId")]
        public DemandParameter DemandParameter { get; set; }
        [Display("计算参数")]
        public int CalculationParameterId { get; set; }
        [ForeignKey("CalculationParameterId")]
        public CalculationParameter CalculationParameter { get; set; }
        [Display("创建人", CharWidth = 0, ChineseWidth = 6)]
        public int? EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }
        [Display("创建日期", CharWidth = 32)]
        public DateTime CreateDate { get; set; }
        [Display("备注", CharWidth = 0, ChineseWidth = 34)]
        public string Remark { get; set; }
        [Display("运行参数")]
        public int RunParameterId { get; set; }
        [ForeignKey("RunParameterId")]
        public RunParameter RunParameter { get; set; }
        

    }
    
}
