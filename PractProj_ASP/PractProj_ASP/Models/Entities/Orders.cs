using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PractProj_ASP.Models.Entities
{
    [Table("Orders")]
    public class Orders
    {
        [Key]
        public int RowId { get; set; }
        public int OrderId { get; set; }
        public DateTime? OrderDate { get; set; }
        public string OrderPriority { get; set; }
        public int OrderQuantity { get; set; }
        public double Sales { get; set; }
        public double Discount { get; set; }
        public string ShipMode { get; set; }
        public double Profit { get; set; }
        public double UnitPrice { get; set; }
        public double ShippingCost { get; set; }
        public string CustomerName { get; set; }
        public string Privince { get; set; }
        public string Region { get; set; }
        public string CustomerSegment { get; set; }
        public string ProductCategory { get; set; }
        public string ProductSubCategory { get; set; }
        public string ProductName { get; set; }
        public string ProductContainer { get; set; }
        public string? ProductBaseMargin { get; set; }
        public DateTime ShiprDate { get; set; }
    }
}
