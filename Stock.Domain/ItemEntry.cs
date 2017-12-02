using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Domain
{
    public class ItemEntry
    {
        

        public static ItemEntry Create(int stockItemId, double quantity, 
                                    decimal pricePerUnit, DateTime expirationDate, string temperature = "")
        {
            return new ItemEntry(stockItemId, quantity, pricePerUnit, expirationDate, temperature);
        }

        private ItemEntry(int stockItemId, double quantity, decimal pricePerUnit, DateTime expirationDate, string temperature = "")
        {
            StockItemId = stockItemId;
            Quantity = quantity;
            ReceivedDate = DateTime.Now;
            ExpirationDate = expirationDate;
            PricePerUnit = pricePerUnit;
            Temperature = temperature;
        }

        public int ItemEntryId { get; set; }
        public int StockItemId { get; set; }
        public DateTime ReceivedDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public double Quantity { get; set; }
        public decimal PricePerUnit { get; set; }
        public string Temperature { get; set; }
    }
}
