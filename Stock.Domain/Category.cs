using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Stock.Domain
{
    public class Category
    {
        private ICollection<StockItem> _stockItems;

        public Category()
        {
            _stockItems = new List<StockItem>();
        }

        [Key]
        public int CategoryId { get; set; }
        public string Name { get; set; }


        public ICollection<StockItem> StockItems
        {
            get { return _stockItems; }
            set { _stockItems = value; }
        }


    }
}