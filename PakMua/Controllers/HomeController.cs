using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PakMua.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Session["role"] != null && Session["role"].ToString().Trim() == "admin")
            {
                return RedirectToAction("Review", "Order");
            }

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Welcome to Rentalan.com";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Hubungi kami";

            return View();
        }
    }
}