using SQ_DB_Framework.Attributes;
using SQ_DB_Framework.Entities;
using SQ_DB_Framework.Entities.PlanManagement;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DisplayAttribute = SQ_DB_Framework.Attributes.DisplayAttribute;

public class Material : EntityBase
{
    [Key, Increment, Display("物料编码")]
    public int MaterialId { get; set; }
    [Display("名称")]
    public string Name { get; set; }
    [Display("规格")]
    public string Specifications { get; set; }
    [Display("物料类型")]
    public int MaterialTypeId { get; set; }
    [ForeignKey("MaterialTypeId")]
    public MaterialType MaterialType{get;set;}
    [Display("计量单位")]
    public int MeterageUnitId { get; set; }

    [ForeignKey("MeterageUnitId")]
    public MeterageUnit MeterageUnit { get; set; }
    public List<DemandParameterMeterailMap> DemandParameterMeterailMaps { get; set; }

    public List<PlanDetailMeterailMap> PlanDetailMeterailMaps { get; set; }
}
