using System.ComponentModel.DataAnnotations;
using DisplayAttribute = SQ_DB_Framework.Attributes.DisplayAttribute;

namespace SQ_DB_Framework.Entities.PlanManagement
{
    public class DetailType
    {
        [Key]
        [Display("筛选类型")]
        public int id { get; set; }
        [Display("描述")]
        public string Describe { get; set; }



    }
}
