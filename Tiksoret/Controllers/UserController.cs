using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tiksoret.Models;
namespace Tiksoret.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        [HttpGet]
        public ActionResult Index()
        {
            using(OurDBContext db=new OurDBContext())
            {
                return View(db.user.ToList());
            }
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(User u)
        {
            if (ModelState.IsValid)
            {
                using(OurDBContext db=new OurDBContext())
                {
                    db.user.Add(u);
                    db.SaveChanges();
                }
                ModelState.Clear();
                ViewBag.Message = u.FirstName + " " + u.LastName + " successfully registered.";
                return RedirectToAction("Login");
            }
            return View();
        }

        //Login
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(User us)
        {
            using (OurDBContext db = new OurDBContext())
            {
                var usr = db.user.FirstOrDefault(u => u.Username == us.Username && u.Password == us.Password);
                if (usr!=null)
                {
                    Session["ID"] = usr.ID.ToString();
                    Session["Username"] = usr.Username.ToString();
                    Session["LastName"] = usr.LastName.ToString();
                    Session["FirstName"] = usr.FirstName.ToString();
                    Session["Email"] = usr.Email.ToString();
                    Session["PhoneNumber"] = usr.PhoneNumber.ToString();
                    return RedirectToAction("Index","FlightBook");
                }
                else
                {
                    ModelState.AddModelError("", "Username or Password is wrong");
                }
            }
            return View();
        }

        
    }
}