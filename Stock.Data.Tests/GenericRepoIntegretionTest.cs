using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using Stock.Domain;
using SharedKernel.Data;
using Stock.Data;
using System.Text;
using System.Data.Entity;
using System.Linq;

namespace Stock.Data.Tests
{
    [TestClass]
    public class GenericRepoIntegretionTest
    {
        private StringBuilder _logBuilder = new StringBuilder();
        private string _log;
        private StockContext _context;
        private GenericRepository<StockItem> _stockItemRepository;


        public GenericRepoIntegretionTest()
        {
            Database.SetInitializer(new NullDatabaseInitializer<StockContext>());
            _context = new StockContext();
            _stockItemRepository = new GenericRepository<StockItem>(_context);
            SetupLogging();
        }

        [TestMethod]
        public void CanFindByStockItemByKeyWithDynamicLambda()
        {
            var results = _stockItemRepository.FindByKey(1);
            WriteLog();
            Assert.IsTrue(_log.Contains("FROM [Stock].[StockItem"));
        }

        [TestMethod]
        public void NoTrackingQueriesDoNotCacheObjects()
        {
            _stockItemRepository.All();
            Assert.AreEqual(0, _context.ChangeTracker.Entries().Count());
        }

        [TestMethod]
        public void CanQueryWithSinglePredicate()
        {
            _stockItemRepository.FindBy(s => s.Name.StartsWith("C"));
            WriteLog();
            Assert.IsTrue(_log.Contains("'C%'"));
        }

        [TestMethod]
        public void CanQueryWithDualPredicate()
        {
            _stockItemRepository
               .FindBy(s => s.Name.StartsWith("C") && s.CategoryId == 1);
            WriteLog();
            Assert.IsTrue(_log.Contains("'C%'") && _log.Contains("1"));
        }

        [TestMethod]
        public void CanQueryWithComplexRelatedPredicate()
        {
            _stockItemRepository
               .FindBy(s => s.Name.StartsWith("C") && s.CategoryId == 1
                                                       && s.ItemEntries.Any());
            WriteLog();
            Assert.IsTrue(_log.Contains("'C%'") && _log.Contains("1") && _log.Contains("ItemEntries"));
        }

        [TestMethod]
        public void CanIncludeNavigationProperties()
        {
            var results = _stockItemRepository.AllInclude(c => c.ItemEntries);
            WriteLog();
            Assert.IsTrue(_log.Contains("ItemEntries"));
            Assert.IsTrue(results.Any(c => c.ItemEntries.Any()));
        }

        [TestMethod]
        public void CanCombineFilterAndInclude()
        {
            var results = _stockItemRepository
             .FindByInclude(s => s.StockItemId == 51, s => s.ItemEntries);
            WriteLog();
            Assert.AreNotEqual(0, results.Count(c => c.ItemEntries.Any()));
            Assert.IsTrue(_log.Contains("51"));
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
