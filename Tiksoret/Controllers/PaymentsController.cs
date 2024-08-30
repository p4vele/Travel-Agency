using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Tiksoret.Models;

namespace Tiksoret.Controllers
{
    public class PaymentsController : Controller
    {
        private OurDBContext db = new OurDBContext();

        // GET: Payments
        public ActionResult Index()
        {
            return View(db.payment.ToList());
        }

        // GET: Payments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = db.payment.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
        }

        // GET: Payments/Create
        public ActionResult Create(int num)
        {
            int resid = db.FlightBookings.Where(x => x.BookingID == num).FirstOrDefault().ResID;
            float priceOne = db.TicketReserve_tbl.Where(l => l.ResID == resid).FirstOrDefault().ResTicketPrice;
            int q = db.FlightBookings.Where(l => l.ResID == resid && l.BookingID==num && l.TotalPrice==priceOne).FirstOrDefault().bCusSeats;
            float t = priceOne * q;
            ViewBag.id = resid.ToString();
            ViewBag.pp = t;
            ViewBag.name = db.FlightBookings.Where(l => l.BookingID == num).FirstOrDefault().bCusName;
            ViewBag.qq = q;
            ViewBag.pc = priceOne;
            return View();
            
        }

        // POST: Payments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PID,UserID,ResID,LastName,FirstName,Email,CreditCardNumber,Digits,ExD,ExM,Price")] Payment payment,int num)
        {
            int redids = int.Parse(Request.Form["ResID"]);
            if (ModelState.IsValid)
            {
                db.payment.Add(payment);
                int pss = db.TicketReserve_tbl.Where(x => x.ResID == redids).FirstOrDefault().PlaneSeat;
                float priceOnee = db.TicketReserve_tbl.Where(l => l.ResID == redids).FirstOrDefault().ResTicketPrice;
                int bcs = db.FlightBookings.Where(l => l.ResID == redids && l.BookingID == num && l.TotalPrice == priceOnee).FirstOrDefault().bCusSeats;
                db.TicketReserve_tbl.Where(x => x.ResID == redids).FirstOrDefault().changePlaneSeat(pss - bcs);
                db.SaveChanges();
                
                return RedirectToAction("Index2","FlightBookings");
            }
            int resid = db.FlightBookings.Where(x => x.BookingID == num).FirstOrDefault().ResID;
            float priceOne = db.TicketReserve_tbl.Where(l => l.ResID == redids).FirstOrDefault().ResTicketPrice;
            int q = db.FlightBookings.Where(l => l.ResID == redids && l.BookingID == num && l.TotalPrice == priceOne).FirstOrDefault().bCusSeats;
            float t = priceOne * q;
            ViewBag.id = redids.ToString();
            ViewBag.pp = t;
            ViewBag.name = db.FlightBookings.Where(l => l.BookingID == num).FirstOrDefault().bCusName;
            ViewBag.qq = q;
            ViewBag.pc = priceOne;
            return View(payment);
        }

        // GET: Payments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = db.payment.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
        }

        // POST: Payments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PID,UserID,ResID,LastName,FirstName,Email,CreditCardNumber,Digits,ExD,ExM,Price")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(payment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(payment);
        }

        // GET: Payments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = db.payment.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
        }

        // POST: Payments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Payment payment = db.payment.Find(id);
            db.payment.Remove(payment);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
