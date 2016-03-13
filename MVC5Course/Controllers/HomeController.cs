using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
   public class HomeController: BaseController
   {
      [共用的ViewBag資料共享於部分HomeController動作方法Attribute]
      public ActionResult Index() {
         return View();
      }

      [紀錄Action執行時間]
      [共用的ViewBag資料共享於部分HomeController動作方法Attribute]
      public ActionResult About() {
         return View(); //TODO: About()
      }

      public ActionResult Contact() {
         ViewBag.Message = "Your contact page.";

         return View();
      }

      public ActionResult Test() {
         ViewBag.Message = "Test";

         return View();
      }


      [HandleError(ExceptionType = typeof(ArgumentException), View = "ErrorArgument")]
      [HandleError(ExceptionType = typeof(SqlException), View = "ErrorSql")]
      public ActionResult ErrorTest(string errorType) {
         if (errorType == "1") {
            throw new Exception("Error1");
         }
         if (errorType == "2") {
            throw new ArgumentException("Error2");

         }

         return Content("NO ERROR");
      }
   }
}