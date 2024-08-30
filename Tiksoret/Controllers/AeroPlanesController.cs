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
    public class AeroPlanesController : Controller
    {
        private OurDBContext db = new OurDBContext();

        // GET: AeroPlanes
        public ActionResult Index()
        {
            if (Session["u"] != null)
            {
                return View(db.PlaneInfo.ToList());
            }
            else
            {
                return RedirectToAction("AdminLogin","Admin");
            }
            
        }

        // GET: AeroPlanes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            APlane aPlane = db.PlaneInfo.Find(id);
            if (aPlane == null)
            {
                return HttpNotFound();
            }
            return View(aPlane);
        }

        // GET: AeroPlanes/Create
        public ActionResult Create()
        {
            if (Session["u"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("AdminLogin", "Admin");
            }
        }

        // POST: AeroPlanes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PlaneID,APlaneName,SeatCapacity,Price")] APlane aPlane)
        {
            if (ModelState.IsValid)
            {
                db.PlaneInfo.Add(aPlane);
                db.SaveChanges();
                ViewBag.m = "Record Set";
                //return View();
                return RedirectToAction("Index");
            }

            return View(aPlane);
        }

        // GET: AeroPlanes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            APlane aPlane = db.PlaneInfo.Find(id);
            if (aPlane == null)
            {
                return HttpNotFound();
            }
            return View(aPlane);
        }

        // POST: AeroPlanes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PlaneID,APlaneName,SeatCapacity,Price")] APlane aPlane)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aPlane).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(aPlane);
        }

        // GET: AeroPlanes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            APlane aPlane = db.PlaneInfo.Find(id);
            if (aPlane == null)
            {
                return HttpNotFound();
            }
            return View(aPlane);
        }

        // POST: AeroPlanes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            APlane aPlane = db.PlaneInfo.Find(id);
            db.PlaneInfo.Remove(aPlane);
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
