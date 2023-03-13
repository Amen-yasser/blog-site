using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using blogApp.Models;

namespace blogApp.Controllers
{
    public class HomeController : Controller
    {
        BlogContext db = new BlogContext();
        public ActionResult Index()
        {
            List<news> News = db.news.ToList();
            return View(News);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}