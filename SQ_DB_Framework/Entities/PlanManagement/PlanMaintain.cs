using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DisplayAttribute = SQ_DB_Framework.Attributes.DisplayAttribute;

namespace SQ_DB_Framework.Entities.PlanManagement
{
    public class PlanMaintain : EntityBase
    {
        [Key, Display("计划编号")]
        public string Code { get; set; }
        [Display("计划名称")]
        public string Name { get; set; }
        [Display("计划类型")]
        public int PlanTypeId { get; set; }
        public PlanType PlanType { get; set; }
        [Display("计划开始")]
        public DateTime StartTime { get; set; }
        [Display("计划结束")]
        public DateTime EndTime { get; set; }
        [Display("所属部门")]
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        [Display("计划编制")]
        public string PlanOrganization { get; set; }
        [Display("编制部门")]
        public string OrganizationDepartment { get; set; }
        [Display("指令号")]
        public string InstructionNumber { get; set; }
        [Display("编制日期")]
        public DateTime OrganizationDate { get; set; }
        [Display("备注")]
        public string Remark { get; set; }
        [Display("状态")]
        public int StatusId { get; set; }
        public Status Status { get; set; }
        [Display("附件")]
        public int PlanAccessoriesId { get; set; }
        public PlanAccessories PlanAccessories { get; set; }
        
        public List<PlanDetailMeterailMap> PlanDetailMeterailMaps { get; set; }

    }
}
