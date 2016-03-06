using MVC5Course.Models;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System;
using System.Linq;

namespace MVC5Course.Controllers
{
   public class EFController: Controller
   {

      FabricsEntities db = new FabricsEntities();

      public ActionResult Index(bool? IsActive, string Keyword) {

         //var product = new Product() {
         //   ProductName = "BMV",
         //   Price = 2,
         //   Stock = 1,
         //   Active = true
         //};

         //db.Products.Add(product);

         //var pkey = product.ProductId;

         //var data = db.Products.Where(p => p.ProductId == pkey).ToList();

         var data = db.Products.OrderByDescending(p => p.ProductId).AsQueryable();

         if (IsActive.HasValue) {
            data = data.Where(a => a.Active.HasValue ? a.Active.Value == IsActive : false);
         }

         if (!String.IsNullOrEmpty(Keyword)) {
            data = data.Where(a => a.ProductName.Contains(Keyword));
         }

         //var data = db.Products.Where(a => a.Active.HasValue ? a.Active.Value == IsActive : false).OrderByDescending(p => p.ProductId).Take(10);

         //foreach (var item in data) {
         //   item.Price = item.Price + 1;
         //}

         SaveChanges();

         return View(data);
      }

      public ActionResult Detail(int Id) {

         var data = db.Products.FirstOrDefault(p => p.ProductId == Id);

         return View(data);
      }

      public ActionResult Delete(int id) {
         var item = db.Products.Find(id);

         db.OrderLines.RemoveRange(item.OrderLines);
         db.Products.Remove(item);
         db.SaveChanges();

         return RedirectToAction("Index");
      }

      public ActionResult QueryPlan(int num = 10) {

         var data = db.Products.Include(t => t.OrderLines).OrderBy(p => p.ProductId).AsQueryable();

         //var data = db.Products.Include(t => t.OrderLines).Where(o => o.OrderLines.Count() < num).OrderBy(p => p.ProductId).AsQueryable();

         //var data = db.Database.SqlQuery<Product>(@"
         //     SELECT *
         //     FROM dbo.Product p
         //     WHERE p.ProductId < @p0", num);

         return View(data);
      }

      private void SaveChanges() {
         try {
            db.SaveChanges();
         }
         catch (DbEntityValidationException ex) {
            foreach (DbEntityValidationResult item in ex.EntityValidationErrors) {
               string entityName = item.Entry.GetType().Name;

               foreach (DbValidationError error in item.ValidationErrors) {
                  throw new Exception(entityName + " 類型驗證失敗: " + error.ErrorMessage);
               }
            }
         }
      }
   }
}