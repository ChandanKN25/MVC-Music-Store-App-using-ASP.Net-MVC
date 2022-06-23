using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAPI.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AccessDenied()
        {
            Response.StatusCode = 403;
            ViewBag.ErrorMessages = "Access Denied";
            ViewBag.StackTrace = Response.StatusDescription;
            return View();
        }
    }
}