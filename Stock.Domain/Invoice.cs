using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Stock.Domain
{
    public class Invoice
    {
        private ICollection<InvoiceItem> _invoiceItems;

        public Invoice()
        {
            OrderDate = DateTime.Now.Date;
            _invoiceItems = new List<InvoiceItem>();
        }

        [Key]
        public int InvoiceId { get; set; }
        public DateTime OrderDate { get; set; }

        public ICollection<InvoiceItem> InvoiceItems
        {
            get { return _invoiceItems; }
            set { _invoiceItems = value; }
        }

    }
}