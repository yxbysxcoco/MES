using System;
using System.ComponentModel.DataAnnotations;
using DisplayAttribute = SQ_DB_Framework.Attributes.DisplayAttribute;

namespace SQ_DB_Framework.Entities.PlanManagement
{
    public class WorkOrder
    {
        [Key]
        [Display("工单编号")]
        public string Code { get; set; }
        [Display("工单名称")]
        public string Name { get; set; }
        [Display("生产对象")]
        public string ProductionObject { get; set; }
        [Display("主制车间")]
        public string MainWorkshop { get; set; }
        [Display("工单编制")]
        public string WorkOrderOrganization { get; set; }
        [Display("工艺")]
        public string TechnologicalCode { get; set; }
        public Technological Technological { get; set; }
        [Display("计划编号")]
        public string PlanMaintainCode { get; set; }
        public PlanMaintain PlanMaintain { get; set; }
        [Display("备注")]
        public string Remark { get; set; }
        [Display("状态")]
        public int StatusId { get; set; }
        public Status Status { get; set; }
        [Display("紧急程度")]
        public int Emergency { get; set; }
        [Display("工单编制日期")]
        public DateTime WorkOrderOrganizationTime { get; set; }
        [Display("打印状态")]
        public string PrintStatus { get; set; }

    }
}
