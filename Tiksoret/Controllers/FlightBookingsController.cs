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
    public class FlightBookingsController : Controller
    {
        private OurDBContext db = new OurDBContext();

        // GET: FlightBookings
        public ActionResult Index()
        {
            var flightBookings = db.FlightBookings.Include(f => f.TicketReserve_tbls);
            return View(flightBookings.ToList());
        }
        public ActionResult Index2()
        {
            var flightBookings = db.FlightBookings.Include(f => f.TicketReserve_tbls);
            return View(flightBookings.ToList());
        }

        // GET: FlightBookings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FlightBooking flightBooking = db.FlightBookings.Find(id);
            if (flightBooking == null)
            {
                return HttpNotFound();
            }
            return View(flightBooking);
        }

        // GET: FlightBookings/Create
        public ActionResult Create(string fid)
        {
            int ids = int.Parse(fid);
            var a = db.TicketReserve_tbl.Where(l=>l.ResID==ids).SingleOrDefault();
            ViewBag.id = fid;
            ViewBag.test = db.TicketReserve_tbl.Where(l => l.ResID == ids).FirstOrDefault().ResTicketPrice;
            ViewBag.ResID = new SelectList(db.TicketReserve_tbl, "ResID", "ResID").Where(l=>l.Value.Equals(fid)).FirstOrDefault().Value;
            return View();
        }

        // POST: FlightBookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookingID,bCusName,bCusAdress,bCusEmail,bCusSeats,bPhoneNum,bCusID,TotalPrice,ResID")] FlightBooking flightBooking)
        {
            if (ModelState.IsValid)
            {
                db.FlightBookings.Add(flightBooking);
                
                int q = db.TicketReserve_tbl.Where(x => x.ResID == flightBooking.ResID).FirstOrDefault().PlaneSeat;
                int num = int.Parse(Request["bCusSeats"].ToString());
                if(q- num < 0 )
                {
                    ViewBag.msg = "You cannot order more seats than avilable!";
                    ViewBag.ResID = db.TicketReserve_tbl.Where(l => l.ResID== flightBooking.ResID).FirstOrDefault().ResID;
                    //int zz = int.Parse(Request["ResID"].ToString());
                    ViewBag.test = db.TicketReserve_tbl.Where(l => l.ResID == flightBooking.ResID).FirstOrDefault().ResTicketPrice;
                    return View(flightBooking);

                    
                }
                //db.TicketReserve_tbl.Where(x => x.ResID == flightBooking.ResID).FirstOrDefault().changePlaneSeat(q - num);
                db.SaveChanges();
                return RedirectToAction("Create","Payments",new {num= flightBooking.BookingID });
            }

            ViewBag.ResID = db.TicketReserve_tbl.Where(l => l.ResID == flightBooking.ResID).FirstOrDefault().ResID;
            //ViewBag.ResID = new SelectList(db.TicketReserve_tbl, "ResID", "ResFrom", flightBooking.ResID);
            //int z= int.Parse(Request["ResID"].ToString());
            ViewBag.test = db.TicketReserve_tbl.Where(l => l.ResID == flightBooking.ResID).FirstOrDefault().ResTicketPrice;
            return View(flightBooking);
        }

        // GET: FlightBookings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FlightBooking flightBooking = db.FlightBookings.Find(id);
            if (flightBooking == null)
            {
                return HttpNotFound();
            }
            ViewBag.ResID = new SelectList(db.TicketReserve_tbl, "ResID", "ResFrom", flightBooking.ResID);
            //ViewBag.test = db.TicketReserve_tbl.Where(l => l.ResID == id).FirstOrDefault().ResTicketPrice;
            return View(flightBooking);
        }
        

        // POST: FlightBookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookingID,bCusName,bCusAdress,bCusEmail,bCusSeats,bPhoneNum,bCusID,TotalPrice,ResID")] FlightBooking flightBooking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(flightBooking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index2");
            }
            ViewBag.ResID = new SelectList(db.TicketReserve_tbl, "ResID", "ResFrom", flightBooking.ResID);
            return View(flightBooking);
        }

        // GET: FlightBookings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FlightBooking flightBooking = db.FlightBookings.Find(id);
            if (flightBooking == null)
            {
                return HttpNotFound();
            }
            return View(flightBooking);
        }

        // POST: FlightBookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FlightBooking flightBooking = db.FlightBookings.Find(id);
            db.FlightBookings.Remove(flightBooking);
            db.SaveChanges();
            return RedirectToAction("Index2");
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
