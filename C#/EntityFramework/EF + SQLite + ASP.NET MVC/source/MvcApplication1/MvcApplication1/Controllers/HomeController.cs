using Shop.DAL;
using Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using (ShopContext context = new ShopContext())
            {
                Product p = new Product { ID = 0, NameProduct = "Aspirin", CountProductUnit = 628, Barcode = "3265462" };
                context.Product.Add(p);
                context.SaveChanges();

                //var product = context.Product;
                //var i = product.Count();
                //foreach (Product pr in product )
                //{

                //}
            }

            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";
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
