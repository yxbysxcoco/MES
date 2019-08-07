using SQ_DB_Framework.Attributes;
using SQ_DB_Framework.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using IndexAttribute = SQ_DB_Framework.Attributes.IndexAttribute;

namespace SQ_DB_Framework.Entities
{
    #region Entities
    [DataContract]
    public class ToolEquipment : EntityBase
    {
            [Key,Column()]
            [Display(Name = "编码") , DisplayWidthAttribute(0,32)]
            [MaxLength(45)]
            [DataMember]
            public string Code { get; set; }
            [Display(Name = "版本"), DisplayWidthAttribute(0,4), Column()]
            [DataMember]
            public Double Edition { get; set; }
            [Display(Name = "类型"),  DisplayWidthAttribute(0, 6),Column(), Index]
            public int TypeId { get; set; }
            [Display(Name = "名称"), DisplayWidthAttribute(4, 2), Column()]
            [DataMember]
            public string Name { get; set; }
            [Display(Name = "规格"), DisplayWidthAttribute(0, 12), Column()]
            [DataMember]
            public string Standard { get; set; }
            [Display(Name = "材料"), DisplayWidthAttribute(0, 6), Column(), Index]
            public int MaterialId { get; set; }
            [Display(Name = "单重"), DisplayWidthAttribute(0, 6), Column()]
            [DataMember]
            public double Weight { get; set; }
            [Display(Name = "代号"), DisplayWidthAttribute(0, 32), Column()]
            [DataMember]
            public string Mark { get; set; }
            [Display(Name = "备注"), DisplayWidthAttribute(0, 100), Column()]
            [DataMember]
            public string Remark { get; set; }
            [Display(Name = "数量计量单位"), DisplayWidthAttribute(0, 6), Column(), Index]
            public int MeterageUnitId { get; set; }
            [Display(Name = "货币单位"), DisplayWidthAttribute(0, 4), Column(), Index]
            public int MoneyUnitId { get; set; }
            [DataMember]
            [Display(Name = "单价"), DisplayWidthAttribute(0, 6), Column()]
            public double Univalence { get; set; }
            [DataMember]
            [Display(Name = "最低库存"), DisplayWidthAttribute(0, 6), Column()]
            public double LowestStock { get; set; }
            [DataMember]
            [Display(Name = "安全库存"), DisplayWidthAttribute(0, 6), Column()]
            public double SaveStock { get; set; }
            [DataMember]
            [Display(Name = "最高库存"), DisplayWidthAttribute(0, 6), Column()]
            public double HighestStock { get; set; }
            [Display(Name = "库房"), DisplayWidthAttribute(0, 4), Column(), Index]
            public int StorehouseId { get; set; }
            [DataMember]
            [Display(Name = "生产厂家"), DisplayWidthAttribute(16, 0), Column()]
            public string Manufacturer { get; set; }
            [DataMember]
            [Display(Name = "生产日期"), DisplayWidthAttribute(0, 32), Column(),Index]
            public DateTime DateAdded { get; set; }
            [DataMember]
            [Display(Name = "出厂编号"), DisplayWidthAttribute(0, 32), Column()]
            public string ExitNumber { get; set; }
            [DataMember]
            [Display(Name = "检验单位"), DisplayWidthAttribute(16, 0), Column()]
            public string InspectionCompany { get; set; }
            [DataMember]
            [Display(Name = "最大使用时长"), DisplayWidthAttribute(0, 10), Column()]
            public double MaxUseTime { get; set; }
            [DataMember]
            [Display(Name = "修理周期"), DisplayWidthAttribute(0, 16), Column()]
            public string RepairCycle { get; set; }
            [DataMember]
            [Display(Name = "修理次数"), DisplayWidthAttribute(0, 6), Column()]
            public double RepairNumber { get; set; }
            [DataMember]
            [Display(Name = "供应商"), DisplayWidthAttribute(16, 0), Column()]
            public string Supplier { get; set; }   


            [ForeignKey("MaterialId")]
            //[Display(Name = "材料")]
            public Material Material { get; set; }
            [ForeignKey("MeterageUnitId")]
            //[Display(Name = "测量")]
            public MeterageUnit MeterageUnit { get; set; }
            [ForeignKey("MoneyUnitId")]
            //[Display(Name = "货币单位")]
            public MoneyUnit MoneyUnit { get; set; }
            [ForeignKey("StorehouseId")]
            //[ Display(Name = "库房")]
            public Storehouse Storehouse { get; set; }
            [ForeignKey("TypeId")]
            //[Display(Name = "工装类型")]
            public ToolEquipmentType ToolEquipmentType { get; set; }

       
    }
}
#endregion
