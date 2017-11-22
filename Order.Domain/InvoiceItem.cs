using System.ComponentModel.DataAnnotations;

namespace Order.Domain
{
    public class InvoiceItem
    {
        [Key]
        public int InvoiceItemId { get; set; }
        public int InvoiceId { get; set; }

        public Invoice Invoice { get; set; }
        public StockItem StockItem { get; set; }

    }
}
