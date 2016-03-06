using MVC5Course.Models;
using System.Web.Mvc;
using System.Data.Entity.Validation;
using System;
using System.Linq;

namespace MVC5Course.Controllers
{
   public class EFController: Controller
   {

      FabricsEntities db = new FabricsEntities();

      public ActionResult Index() {

         var product = new Product() {
            ProductName = "BMV",
            Price = 2,
            Stock = 1,
            Active = true
         };

         db.Products.Add(product);

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

            throw;
         }


         var pkey = product.ProductId;

         //var data = db.Products.Where(p => p.ProductId == pkey).ToList();

         var data = db.Products.OrderByDescending(p => p.ProductId);

         return View(data);
      }

      public ActionResult Detail(int Id) {

         var data = db.Products.FirstOrDefault(p => p.ProductId == Id);

         return View(data);
      }
   }
}