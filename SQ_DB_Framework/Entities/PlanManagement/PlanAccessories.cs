using System.ComponentModel.DataAnnotations;
using DisplayAttribute = SQ_DB_Framework.Attributes.DisplayAttribute;

namespace SQ_DB_Framework.Entities.PlanManagement
{

    public class PlanAccessories : EntityBase
    { 
        [Key]
        [Display("附件")]
        public int PlanAccessoriesId { get; set; }
        [Display("附件名")]
        public string Name { get; set; }
    
    }
}
