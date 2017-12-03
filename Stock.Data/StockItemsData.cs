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

        public async Task<List<StockItem>> GetStockItemsAsync()
        {
            return await _context.StockItems.AsNoTracking().ToListAsync();
        }

        public List<StockItem> GetStockItems()
        {
            return _context.StockItems.AsNoTracking().ToList();
        }

        public StockItem GetStockItemByIdWithItemEntires(int id)
        {
            return _context.StockItems.AsNoTracking()
                .Include(s => s.ItemEntries).SingleOrDefault(s => s.StockItemId == id);
        }

        public List<StockItem> GetStockItemsWithItemEntries()
        {
            return _context.StockItems.Include(s => s.ItemEntries).AsNoTracking().ToList();
        }

        public List<Category> GetCategories()
        {
            return _context.Categories.AsNoTracking().ToList();
        }

        public void UpdateItemsForExistingStock(StockItem stockItem)
        {
            _context.Configuration.AutoDetectChangesEnabled = false;
            foreach (var item in stockItem.ItemEntries)
            {
                _context.ItemEntries.Attach(item);
            }
            _context.ChangeTracker.DetectChanges();
            _context.FixState();
            _context.SaveChanges();
        }
    }
}
