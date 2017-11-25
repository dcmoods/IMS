using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Domain
{
    public class Order
    {
        private ICollection<LineItem> _lineItems;

        public Order()
        {
            OrderDate = DateTime.Now.Date;
            _lineItems = new List<LineItem>();
        }

        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal SubTotal { get; set; }

        public ICollection<LineItem> LineItems
        {
            get { return _lineItems; }
        }

        public void CreateLineItems(int stockItemId, double quantity, decimal pricePerUnit)
        {
            _lineItems.Add(LineItem.Create(this.OrderId, stockItemId, quantity, pricePerUnit));
        }

        
    }
}
