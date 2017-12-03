using System;
using SharedKernel.Data;

namespace Stock.Domain
{
    public class ItemEntry : IStateObject
    { 
        //Factory method used to create new ItemEntries
        public static ItemEntry Create(int stockItemId, double quantity, decimal pricePerUnit, DateTime expirationDate, string temperature = "")
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
            this.State = ObjectState.Added;
        }

        //Icluded to help with ORM
        //entity framework requires a public constructor for navigation properites. 
        private ItemEntry()
        {
        }

        public int ItemEntryId { get; set; }
        public int StockItemId { get; set; }
        public DateTime ReceivedDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public double Quantity { get; set; }
        public decimal PricePerUnit { get; set; }
        public string Temperature { get; set; }
        public ObjectState State { get; set; }
       

        public void UpdateQuantity(double newQuantity)
        {
            if (Quantity != newQuantity && Quantity > 1)
            {
                Quantity = newQuantity;
                this.State = ObjectState.Modified;
            }
            else if (Quantity <= 0)
            {
                this.State = ObjectState.Deleted;
            }
        }

        public void UseSingleItem()
        {
            if (this.Quantity > 1)
            {
                this.Quantity -= 1;
                this.State = ObjectState.Modified;
            }
            else
            {
                this.State = ObjectState.Deleted;
            }
        }
    }
}
