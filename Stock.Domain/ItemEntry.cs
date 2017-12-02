using System;

namespace Stock.Domain
{
    public class ItemEntry
    {

        //Factory method used to create new ItemEntries
        public static ItemEntry Create(double quantity,
                                    decimal pricePerUnit, DateTime expirationDate, string temperature = "")
        {
            return new ItemEntry(quantity, pricePerUnit, expirationDate, temperature);
        }

        private ItemEntry(double quantity, decimal pricePerUnit, DateTime expirationDate, string temperature = "")
        {
            Quantity = quantity;
            ReceivedDate = DateTime.Now;
            ExpirationDate = expirationDate;
            PricePerUnit = pricePerUnit;
            Temperature = temperature;
        }

        //Icluded to help with ORM
        //entity framework requires a public constructor for navigation properites. 
        public ItemEntry()
        {
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
