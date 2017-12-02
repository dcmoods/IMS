using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Stock.Domain
{
    public class StockItem
    {
        private ICollection<ItemEntry> _itemEntries;

        public StockItem()
        {
            _itemEntries = new List<ItemEntry>();
        }

        [Key]
        public int StockItemId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double MinimumLevel { get; set; }
        public double MaximumLevel { get; set; }
        public string LevelUnit { get; set; }
        public int ReceivedBy { get; set; }
        public int UsedBy { get; set; }
        public Category Category { get; set; }

        public ICollection<ItemEntry> ItemEntries
        {
            get { return _itemEntries; }
        }

        public void CreateItemEntry(double quantity, decimal pricePerUnit, DateTime expirationDate, string temperature = "")
        {
            _itemEntries.Add(ItemEntry.Create(this.StockItemId, quantity, pricePerUnit, expirationDate, temperature));
        }

        public double TotalOnHand
        {
            get
            {
                return _itemEntries.Sum(s => s.Quantity);
            }
        }

        public decimal StockItemTotalValuation
        {
            get
            {
                return _itemEntries.Sum(s => s.PricePerUnit * Convert.ToDecimal(s.Quantity));
            }
        }

    }
}
