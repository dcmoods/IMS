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
using SharedKernel.Data;

namespace IMS.Web.Controllers
{
    public class CategoriesController : ApiController
    {

        private readonly GenericRepository<Category> _repository;

        public CategoriesController(GenericRepository<Category> repository)
        {
            _repository = repository;
        }

        public IEnumerable<Category> GetCategories()
        {
            return _repository.All();
        }

        [ResponseType(typeof(Category))]
        public IHttpActionResult GetCategory(int id)
        {
            Category category = _repository.FindByKey(id);
            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

    }
}