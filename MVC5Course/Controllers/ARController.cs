using System.Text;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
   public class ARController: BaseController
   {
      // GET: AR
      public ActionResult Index() {
         return View();
      }

      public ActionResult Index2() {
         return PartialView("Index");
      }

      public ActionResult ContentTest() {
         return Content("<script>alert('Loading...')</script>", "application/javascript", Encoding.UTF8);
      }

      public ActionResult FileTest() {
         return File(Server.MapPath("~/Content/imgres.png"), "image/png");
      }
   }
}