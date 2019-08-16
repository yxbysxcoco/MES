
using DisplayAttribute = SQ_DB_Framework.Attributes.DisplayAttribute;
namespace SQ_DB_Framework.Entities.PlanManagement
{
    public class PlanDetailMeterailMap
    {
        [Display("物料")]
        public int materialId { get; set; }
        public Material Material { get; set; }
        [Display("计划编号")]
        public string PlanCode { get; set; }
        public PlanMaintain PlanMaintain { get; set; }
        [Display("数量")]
        public int Count { get; set; }
        [Display("数量")]
        public double Weight { get; set; }
        [Display("单位")]
        public int MeterageUnitIdId { get; set; }
        public MeterageUnit MeterageUnit { get; set; }
        [Display("类型")]
        public int DetailTypeId { get; set; }
        public DetailType DetailType { get; set; }
        [Display("状态")]
        public int StatusId { get; set; }
        public Status Status { get; set; }
    }
    
   
}
