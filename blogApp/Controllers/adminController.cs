using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using blogApp.Models;

namespace blogApp.Controllers
{
    public class adminController : Controller
    {
        // GET: admin
        BlogContext db = new BlogContext();
        public ActionResult admin()
        {
            if (Session["adminId"] == null) return RedirectToAction("login", "user");
        
            var admin = db.users.Where(a => a.username == "admin").Select(n=>n.username).SingleOrDefault();
            List<user> User = db.users.ToList();
            List<catalog> Catalogs = db.catalogs.ToList();
            ViewBag.cats = Catalogs;
            ViewBag.adminName = admin;
            return View(User);
        }
        public ActionResult delete(int id)
        {
            user User = db.users.Where(u => u.id == id).SingleOrDefault();
            db.users.Remove(User);
            db.SaveChanges();
            return RedirectToAction("admin");
        }
    }
}