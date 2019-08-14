
using System;
using System.ComponentModel.DataAnnotations;
using DisplayAttribute = SQ_DB_Framework.Attributes.DisplayAttribute;

namespace SQ_DB_Framework.Entities.PlanManagement
{
    class ProductionDemandScheme
    {
        [Key,Display("方案编号")]
        public string Code { get; set; }
        [Display("方案名称")]
        public string Name { get; set; }
        [Display("需求来源")]
        public int DemandSourceId { get; set; }
        [Display("创建人")]
        public int EmployeeId { get; set; }
        [Display("创建部门")]
        public int DepartmentId { get; set; }
        [Display("创建日期")]
        public DateTime CreateDate { get; set; }
        [Display("备注")]
        public string Remark { get; set; }

        public Employee Employee { get; set; }
        public Department Department { get; set; }
        public DemandSource DemandSource { get; set; }
    }
}
