using DisplayAttribute = SQ_DB_Framework.Attributes.DisplayAttribute;

namespace SQ_DB_Framework.Entities.PlanManagement
{
    public class DemandParameterSalesOrderMap
    {
        [Display("需求参数")]
        public int DemandParameterId { get; set; }
        public DemandParameter DemandParameter { get; set; }
        [Display("物料")]
        public string OrderCode { get; set; }
        public SalesOrder SalesOrder { get; set; }
        [Display("类型")]
        public string Type { get; set; }

    }
}
