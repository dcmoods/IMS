using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Domain
{
    public class LineItem
    {
        public static LineItem Create(int invoiceId, int stockId, double quantity, decimal pricePerUnit)
        {
            return new LineItem(invoiceId, stockId, quantity, pricePerUnit);
        }
        
        //Icluded to help with ORM
        //entity framework requires a public constructor for navigation properites. 
        public LineItem()
        {
        }

        private LineItem(int invoiceId, int stockItemId, double? quantity, decimal? pricePerUnit)
        {
            InvoiceId = invoiceId;
            StockItemId = stockItemId;
            Quantity = quantity;
            PricePerUnit = pricePerUnit;
        }

        [Key]
        public int InvoiceItemId { get; private set; }
        public int InvoiceId { get; private set; }
        public int StockItemId { get; private set; }
        public double? Quantity { get; private set; }
        public decimal? PricePerUnit { get; private set; }

                
        public decimal LineItemTotal
        {
            get
            {
                if (PricePerUnit.HasValue && Quantity.HasValue)
                {
                    return (decimal)Quantity.Value * PricePerUnit.Value;
                }
                return 0;
            }
        }

        
    }
}
