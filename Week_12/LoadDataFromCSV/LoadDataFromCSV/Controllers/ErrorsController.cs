using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoadDataFromCSV.Controllers
{
    // Inspiration: http://stackoverflow.com/a/9026907 

    public sealed class ErrorsController : Controller
    {
        public ActionResult NotFound()
        {
            ViewBag.StatusCode = Response.StatusCode.ToString();

            return View();
        }

        public ActionResult ServerError()
        {
            ViewBag.StatusCode = Response.StatusCode.ToString();

            // The view will show the error message 
            // if running on localhost (for you, the programmer)

            // Otherwise, you must hover your mouse over the
            // "Technical information" error message

            ViewBag.Prompt = "Error message:";
            ViewBag.Error = HttpContext.Error.Message;
            ViewBag.ErrorDetail = HttpContext.Error.InnerException.Message;

            return View();
        }
    }
}
