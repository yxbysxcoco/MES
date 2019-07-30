using SQ_DB_Framework.Attributes;
using SQ_DB_Framework.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace SQ_DB_Framework.Entities
{
    #region Entities
    [DataContract]
    public class ToolEquipment : EntityBase
    {
            [Key,Column()]
            [Display(Name = "编码")]
            [MaxLength(45)]
            [DataMember]
            public string Code { get; set; }
            [Display(Name = "版本"), Column()]
            [DataMember]
            public Double Edition { get; set; }
            [Display(Name = "类型Id"), Column(), Index]
            public int TypeId { get; set; }
            /*[NotMapped]
            [DataMember]
            public string TypeName { get => ToolEquipmentType.Name; }*/
            [Display(Name = "名称"), Column()]
            [DataMember]
            public string Name { get; set; }
            [Display(Name = "规格"), Column()]
            [DataMember]
            public string Standard { get; set; }
            [Display(Name = "材料Id"), Column(), Index]
            public int MaterialId { get; set; }
            /*[NotMapped]
            [DataMember]
            public string MaterialName { get => Material.Name; }*/
            [Display(Name = "单重"), Column()]
            [DataMember]
            public double Weight { get; set; }
            [Display(Name = "代号"), Column()]
            [DataMember]
            public string Mark { get; set; }
            [Display(Name = "备注"), Column()]
            [DataMember]
            public string Remark { get; set; }
            [Display(Name = "数量计量单位"), Column(), Index]
            public int MeterageUnitId { get; set; }
            [NotMapped]
            [DataMember]
            public string MeterageUnitName { get => MeterageUnit.Name; }
            [Display(Name = "货币单位Id"), Column(), Index]
            public int MoneyUnitId { get; set; }
            /*[NotMapped]
            [DataMember]
            public string MoneyUnitName { get => MoneyUnit.Name; }*/
            [DataMember]
            [Display(Name = "单价"), Column()]
            public double Univalence { get; set; }
            [DataMember]
            [Display(Name = "最低库存"), Column()]
            public double LowestStock { get; set; }
            [DataMember]
            [Display(Name = "安全库存"), Column()]
            public double SaveStock { get; set; }
            [DataMember]
            [Display(Name = "最高库存"), Column()]
            public double HighestStock { get; set; }
            [Display(Name = "库房Id"), Column(), Index]
            public int StorehouseId { get; set; }
          /*  [NotMapped]
            [DataMember]
            public string StorehouseName { get => Storehouse.Name; }*/
            [DataMember]
            [Display(Name = "生产厂家"), Column()]
            public string Manufacturer { get; set; }
            [DataMember]
            [Display(Name = "生产日期"), Column(),Index]
            public DateTime DateAdded { get; set; }
            [DataMember]
            [Display(Name = "出厂编号"), Column()]
            public string ExitNumber { get; set; }
            [DataMember]
            [Display(Name = "检验单位"), Column()]
            public string InspectionCompany { get; set; }
            [DataMember]
            [Display(Name = "最大使用时长"), Column()]
            public double MaxUseTime { get; set; }
            [DataMember]
            [Display(Name = "修理周期"), Column()]
            public string RepairCycle { get; set; }
            [DataMember]
            [Display(Name = "修理次数"), Column()]
            public double RepairNumber { get; set; }
            [DataMember]
            [Display(Name = "供应商"), Column()]
            public string Supplier { get; set; }   


            [ForeignKey("MaterialId"), Display(Name = "材料")]
            public Material Material { get; set; }
            [ForeignKey("MeterageUnitId"), Display(Name = "测量")]
            public MeterageUnit MeterageUnit { get; set; }
            [ForeignKey("MoneyUnitId"), Display(Name = "货币单位")]
            public MoneyUnit MoneyUnit { get; set; }
            [ForeignKey("StorehouseId"), Display(Name = "库房")]
            public Storehouse Storehouse { get; set; }
            [ForeignKey("TypeId"), Display(Name = "工装类型")]
            public ToolEquipmentType ToolEquipmentType { get; set; }

        }
}
#endregion
