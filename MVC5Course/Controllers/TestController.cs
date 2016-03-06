
using MVC5Course.Models;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
   public class TestController: Controller
   {
      // GET: Test
      public ActionResult Index() {
         return View();
      }

      public ActionResult MemberProfile() {
         return View();
      }

      [HttpPost]
      public ActionResult MemberProfile(MemberViewModel data) {
         return View();
      }
   }
}