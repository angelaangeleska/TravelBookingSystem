using Microsoft.AspNet.Identity;
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
    public class ReservationsController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        // GET: Reservations
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var reservations = db.Reservations.Include(r => r.Package);
            return View(reservations.ToList());
        }

        // GET: Reservations/UserReservations
        [Authorize(Roles = "User")]
        public ActionResult UserReservations()
        {
            var userEmail = User.Identity.GetUserName();
            var reservations = db.Reservations.Where(r => r.UserEmail == userEmail).ToList();
            return View(reservations);
        }

        // GET: Reservations/UpdateStatus/5
        [Authorize(Roles = "Admin")]
        public ActionResult UpdateStatus(int id)
        {
            var reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }


        // POST: Reservations/UpdateStatusConfirmed
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateStatusConfirmed(int id, FormCollection collection)
        {
            var newStatus = (ReservationStatus)Enum.Parse(typeof(ReservationStatus), collection["Status"]);

            var reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }

            if (reservation.Status != ReservationStatus.Pending)
            {
                ModelState.AddModelError("", "You can only update the status if it is currently 'Pending'.");
                return View(reservation); 
            }

            reservation.Status = newStatus;

            if (newStatus == ReservationStatus.Rejected)
            {
                var package = db.Packages.Find(reservation.PackageId);
                if (package != null)
                {
                    package.AvailablePlaces += reservation.TotalPeople;
                    db.Entry(package).State = EntityState.Modified;
                }
            }

            db.Entry(reservation).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index");
        }


        // GET: Reservations/Create
        [Authorize]
        public ActionResult Create(int packageId)
        {
            var package = db.Packages.Find(packageId);
            if (package == null)
            {
                return HttpNotFound();  
            }

            var reservation = new Reservation
            {
                PackageId = packageId,
                UserEmail = User.Identity.Name, 
                TotalCost = 0 
            };

            ViewBag.PackagePrice = package.Price; 

            return View(reservation);
        }


        // POST: Reservations/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                var package = db.Packages.Find(reservation.PackageId);
                if (package != null)
                {
                    int totalPeople = reservation.NumberOfAdults + reservation.NumberOfChildren;

                    if (totalPeople > package.AvailablePlaces)
                    {
                        ModelState.AddModelError("", "The number of people exceeds the available places.");
                        return View(reservation);
                    }

                    package.AvailablePlaces -= totalPeople;
                    db.Entry(package).State = EntityState.Modified;

                    reservation.Status = ReservationStatus.Pending;
                    reservation.TotalCost = (reservation.NumberOfAdults * package.Price) + (reservation.NumberOfChildren * package.Price * 0.5m);
                    reservation.CreatedDate = DateTime.Now;
                    db.Reservations.Add(reservation);
                    db.SaveChanges();

                    var depositAmount = reservation.TotalCost * 0.30m;

                    TempData["ConfirmationMessage"] = $"To confirm your reservation, a 30% deposit of ${depositAmount:F2} is required today. You can pay at our agency offices or via bank transfer to account '0123456789'.";

                    return RedirectToAction("Index", "Packages");
                }
            }

            return View(reservation);
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
