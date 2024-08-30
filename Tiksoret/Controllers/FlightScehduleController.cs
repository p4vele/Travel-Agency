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
    public class FlightScehduleController : Controller
    {
        private OurDBContext db = new OurDBContext();

        // GET: FlightScehdule
        public ActionResult Index()
        {
            if (Session["u"] != null)
            {
                return View(db.TicketReserve_tbl.ToList());
            }
            else
            {
                return RedirectToAction("AdminLogin","Admin");
            }
          
        }

        // GET: FlightScehdule/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketReserve_tbl ticketReserve_tbl = db.TicketReserve_tbl.Find(id);
            if (ticketReserve_tbl == null)
            {
                return HttpNotFound();
            }
            return View(ticketReserve_tbl);
        }

        // GET: FlightScehdule/Create
        public ActionResult Create()
        {
            if (Session["u"] != null)
            {
                ViewBag.Types = new SelectList(db.PlaneInfo.ToList(), "PlaneID", "APlaneName", "0");
                var d= new SelectList(db.PlaneInfo.ToList(), "PlaneID", "APlaneName", "0").Where(p => p.Value == "1").First().Value;
                int dd = int.Parse(d);
                ViewBag.Types2 = db.PlaneInfo.Where(x => x.PlaneID == dd).FirstOrDefault().SeatCapacity;
                return View();
            }
            else
            {
                return RedirectToAction("AdminLogin", "Admin");
            }
            
        }

        // POST: FlightScehdule/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ResID,ResFrom,ResTo,ResDepDate,ResTime,PlaneID,PlaneSeat,ResTicketPrice,ResPlaneType")] TicketReserve_tbl ticketReserve_tbl)
        {
            if (ModelState.IsValid)
            {
                int z = int.Parse(Request["PlaneID"].ToString());
                int cap= db.PlaneInfo.Where(x => x.PlaneID == z).FirstOrDefault().SeatCapacity;
                string qq = Convert.ToString(z);
                db.TicketReserve_tbl.Add(ticketReserve_tbl);
                db.SaveChanges();
                db.TicketReserve_tbl.Where(x => x.PlaneID == qq).FirstOrDefault().changePlaneSeat(cap);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ticketReserve_tbl);
        }

        // GET: FlightScehdule/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketReserve_tbl ticketReserve_tbl = db.TicketReserve_tbl.Find(id);
            if (ticketReserve_tbl == null)
            {
                return HttpNotFound();
            }
            return View(ticketReserve_tbl);
        }

        // POST: FlightScehdule/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ResID,ResFrom,ResTo,ResDepDate,ResTime,PlaneID,PlaneSeat,ResTicketPrice,ResPlaneType")] TicketReserve_tbl ticketReserve_tbl)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ticketReserve_tbl).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ticketReserve_tbl);
        }

        // GET: FlightScehdule/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketReserve_tbl ticketReserve_tbl = db.TicketReserve_tbl.Find(id);
            if (ticketReserve_tbl == null)
            {
                return HttpNotFound();
            }
            return View(ticketReserve_tbl);
        }

        // POST: FlightScehdule/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TicketReserve_tbl ticketReserve_tbl = db.TicketReserve_tbl.Find(id);
            db.TicketReserve_tbl.Remove(ticketReserve_tbl);
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
