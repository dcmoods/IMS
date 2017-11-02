using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using Stock.Domain;
using SharedKernel.Data;
using Stock.Data;
using System.Text;
using System.Data.Entity;

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
