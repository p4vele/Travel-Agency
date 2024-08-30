using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tiksoret.Models;

namespace Tiksoret.Controllers
{
    public class PayPalController : Controller
    {
        private OurDBContext db = new OurDBContext();
        public ActionResult Index(int num)
        {
            
            float p= db.TicketReserve_tbl.Where(l => l.ResID == num).FirstOrDefault().ResTicketPrice;
            int q = db.FlightBookings.Where(l => l.ResID == num).FirstOrDefault().bCusSeats;
            float t = p * q;
            ViewBag.pp = t;
            ViewBag.name= db.FlightBookings.Where(l => l.ResID == num).FirstOrDefault().bCusName;
            ViewBag.qq = q;
            ViewBag.pc = p;
            return View();
        }
        public ActionResult Success()
        {
            ViewBag.result = PDTHolder.Success(Request.QueryString.Get("tx"));
            return View("Success");
        }
    }
}