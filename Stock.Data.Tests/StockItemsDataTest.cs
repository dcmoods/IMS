using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using System.Diagnostics;
using System.Data.Entity;
using System.Linq;
using Stock.Domain;
using System.Collections.Generic;

namespace Stock.Data.Tests
{
    [TestClass]
    public class StockItemsDataTest
    {
        private StringBuilder _logBuilder = new StringBuilder();
        private string _log;
        private StockContext _context;
        private StockItemsData _stockData;
        public StockItemsDataTest()
        {
            Database.SetInitializer(new NullDatabaseInitializer<StockContext>());
            _context = new StockContext();
            _stockData = new StockItemsData(_context);
            SetupLogging();
        }

        [TestMethod]
        public void CanFindStockItemByIdAndIncludeItemEntries()
        {
            var results = _stockData.GetStockItemByIdWithItemEntries(51);
            WriteLog();
            Assert.IsTrue(_log.Contains("ItemEntries"));
            Assert.AreNotEqual(0, results.ItemEntries.Count());
        }

        [TestMethod]
        public void CanUpdateItemEntriesForStockItem()
        {
            //get stock item 
            var stockItem = _stockData.GetStockItemByIdWithItemEntries(51);

            //add new item
            stockItem.InsertNewItemEntry(1, 2.50m, DateTime.Now.AddDays(1));

            //update stock item
            _stockData.UpdateItemsForExistingStock(stockItem);
            Assert.IsTrue(_log.Contains("ItemEntries"));
        }

        private void WriteLog()
        {
            Debug.WriteLine(_log);
        }

        private void SetupLogging()
        {
            _context.Database.Log = BuildLogString;
        }

        private void BuildLogString(string message)
        {
            _logBuilder.Append(message);
            _log = _logBuilder.ToString();
        }
    }
}
