using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Stock.Domain.Tests
{
    [TestClass]
    public class StockDomainTests
    {
        private List<Category> _mockCategories;
        private List<ItemEntry> _mockItemEntries;

        public StockDomainTests()
        {
            _mockCategories = new List<Category>()
            {
                new Category()
                {
                    CategoryId = 1,
                    Name = "Dry Goood"
                },
                new Category()
                {
                    CategoryId = 2,
                    Name = "Cooler"
                },
                new Category()
                {
                    CategoryId = 3,
                    Name = "Frozen"
                },
            };
        }

        [TestMethod]
        public void CanCreateNewStockItem()
        {
            var result = new StockItem();
            Assert.IsInstanceOfType(result, typeof(StockItem));
        }

        [TestMethod]
        public void CanCreateNewStockItemWithCategory()
        {
            var result = new StockItem()
            {
                Category = _mockCategories.SingleOrDefault(c => c.CategoryId == 1)
            };
            Assert.AreEqual(1, result.Category.CategoryId);
        }

        [TestMethod]
        public void CanAddItemsToStockItem()
        {
            var stockItem = new StockItem();
            _mockItemEntries = new List<ItemEntry>()
            {
                ItemEntry.Create(stockItem.StockItemId,15, 3.50m, DateTime.Now.AddDays(14)),
                ItemEntry.Create(stockItem.StockItemId,20, 2.50m, DateTime.Now.AddDays(14))
            };
            stockItem.AddItemEntries(_mockItemEntries);
            Assert.AreEqual(2, stockItem.ItemEntries.Count());
        }

        [TestMethod]
        public void CanGetTotalOnHand()
        {
            var stockItem = new StockItem();
            _mockItemEntries = new List<ItemEntry>()
            {
                ItemEntry.Create(stockItem.StockItemId,15, 3.50m, DateTime.Now.AddDays(14)),
                ItemEntry.Create(stockItem.StockItemId,20, 2.50m, DateTime.Now.AddDays(14))
            };
            stockItem.AddItemEntries(_mockItemEntries);
            Assert.AreEqual(35, stockItem.TotalOnHand);

        }

        [TestMethod]
        public void CanGetTotalValuationOFStock()
        {
            var stockItem = new StockItem();
            _mockItemEntries = new List<ItemEntry>()
            {
                ItemEntry.Create(stockItem.StockItemId,15, 3.50m, DateTime.Now.AddDays(14)),
                ItemEntry.Create(stockItem.StockItemId,20, 2.50m, DateTime.Now.AddDays(14))
            };
            stockItem.AddItemEntries(_mockItemEntries);
            Assert.AreEqual(102.50m, stockItem.TotalValuation);
        }

        [TestMethod]
        public void CanUseItemFromStockItem()
        {
            var stockItem = new StockItem();
            _mockItemEntries = new List<ItemEntry>()
            {
                ItemEntry.Create(stockItem.StockItemId,15, 3.50m, DateTime.Now.AddDays(14)),
                ItemEntry.Create(stockItem.StockItemId,20, 2.50m, DateTime.Now.AddDays(14))
            };
            stockItem.AddItemEntries(_mockItemEntries);
            Assert.AreEqual(35, stockItem.TotalOnHand);

            stockItem.UseStockItem();
            Assert.AreEqual(34, stockItem.TotalOnHand);
        }
    }
}
