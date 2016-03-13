using MVC5Course.Models;
using System.Web.Mvc;
using System.Web.Security;

namespace MVC5Course.Controllers
{
   [Authorize]
   public class MemberController: Controller
   {
      [AllowAnonymous]
      public ActionResult Login() {
         return View();
      }

      [AllowAnonymous]
      [HttpPost]
      public ActionResult Login(LoginViewModel login) {
         if (CheckLogin(login.Email, login.Password)) {
            FormsAuthentication.RedirectFromLoginPage(login.Email, false);

            return RedirectToAction("Index", "Home");
         }

         ModelState.AddModelError("Password", "你輸入得密碼錯誤");

         return View();
      }

      public ActionResult LoginOut(LoginViewModel login) {
         FormsAuthentication.SignOut();

         return RedirectToAction("Index", "Home");
      }

      private bool CheckLogin(string username, string password) {
         return (username == "abc@gmail.com" && password == "123");
      }
   }
}