using SQ_DB_Framework.Attributes;
using SQ_DB_Framework.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using DisplayAttribute = SQ_DB_Framework.Attributes.DisplayAttribute;
using IndexAttribute = SQ_DB_Framework.Attributes.IndexAttribute;

namespace SQ_DB_Framework.Entities
{
    #region Entities
    [DataContract]
    public class ToolEquipment : EntityBase
    {
            [Key]
            [Display( "编码", CharWidth = 9)]
            [MaxLength(45)]
            [DataMember]
            public string Code { get; set; }
            [Display("版本")]
            [DataMember]
            public Double Edition { get; set; }
            [Display("类型"), Index]
            public int TypeId { get; set; }
            [Display("名称")]
            [DataMember]
            public string Name { get; set; }
            [Display("规格")]
            [DataMember]
            public string Standard { get; set; }
            [Display("材料"),  Index]
            public int MaterialId { get; set; }
            [Display("单重")]
            [DataMember]
            public double Weight { get; set; }
            [Display("代号")]
            [DataMember]
            public string Mark { get; set; }
            [Display("备注")]
            [DataMember]
            public string Remark { get; set; }
            [Display("数量计量单位"),  Index]
            public int MeterageUnitId { get; set; }
            [Display( "货币单位"),  Index]
            public int MoneyUnitId { get; set; }
            [DataMember]
            [Display("单价")]
            public double Univalence { get; set; }
            [DataMember]
            [Display("最低库存")]
            public double LowestStock { get; set; }
            [DataMember]
            [Display("安全库存")]
            public double SaveStock { get; set; }
            [DataMember]
            [Display("最高库存")]
            public double HighestStock { get; set; }
            [Display("库房"),  Index]
            public int StorehouseId { get; set; }
            [DataMember]
            [Display("生产厂家")]
            public string Manufacturer { get; set; }
            [DataMember]
            [Display("生产日期"),Index]
            public DateTime DateAdded { get; set; }
            [DataMember]
            [Display("出厂编号")]
            public string ExitNumber { get; set; }
            [DataMember]
            [Display("检验单位")]
            public string InspectionCompany { get; set; }
            [DataMember]
            [Display("最大使用时长")]
            public double MaxUseTime { get; set; }
            [DataMember]
            [Display("修理周期")]
            public string RepairCycle { get; set; }
            [DataMember]
            [Display("修理次数")]
            public double RepairNumber { get; set; }
            [DataMember]
            [Display("供应商")]
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
            public virtual ToolEquipmentType ToolEquipmentType { get; set; }

       
    }
}
#endregion
