using MvcApplication1.Models;
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
            int hour = DateTime.Now.Hour;
            ViewData["greeting"] = (hour < 12 ? "Доброе утро" : "Добрый вечер");
            return View();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ViewResult RegForm()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ViewResult RegForm(GuestResponseModel guestResponse)
        {
            if (ModelState.IsValid)
            {
                return View("Thanks", guestResponse);
            }
            else
            {
                return View();
            }
        }
    }
}
