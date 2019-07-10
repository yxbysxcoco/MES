using MES.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MES.Table
{
    public class ToolEquipment : DBTable
    {
        [PrimaryKey, Field("Code", "Varchar2")]
        public string Code { get; set; }
        [Field("Edition", "Double")]
        public double Edition { get; set; }
        [ForeignKey(typeof(ToolEquipmentType), "Name")]
        [Field("TypeId", "Int")]
        public string TypeName { get; set; }
        [Field("Name", "Varchar2")]
        public string Name { get; set; }
        [Field("Standard", "Varchar2")]
        public string Standard { get; set; }
        [ForeignKey(typeof(Material), "Name")]
        [Field("MaterialId", "Int")]
        public string MaterialName { get; set; }
        [Field("Weight", "Double")]
        public double Weight { get; set; }
        [Field("Mark", "Varchar2")]
        public string Mark { get; set; }
        [Field("Remark", "Varchar2")]
        public string Remark { get; set; }
        [ForeignKey(typeof(MeterageUnit), "Name")]
        [Field("MeterageUnitId", "Int")]
        public string MeterageUnitName{ get; set; }
        [ForeignKey(typeof(MoneyUnit), "Name")]
        [Field("MoneyUnitId", "Int")]
        public string MoneyUnitName { get; set; }
        [Field("Univalence", "Double")]
        public double Univalence { get; set; }
        [Field("LowestStock", "Double")]
        public double LowestStock { get; set; }
        [Field("SaveStock", "Double")]
        public double SaveStock { get; set; }
        [Field("HighestStock", "Double")]
        public double HighestStock { get; set; }
        [ForeignKey(typeof(Storehouse), "Name")]
        [Field("StorehouseId", "Int")]
        public string StorehouseName { get; set; }
        [Field("Manufacturer", "Varchar2")]
        public string Manufacturer { get; set; }
        [Field("DateAdded", "TIMESTAMP")]
        public DateTime date { get; set; }
        [Field("ExitNumber", "Varchar2")]
        public string ExitNumber { get; set; }
        [Field("InspectionCompany", "Varchar2")]
        public string InspectionCompany { get; set; }
        [Field("MaxUseTime", "Double")]
        public double MaxUseTime { get; set; }
        [Field("RepairCycle", "Varchar2")]
        public string RepairCycle { get; set; }
        [Field("RepairNumber", "Double")]
        public double RepairNumber { get; set; }
        [Field("Supplier", "Varchar2")]
        public string Supplier { get; set; }
        public Material Material { get; set; }
        public MeterageUnit MeterageUnit { get; set; }
        public MoneyUnit MoneyUnit { get; set; }
        public Storehouse Storehouse { get; set; }
        public ToolEquipmentType ToolEquipmentType { get; set; }
    }
    public class Material : DBTable
    {
        [PrimaryKey, Increment]
        [Field("MaterialId", "Int")]
        public int MaterialId { get; set; }
        [Field("Name", "Varchar2")]
        public string Name { get; set; }
    }
    public class MeterageUnit : DBTable
    {
        [PrimaryKey, Increment]
        [Field("MeterageUnitId", "Int")]
        public int MeterageUnitId { get; set; }
        [Field("Name", "Varchar2")]
        public string Name { get; set; }
    }
    public class MoneyUnit : DBTable
    {
        [PrimaryKey, Increment]
        [Field("MoneyUnitId", "Int")]
        public int MoneyUnitId { get; set; }
        [Field("Name", "Varchar2")]
        public string Name { get; set; }
    }

    public class Storehouse : DBTable
    {
        [PrimaryKey, Increment]
        [Field("StorehouseId", "Int")]
        public int StorehouseId { get; set; }
        [Field("Name", "Varchar2")]
        public string Name { get; set; }
    }
    public class ToolEquipmentType : DBTable
    {
        [PrimaryKey, Increment]
        [Field("TypeId", "Int")]
        public int TypeId { get; set; }
        [Field("Name", "Varchar2")]
        public string Name { get; set; }
    }
}