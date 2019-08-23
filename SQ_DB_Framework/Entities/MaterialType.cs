using SQ_DB_Framework.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using DisplayAttribute = SQ_DB_Framework.Attributes.DisplayAttribute;

namespace SQ_DB_Framework.Entities
{
    public class MaterialType : EntityBase
    {
        [Key, Display("物料类型")]
        public int Id { get; set; }
        [Display("名称")]
        public string Name { get; set; }
        [Display("父类型")]
        public int? ParentMaterialTypeId { get; set; }
        
        [ForeignKey("ParentMaterialTypeId")]
        public MaterialType ParentMaterialType { get; set; }

        [Include]
        public List<MaterialType> ChildrenMaterialTypes { get; set; }
        [Include]
        public List<Material> Materials { get; set; }
    }
}