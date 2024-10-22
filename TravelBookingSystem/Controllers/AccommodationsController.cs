using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TravelBookingSystem.Models;

namespace TravelBookingSystem.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AccommodationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Accommodations
        public ActionResult Index()
        {
            return View(db.Accommodations.ToList());
        }

        // GET: Accommodations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Accommodation accommodation = db.Accommodations.Find(id);
            if (accommodation == null)
            {
                return HttpNotFound();
            }
            return View(accommodation);
        }

        // GET: Accommodations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Accommodations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,AccommodationImageURL")] Accommodation accommodation)
        {
            if (ModelState.IsValid)
            {
                db.Accommodations.Add(accommodation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(accommodation);
        }

        // GET: Accommodations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Accommodation accommodation = db.Accommodations.Find(id);
            if (accommodation == null)
            {
                return HttpNotFound();
            }
            return View(accommodation);
        }

        // POST: Accommodations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,AccommodationImageURL")] Accommodation accommodation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(accommodation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(accommodation);
        }

        // GET: Accommodations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Accommodation accommodation = db.Accommodations.Find(id);
            if (accommodation == null)
            {
                return HttpNotFound();
            }
            return View(accommodation);
        }

        // POST: Accommodations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Accommodation accommodation = db.Accommodations.Find(id);
            db.Accommodations.Remove(accommodation);
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
