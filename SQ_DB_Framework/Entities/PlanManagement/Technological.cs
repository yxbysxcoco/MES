using System;
using System.ComponentModel.DataAnnotations;
using DisplayAttribute = SQ_DB_Framework.Attributes.DisplayAttribute;
namespace SQ_DB_Framework.Entities.PlanManagement
{
    public class Technological
    {
        [Key]
        [Display("工艺代号")]
        public string Code { get; set; }
        [Display("工艺名称")]
        public string Name { get; set; }
        [Display("工艺版本")]
        public string Version { get; set; }
        [Display("工艺路线")]
        public string TechnologicalRoute { get; set; }
        [Display("工艺编制")]
        public string ProductionObject { get; set; }
        [Display("工艺编制日期")]
        public DateTime OrganizationTime { get; set; }
        [Display("工时定额编制")]
        public string TaskTimeOrganization { get; set; }
        [Display("工时定额编制日期")]
        public DateTime TaskTimeOrganizationTime { get; set; }

    }
}
