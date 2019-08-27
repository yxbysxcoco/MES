using System.ComponentModel.DataAnnotations;
using DisplayAttribute = SQ_DB_Framework.Attributes.DisplayAttribute;

namespace SQ_DB_Framework.Entities.PlanManagement
{
    public class CalculationParameter : EntityBase
    {
        [Key]
        [Display("计算参数")]
        public int Id { get; set; }
        [Display("计划周期")]
        public int Cycle { get; set; }
        [Display("计划周期数")]
        public int CycleCount { get; set; }
        [Display("考虑损耗率")]
        public bool Attrition { get; set; }
        [Display("考虑成品率")]
        public bool Yield { get; set; }
        [Display("考虑安全库存")]
        public bool SafetyStock { get; set; }
        [Display("考虑已占用量")]
        public bool Occupancy { get; set; }
        [Display("考虑已计划量")]
        public bool InvName { get; set; }
        [Display("考虑最小批量")]
        public bool MinBatch { get; set; }
        [Display("考虑已发货量")]
        public bool Delivergoodsed { get; set; }
        [Display("考虑现有库存")]
        public bool NowStock { get; set; }
        public CalculationParameter() { }
   
    }
}
