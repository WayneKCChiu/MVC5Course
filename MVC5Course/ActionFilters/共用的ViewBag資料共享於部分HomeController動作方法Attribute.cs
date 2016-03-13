﻿using System;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
   public class 共用的ViewBag資料共享於部分HomeController動作方法Attribute: ActionFilterAttribute
   {
      public override void OnActionExecuting(ActionExecutingContext filterContext) {
         filterContext.Controller.ViewBag.description = "Your application description page.";

         base.OnActionExecuting(filterContext);
      }
   }
}