using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using blogApp.Models;

namespace blogApp.Controllers
{
    public class userController : Controller
    {
        // GET: user
        BlogContext db = new BlogContext();
        public ActionResult register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult register(user u,HttpPostedFileBase img)
        {
            // save ucser image
            img.SaveAs(Server.MapPath($"~/Attach/{img.FileName}"));
            u.photo = img.FileName;

            // chick email is found or not
            user em = db.users.Where(n => n.email == u.email).FirstOrDefault();
            if(em!=null)
            {
                ViewBag.status = "this E-mail has been registerd befor";
                return View();
            }

            // to save to database
            if(ModelState.IsValid)
            {
                db.users.Add(u);
                db.SaveChanges();
                return RedirectToAction("login");
            }
            return View();
        }
        public ActionResult login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult login(user u)
        {
            user us = db.users.Where(n => n.email == u.email&&n.password==u.password).FirstOrDefault();
            if(us!=null&& us.email== "admin@gmail.com")
            {
                Session.Add("adminId", us.id);
                return RedirectToAction("admin","admin");
            }
            if(us!=null)
            {
                Session.Add("userid", us.id);
                return RedirectToAction("profile");
            }
            else
            {
                ViewBag.status = "Invaled E-mail OR Password";
                return View();
            }
         
        }

        public ActionResult profile()
        {
            if (Session["userid"] == null) return RedirectToAction("login");
            int userId = (int)Session["userid"];
            user s = db.users.Where(n => n.id == userId).FirstOrDefault();
            return View(s);
        }
        public ActionResult logout()
        {
            Session["userid"] = null;
            Session["adminId"] = null;
            return RedirectToAction("login");
        }
        public ActionResult editProfile(int? id)
        {
            if (id == null) return RedirectToAction("profile");
            user User = db.users.Where(u => u.id == id).SingleOrDefault();
            return View(User);
        }
        [HttpPost]
        public ActionResult editProfile(user User, HttpPostedFileBase img)
        {
            if(img==null)
            {
                ViewBag.status = " You Must Insert Photo";
                return View();
            }
  
            if(ModelState.IsValid)
            {
                img.SaveAs(Server.MapPath($"~/Attach/{img.FileName}"));
                User.photo = img.FileName;
                User.id = (int)Session["userid"];
                db.Entry(User).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("profile");
            }
            return View();
        }
    }
}