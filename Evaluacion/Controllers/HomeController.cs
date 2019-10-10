using Evaluacion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Evaluacion.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if(Request.IsAuthenticated)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login","Account");
            }
            
        }

        public ActionResult About(Modulo modulo)
        {
            ModelState.AddModelError("ErrorNombre", ("No puede quedar en blanco"));
            
            return View();
        }



        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpPost,ActionName("Contact")]
        public ActionResult InfoContact()
        {

            return View();
        }
    }
}