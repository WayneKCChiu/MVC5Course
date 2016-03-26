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
using MVC5Course.Models;

namespace MVC5Course.Controllers
{
   public class ProducstApiController: ApiController
   {
      private FabricsEntities db = new FabricsEntities();

      public ProducstApiController() {
         //db.Configuration.LazyLoadingEnabled = false;
      }

      // GET: api/ProducstApi
      public IQueryable<ProductApiViewModel> GetProduct() {

         return from p in db.Product
                select new ProductApiViewModel() {
                   id = p.ProductId,
                   name = p.ProductName
                };
      }

      // GET: api/ProducstApi/5
      [ResponseType(typeof(Product))]
      public IHttpActionResult GetProduct(int id) {
         Product product = db.Product.Find(id);
         if (product == null) {
            return NotFound();
         }

         return Ok(product);
      }

      // PUT: api/ProducstApi/5
      [ResponseType(typeof(void))]
      public IHttpActionResult PutProduct(int id, Product product) {
         if (!ModelState.IsValid) {
            return BadRequest(ModelState);
         }

         if (id != product.ProductId) {
            return BadRequest();
         }

         db.Entry(product).State = EntityState.Modified;

         try {
            db.SaveChanges();
         }
         catch (DbUpdateConcurrencyException) {
            if (!ProductExists(id)) {
               return NotFound();
            }
            else {
               throw;
            }
         }

         return StatusCode(HttpStatusCode.NoContent);
      }

      // POST: api/ProducstApi
      [ResponseType(typeof(Product))]
      public IHttpActionResult PostProduct(Product product) {
         if (!ModelState.IsValid) {
            return BadRequest(ModelState);
         }

         db.Product.Add(product);
         db.SaveChanges();

         return CreatedAtRoute("DefaultApi", new { id = product.ProductId }, product);
      }

      // DELETE: api/ProducstApi/5
      [ResponseType(typeof(Product))]
      public IHttpActionResult DeleteProduct(int id) {
         Product product = db.Product.Find(id);
         if (product == null) {
            return NotFound();
         }

         db.Product.Remove(product);
         db.SaveChanges();

         return Ok(product);
      }

      protected override void Dispose(bool disposing) {
         if (disposing) {
            db.Dispose();
         }
         base.Dispose(disposing);
      }

      private bool ProductExists(int id) {
         return db.Product.Any(e => e.ProductId == id);
      }
   }
}