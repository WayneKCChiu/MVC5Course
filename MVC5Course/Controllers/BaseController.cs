using MVC5Course.Models;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
   //[Authorize]
   public abstract class BaseController : Controller
    {
      protected override void HandleUnknownAction(string actionName) {
         this.Redirect("/").ExecuteResult(this.ControllerContext);

         base.HandleUnknownAction(actionName);
      }

      protected ProductRepository repo = RepositoryHelper.GetProductRepository();

        // GET: Base
        public ActionResult Debug()
        {
            return Content("Debug");
        }
    }
}