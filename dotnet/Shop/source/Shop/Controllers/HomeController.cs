using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult Index()
        {
            int hour = DateTime.Now.Hour;
            ViewData["greeting"] = (hour < 12 ? "Доброе утро" : "Добрый вечер");
            return View();

            //using (ShopEntities context = new ShopEntities())
            //{
            //    foreach (Products p in context.Products)
            //    {

            //    }
            //}
        }
        
        public ViewResult AdminForm()
        {
            return View();
        }

        
        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
