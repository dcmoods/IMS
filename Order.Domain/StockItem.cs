using System;
using System.ComponentModel.DataAnnotations;

namespace Order.Domain
{
    public class StockItem
    {
        [Key]
        public int StockItemId { get; set; }
        public string Name { get; set; }
        public decimal PricePerUnit { get; set; }
        public double Quantity { get; set; }
    }
}
