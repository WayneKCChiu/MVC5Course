﻿using System.Data.Entity;
using System.Net;
using System.Web.Mvc;
using MVC5Course.Models;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace MVC5Course.Controllers
{
   public class ProductsController: BaseController
   {
      // GET: Product
      public ActionResult Index(int? ProductId, string type, bool? isActive, string keyword) {
         //var data = repo.All().Take(5);

         var data = repo.All(true);


         if (isActive.HasValue) {
            data = data.Where(p => p.Active.HasValue && p.Active == isActive.Value);
         }

         if(!string.IsNullOrEmpty(keyword)) {
            data = data.Where(p => p.ProductName.Contains(keyword));

         }

         var item = new List<SelectListItem>();
         item.Add(new SelectListItem() { Value = "true", Text = "有效" });
         item.Add(new SelectListItem() { Value = "false", Text = "無效" });
         ViewData["isActive"] = new SelectList(item, "Value", "Text");

         ViewBag.type = type;

         if (ProductId.HasValue) {
            ViewBag.SelectProductId = ProductId;
         }
         return View(data.Take(5));
      }

      [HttpPost]
      public ActionResult Index(IList<Product批次更新ViewModel> products) {
         if (ModelState.IsValid) {
            foreach (var item in products) {
               var product = repo.Find(item.ProductId);
               product.Stock = item.Stock;
               product.Price = item.Price;
            }

            repo.UnitOfWork.Commit();
            return RedirectToAction("Index");
         }

         return View(repo.All().Take(5));

      }

      // GET: Product/Details/5
      public ActionResult Details(int? id) {
         if (id == null) {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
         }
         //Product product = db.Product.Find(id);

         Product product = repo.Find(id.Value);
         if (product == null && product.IsDeleted) {
            return HttpNotFound();
         }
         return View(product);
      }

      // GET: Product/Create
      public ActionResult Create() {
         return View();
      }

      // POST: Product/Create
      // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
      // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
      [HttpPost]
      [ValidateAntiForgeryToken]
      public ActionResult Create([Bind(Include = "ProductId,ProductName,Price,Active,Stock")] Product product) {
         if (ModelState.IsValid) {
            repo.Add(product);
            repo.UnitOfWork.Commit();
            return RedirectToAction("Index");
         }

         return View(product);
      }

      // GET: Product/Edit/5
      public ActionResult Edit(int? id) {
         if (id == null) {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
         }

         Product product = repo.Find(id.Value);
         if (product == null) {
            return HttpNotFound();
         }
         return View(product);
      }

      // POST: Product/Edit/5
      // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
      // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
      [HttpPost]
      [ValidateAntiForgeryToken]
      public ActionResult Edit(int id, FormCollection a) {
         //if (ModelState.IsValid) {
         //   var db = (FabricsEntities)repo.UnitOfWork.Context; // 轉型
         //   db.Entry(product).State = EntityState.Modified;
         //   db.SaveChanges();
         //   return RedirectToAction("Index");
         //}

         var product = repo.Find(id);

         if (TryUpdateModel<Product>(product, new string[] { "ProductId,ProductName,Price,Active,Stock" })) {
            repo.UnitOfWork.Commit();

            TempData["ProductsEditDoneMsg"] = "商品編輯成功";

            return RedirectToAction("Index");
         }

         return View(product);
      }

      // GET: Product/Delete/5
      public ActionResult Delete(int? id) {
         if (id == null) {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
         }
         //Product product = db.Product.Find(id);

         Product product = repo.Find(id.Value);

         if (product == null) {
            return HttpNotFound();
         }
         return View(product);
      }

      // POST: Product/Delete/5
      [HttpPost, ActionName("Delete")]
      [ValidateAntiForgeryToken]
      public ActionResult DeleteConfirmed(int id) {
         //Product product = db.Product.Find(id);
         //db.Product.Remove(product);
         //db.SaveChanges();
         Product product = repo.Find(id);
         product.IsDeleted = true;
         repo.UnitOfWork.Commit();

         return RedirectToAction("Index");
      }

      protected override void Dispose(bool disposing) {
         if (disposing) {
            var db = (FabricsEntities)repo.UnitOfWork.Context;
            db.Dispose();
         }
         base.Dispose(disposing);
      }
   }
}
