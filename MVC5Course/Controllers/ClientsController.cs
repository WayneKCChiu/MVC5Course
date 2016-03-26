using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using MVC5Course.Models;
using PagedList;

namespace MVC5Course.Controllers
{
   public class ClientsController: Controller
   {
      private FabricsEntities db = new FabricsEntities();

      // GET: Clients
      public ActionResult Index(int pageNo = 1) {
         var client = db.Client.Include(c => c.Occupation).OrderBy(p => p.ClientId);

         var data = client.ToPagedList(pageNo, 10);

         ViewBag.pageNo = 1;

         return View(data);
      }

      // GET: Clients/Details/5
      public ActionResult Details(int? id) {
         if (id == null) {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
         }
         Client client = db.Client.Find(id);
         if (client == null) {
            return HttpNotFound();
         }
         return View(client);
      }

      // GET: Clients/Create
      public ActionResult Create() {
         ViewBag.OccupationId = new SelectList(db.Occupation, "OccupationId", "OccupationName");
         return View();
      }

      // POST: Clients/Create
      // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
      // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
      [HttpPost]
      [ValidateAntiForgeryToken]
      public ActionResult Create([Bind(Include = "ClientId,FirstName,MiddleName,LastName,Gender,DateOfBirth,CreditRating,XCode,OccupationId,TelephoneNumber,Street1,Street2,City,ZipCode,Longitude,Latitude,Notes")] Client client) {
         if (ModelState.IsValid) {
            db.Client.Add(client);
            db.SaveChanges();
            return RedirectToAction("Index");
         }

         ViewBag.OccupationId = new SelectList(db.Occupation, "OccupationId", "OccupationName", client.OccupationId);
         return View(client);
      }

      // GET: Clients/Edit/5
      public ActionResult Edit(int? id, int pageNo = 1) {
         if (id == null) {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
         }
         Client client = db.Client.Find(id);
         if (client == null) {
            return HttpNotFound();
         }

         ViewBag.pageNo = pageNo;

         ViewBag.OccupationId = new SelectList(db.Occupation, "OccupationId", "OccupationName", client.OccupationId);
         return View(client);
      }

      // POST: Clients/Edit/5
      // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
      // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
      [HttpPost]
      [ValidateAntiForgeryToken]
      public ActionResult Edit([Bind(Include = "ClientId,FirstName,MiddleName,LastName,Gender,DateOfBirth,CreditRating,XCode,OccupationId,TelephoneNumber,Street1,Street2,City,ZipCode,Longitude,Latitude,Notes")] Client client, int pageNo = 1) {
         if (ModelState.IsValid) {
            db.Entry(client).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", new { pageNo = pageNo });
         }
         ViewBag.OccupationId = new SelectList(db.Occupation, "OccupationId", "OccupationName", client.OccupationId);
         return View(client);
      }

      // GET: Clients/Delete/5
      public ActionResult Delete(int? id) {
         if (id == null) {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
         }
         Client client = db.Client.Find(id);
         if (client == null) {
            return HttpNotFound();
         }
         return View(client);
      }

      // POST: Clients/Delete/5
      [HttpPost, ActionName("Delete")]
      [ValidateAntiForgeryToken]
      public ActionResult DeleteConfirmed(int id) {
         Client client = db.Client.Find(id);
         db.Client.Remove(client);
         db.SaveChanges();
         return RedirectToAction("Index");
      }

      protected override void Dispose(bool disposing) {
         if (disposing) {
            db.Dispose();
         }
         base.Dispose(disposing);
      }
   }
}
