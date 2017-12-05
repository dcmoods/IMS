using SharedKernel.Data;
using Stock.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Data
{
    public class StockItemsData
    {
        private readonly StockContext _context;

        public StockItemsData(StockContext context)
        {
            _context = context;
        }

        public StockItem GetStockItemByIdWithItemEntries(int id)
        {
            return _context.StockItems.AsNoTracking()
                .Include(s => s.ItemEntries).SingleOrDefault(s => s.StockItemId == id);
        }

        public StockItem UpdateItemsForExistingStock(StockItem stockItem)
        {
            _context.Configuration.AutoDetectChangesEnabled = false;
            foreach (var item in stockItem.ItemEntries)
            {
                _context.ItemEntries.Attach(item);
            }
            _context.ChangeTracker.DetectChanges();
            _context.FixState();
            _context.SaveChanges();

            return this.GetStockItemByIdWithItemEntries(stockItem.StockItemId);
        }
    }
}
