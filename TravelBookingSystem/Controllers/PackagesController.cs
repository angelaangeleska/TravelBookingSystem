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
    public class PackagesController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        // GET: Packages
        public ActionResult Index(int page = 1, int pageSize = 6)
        {
            var packages = db.Packages.Include(p => p.Accommodation)
                                       .Include(p => p.Destination);

            var totalPackages = packages.Count();

            var pagedPackages = packages.OrderBy(p => p.Id) 
                                        .Skip((page - 1) * pageSize)
                                        .Take(pageSize)
                                        .ToList();

            ViewBag.TotalPages = Math.Ceiling((double)totalPackages / pageSize);
            ViewBag.CurrentPage = page;

            return View(pagedPackages);
        }


        // GET: Packages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Package package = db.Packages.Find(id);
            if (package == null)
            {
                return HttpNotFound();
            }
            return View(package);
        }

        // GET: Packages/Create
        public ActionResult Create()
        {
            ViewBag.AccommodationId = new SelectList(db.Accommodations, "Id", "Name");
            ViewBag.DestinationId = new SelectList(db.Destinations, "Id", "Name");
            return View();
        }

        // POST: Packages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,DestinationId,Price,StartDate,EndDate,AccommodationId,AvailablePlaces")] Package package)
        {
            if (ModelState.IsValid)
            {
                db.Packages.Add(package);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AccommodationId = new SelectList(db.Accommodations, "Id", "Name", package.AccommodationId);
            ViewBag.DestinationId = new SelectList(db.Destinations, "Id", "Name", package.DestinationId);
            return View(package);
        }

        // GET: Packages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Package package = db.Packages.Find(id);
            if (package == null)
            {
                return HttpNotFound();
            }
            ViewBag.AccommodationId = new SelectList(db.Accommodations, "Id", "Name", package.AccommodationId);
            ViewBag.DestinationId = new SelectList(db.Destinations, "Id", "Name", package.DestinationId);
            return View(package);
        }

        // POST: Packages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DestinationId,Price,StartDate,EndDate,AccommodationId,AvailablePlaces")] Package package)
        {
            if (ModelState.IsValid)
            {
                db.Entry(package).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AccommodationId = new SelectList(db.Accommodations, "Id", "Name", package.AccommodationId);
            ViewBag.DestinationId = new SelectList(db.Destinations, "Id", "Name", package.DestinationId);
            return View(package);
        }

        // GET: Packages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Package package = db.Packages.Find(id);
            if (package == null)
            {
                return HttpNotFound();
            }
            return View(package);
        }

        // POST: Packages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Package package = db.Packages.Find(id);
            db.Packages.Remove(package);
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
