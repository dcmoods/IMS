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
        private StockContext db = new StockContext();

        private readonly GenericRepository<StockItem> _repository;


        public StockItemsController()
        {
            
        }

        // GET: api/StockItems
        public IQueryable<StockItem> GetStockItems()
        {
            return db.StockItems;
        }

        // GET: api/StockItems/5
        [ResponseType(typeof(StockItem))]
        public IHttpActionResult GetStockItem(int id)
        {
            StockItem stockItem = db.StockItems.Find(id);
            if (stockItem == null)
            {
                return NotFound();
            }

            return Ok(stockItem);
        }

        // PUT: api/StockItems/5
        [ResponseType(typeof(void))]
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

            db.Entry(stockItem).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
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

        // POST: api/StockItems
        [ResponseType(typeof(StockItem))]
        public IHttpActionResult PostStockItem(StockItem stockItem)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            stockItem.ReceivedDate = DateTime.Today;
            stockItem.ExpirationDate = DateTime.Today.AddDays(14);
            

            db.StockItems.Add(stockItem);
            db.SaveChanges();
            
            Hub.Clients.Group("Restaurant").addItem(stockItem); ;

            return CreatedAtRoute("DefaultApi", new { id = stockItem.StockItemId }, stockItem);
        }

        // DELETE: api/StockItems/5
        [ResponseType(typeof(StockItem))]
        public IHttpActionResult DeleteStockItem(int id)
        {
            StockItem stockItem = db.StockItems.Find(id);
            if (stockItem == null)
            {
                return NotFound();
            }

            db.StockItems.Remove(stockItem);
            db.SaveChanges();

            return Ok(stockItem);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StockItemExists(int id)
        {
            return db.StockItems.Count(e => e.StockItemId == id) > 0;
        }
    }
}