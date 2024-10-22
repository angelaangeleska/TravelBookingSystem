using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelBookingSystem.Models;

namespace TravelBookingSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var randomPackages = db.Packages
                .OrderBy(p => Guid.NewGuid()) 
                .Take(3) 
                .ToList();

            ViewBag.RandomPackages = randomPackages;
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Search(string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                ViewBag.Message = "Please enter a search term.";
                return View(new List<Package>());
            }

            var results = db.Packages
                .Where(p => p.Destination.Name.Contains(query) || p.Accommodation.Name.Contains(query))
                .ToList();

            return View(results);
        }
    }
}