using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication1.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult Index()
        {
            ViewData["message"] = "Сейчас время " + DateTime.Now.TimeOfDay;

            return View();
        }
    }
}
