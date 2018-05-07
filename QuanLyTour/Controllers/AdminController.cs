using QuanLyTour.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyTour.Controllers.Utils;
using QuanLyTour.Models;

namespace QuanLyTour.Controllers
{
    public class AdminController : Controller
    {
        private TourContext db = new TourContext();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignIn(String username, String password)
        {
            ViewBag.error = "";
            if (!String.IsNullOrEmpty(username) && !String.IsNullOrEmpty(password))
            {
                if(username == "Admin" && MD5.Create(password) == "E3AFED0047B08059D0FADA10F400C1E5")
                {
                    return RedirectToAction("Home", "Admin");
                }
                else
                {
                    ViewBag.error = "Email or Password not match.";
                }
            }
            return View("Index");
        }

        public ActionResult Home()
        {
            ViewBag.numberOfLocations = db.Locations.Count();
            ViewBag.numberOfTours = db.Tours.Count();
            ViewBag.numberOfGroups = db.TourGroups.Count();
            ViewBag.numberOfUsers = db.Users.Count();
            return View("Home");
        }

        public ActionResult CreateTour()
        {
            return View();
        }

        public ActionResult AddLocations(string tourID)
        {
            ViewBag.tourID = tourID;
            return View(db.Locations.ToList());
        }

        [HttpPost]
        public ActionResult AddLocations(Location location)
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateNewTour(FormCollection formCollection, Tour tour)
        {
            string description = formCollection["Description"];
            tour.TourDescription = description;

            if (ModelState.IsValid)
            {
                db.Tours.Add(tour);
                db.SaveChanges();
                System.Diagnostics.Debug.WriteLine("TOUR ID: ", tour.TourID);
                return RedirectToAction("AddLocations", "Admin", new { tourID = tour.TourID});
            }
            else
            {
                ViewBag.error = "Please fill in every field before sumit.";
            }
            return RedirectToAction("CreateTour", "Admin");
        }

        public ActionResult Locations()
        {
            return RedirectToAction("Index", "Locations");
        }

        public ActionResult Groups()
        {
            return View(db.TourGroups.ToList());
        }

        public ActionResult Tours()
        {
            return View(db.TourGroups.ToList());
        }

        public ActionResult Users()
        {
            return View(db.Users.ToList());
        }
    }
}