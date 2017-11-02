using System;
using System.ComponentModel.DataAnnotations;

namespace Stock.Domain
{
    public class StockItem
    {
        [Key]
        public int StockItemId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double MinimumLevel { get; set; }
        public double MaximumLevel { get; set; }
        public string LevelUnit { get; set; }
        public decimal PricePerUnit { get; set; }
        public DateTime ReceivedDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public double Quantity { get; set; }

        public int ReceivedBy { get; set; }
        public int UsedBy { get; set; }

        [Required]
        public Category Category { get; set; }
    }
}
