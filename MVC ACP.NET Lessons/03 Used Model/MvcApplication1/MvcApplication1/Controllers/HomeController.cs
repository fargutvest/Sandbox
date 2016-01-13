using MvcApplication2.Models;
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
            TextModel tm = new TextModel(Server.MapPath("~/Content/MyFile.txt"));
            ViewData["message"] = tm.text;
            return View();
        }
    }
}
