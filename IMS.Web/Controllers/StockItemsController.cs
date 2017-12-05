using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Stock.Data;
using Stock.Domain;
using IMS.Web.Hubs;
using SharedKernel.Data;

namespace IMS.Web.Controllers
{
    public class StockItemsController : BaseApiControllerWithHub<StockHub>
    {
        private readonly GenericRepository<StockItem> _repository;
        private readonly StockItemsData _stockRepo;

        public StockItemsController(GenericRepository<StockItem> repository, StockItemsData stockRepo)
        {
            _repository = repository;
            _stockRepo = stockRepo;
        }

        public IEnumerable<StockItem> GetStockItems()
        {
            return _repository.AllInclude(s => s.ItemEntries);
        }

        [ResponseType(typeof(StockItem))]
        public IHttpActionResult GetStockItem(int id)
        {
            StockItem stockItem = _repository.FindByInclude(s => s.StockItemId == id, s => s.ItemEntries).First();
            if (stockItem == null)
            {
                return NotFound();
            }

            return Ok(stockItem);
        }


        public IHttpActionResult PutStockItem(int id, StockItem stockItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != stockItem.StockItemId)
            {
                return BadRequest();
            }

            try
            {
                _repository.Update(stockItem);
                Hub.Clients.Group("Restaurant").updateItem(stockItem);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StockItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        [ResponseType(typeof(StockItem))]
        public IHttpActionResult PostStockItem(StockItem stockItem)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            _repository.Insert(stockItem);
            
            Hub.Clients.Group("Restaurant").addItem(stockItem); 

            return CreatedAtRoute("DefaultApi", new { id = stockItem.StockItemId }, stockItem);
        }

        [ResponseType(typeof(StockItem))]
        public IHttpActionResult DeleteStockItem(int id)
        {
            StockItem stockItem = _repository.FindByKey(id);
            if (stockItem == null)
            {
                return NotFound();
            }

            _repository.Delete(stockItem.StockItemId);
            var stockItems = _repository.AllInclude(s => s.ItemEntries);
            Hub.Clients.Group("Restaurant").deleteStockItem(stockItems);
            return Ok(stockItem);
        }

        public IHttpActionResult AddItemEntry(int id, ItemEntry item)
        {
            if (id != item.StockItemId)
            {
                return BadRequest();
            }

            var stockItem = _stockRepo.GetStockItemByIdWithItemEntries(id);
            if (stockItem == null)
            {
                return NotFound();
            }

            try
            {

                stockItem.InsertNewItemEntry(item.Quantity, item.PricePerUnit, item.ExpirationDate, item.Temperature);
                stockItem = _stockRepo.UpdateItemsForExistingStock(stockItem);

                Hub.Clients.Group("Restaurant").addItemEntry(item);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StockItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }


        public IHttpActionResult UseStockItem(int id)
        {

            var stockItem = _stockRepo.GetStockItemByIdWithItemEntries(id);
            if (stockItem == null)
            {
                return NotFound();
            }

            try
            {
                stockItem.UseStockItem();
                stockItem = _stockRepo.UpdateItemsForExistingStock(stockItem);

                Hub.Clients.Group("Restaurant").updateItem(stockItem);
            }
            catch (Exception)
            {

                return NotFound();
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        private bool StockItemExists(int id)
        {
            return _repository.All().Count(e => e.StockItemId == id) > 0;
        }
    }
}