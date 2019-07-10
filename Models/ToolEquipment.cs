using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace MES.Models
{
    public class ToolEquipment
    {
        [Key]
        public string Code { get; set; }
        public double Edition { get; set; }
        public int TypeId { get; set; }
        [MaxLength(32)]
        public string Name { get; set; }
        [MaxLength(32)]
        public string Standard { get; set; }
        public int MaterialId { get; set; }
        public double Weight { get; set; }
        [MaxLength(64)]
        public string Mark { get; set; }
        [MaxLength(100)]
        public string Remark { get; set; }
        public int MeterageUnitId { get; set; }
        public int MoneyUnitId { get; set; }
        public double Univalence { get; set; }
        public double LowestStock { get; set; }
        public double SaveStock { get; set; }
        public double HighestStock { get; set; }
        public int StorehouseId { get; set; }
        [MaxLength(10)]
        public string Manufacturer { get; set; }
        public DateTime date { get; set; }
        [MaxLength(32)]
        public string ExitNumber { get; set; }
        [MaxLength(64)]
        public string InspectionCompany{ get; set; }
        public double MaxUseTime { get; set; }
        [MaxLength(32)]
        public string RepairCycle { get; set; }
        public double RepairNumber { get; set; }
        [MaxLength(32)]
        public string Supplier { get; set; }
        public Material Material { get; set; }
        public MeterageUnit MeterageUnit { get; set; }
        public MoneyUnit MoneyUnit { get; set; }
        public Storehouse Storehouse { get; set; }
        public ToolEquipmentType ToolEquipmentType { get; set; }
    }
    public class Material
    {
        public int MaterialId { get; set; }
        [MaxLength(32)]
        public string Name { get; set; }
    }
    public class MeterageUnit
    {
        public int MeterageUnitId { get; set; }
        [MaxLength(10)]
        public string Name { get; set; }
    }
    public class MoneyUnit
    {
        public int MoneyUnitId { get; set; }
        [MaxLength(10)]
        public string Name { get; set; }
    }

    public class Storehouse
    {
        public int StorehouseId { get; set; }
        [MaxLength(32)]
        public string Name { get; set; }
    }
    public class ToolEquipmentType
    {
        [Key]
        public int TypeId { get; set; }
        [MaxLength(32)]
        public string Name { get; set; }
    }
}