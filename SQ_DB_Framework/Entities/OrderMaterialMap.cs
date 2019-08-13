using System;
using System.Collections.Generic;
using System.Text;

namespace SQ_DB_Framework.Entities
{
    public class OrderMaterialMap
    {
        public int MaterialId { get; set; }
        public string OrderCode { get; set; }
        public DateTime DeliveryTime { get; set; }
        public double UnitPrice { get; set; }
        public double Count { get; set; }
        public double Discount { get; set; }
        public double TotalPrice { get => UnitPrice * Count; }
        public string Remarks { get; set; }


        public Material Material { get; set; }
    }
}
