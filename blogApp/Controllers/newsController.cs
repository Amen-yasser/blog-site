using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using blogApp.Models;

namespace blogApp.Controllers
{
    public class newsController : Controller
    {
        // GET: news
        BlogContext db = new BlogContext();
        public ActionResult AddNews()
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("login", "user");
            }
            SelectList cats = new SelectList(db.catalogs.ToList(), "id", "name");
            ViewBag.catalogs = cats;
            return View();
        }
        [HttpPost]
        public ActionResult AddNews(news n,HttpPostedFileBase img)
        {
            // save images
            img.SaveAs(Server.MapPath($"~/Attach/imageNews/{img.FileName}"));
            n.photo = img.FileName;

            n.userId = (int)Session["userid"];
            n.date = DateTime.Now;
            db.news.Add(n);
            db.SaveChanges();
            return RedirectToAction("myNews");

        }
        public ActionResult myNews()
        {
            if (Session["userid"] == null) return RedirectToAction("login", "user");
            int userId = (int)Session["userid"];
            List<news> News = db.news.Where(n => n.userId == userId).ToList();
            return View(News);
        }
        public ActionResult AllNews()
        {
            List<news> News = db.news.ToList();
            return View(News);
        }
        public ActionResult details(int id)
        {
            news New = db.news.Where(n => n.id == id).FirstOrDefault();
            return View(New);
        }
        public ActionResult delete(int id)
        {
            news New = db.news.Where(n => n.id == id).SingleOrDefault();
            db.news.Remove(New);
            db.SaveChanges();
            return RedirectToAction("myNews");
        }
        public ActionResult update(int id)
        {
            news New = db.news.Where(n => n.id == id).SingleOrDefault();
            SelectList cats = new SelectList(db.catalogs.ToList(), "id", "name");
            ViewBag.catalogs = cats;
            return View(New);
        }
        [HttpPost]
        public ActionResult update(news New,HttpPostedFileBase img)
        {
            //save image
            img.SaveAs(Server.MapPath($"~/Attach/imageNews/{img.FileName}"));
            New.photo = img.FileName;
            New.userId = (int)Session["userid"];
            New.date = DateTime.Now;
            db.Entry(New).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("myNews");
        }
    }
}