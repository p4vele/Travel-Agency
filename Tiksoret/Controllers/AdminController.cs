using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tiksoret.Models;

namespace Tiksoret.Controllers
{
    public class AdminController : Controller
    {
        OurDBContext c = new OurDBContext();
        // GET: Admin
        public ActionResult Index()
        {
            if (Session["u"] != null)
            {
                return View();
                
            }
            else
            {
                return RedirectToAction("AdminLogin");
            }
        }
        [HttpGet]
        public ActionResult AdminLogin()
        {
            if (Session["u"] != null)
            {
                return RedirectToAction("Dashboard");
            }
            else
            {
                return View();
            }
            
        }
        [HttpPost]
        public ActionResult AdminLogin(Admin l )
        {
            var x = c.admin.Where(a => a.AUsername.Equals(l.AUsername) && a.APassword.Equals( l.APassword)).FirstOrDefault();
            if (x != null)
            {
                Session["u"] = l.AUsername;
                return RedirectToAction("Dashboard");
            }
            else
            {
                ModelState.AddModelError("", "Username or Password is wrong");
                return View();
            }
            
        }
        public ActionResult Dashboard()
        {
            if (Session["u"] != null)
            {
                return View();

            }
            else
            {
                return RedirectToAction("AdminLogin");
            }
        }
    }
}