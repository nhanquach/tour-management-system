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
        public static Tour globalTour;

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
            ViewBag.pending = db.Bills.Where(b => b.Status == BillStatus.Pending).Count();
            ViewBag.completed = db.Bills.Where(b => b.Status == BillStatus.Complete).Count();
            ViewBag.cancelled = db.Bills.Where(b => b.Status == BillStatus.Cancel).Count();
            return View("Home");
        }

        public ActionResult AddGroup()
        {
            ViewBag.tour = globalTour;
            return View();
        }

        [HttpPost]
        public ActionResult Save(TourGroup tourGroup)
        {
            tourGroup.TourID = globalTour.ID;
            if (ModelState.IsValid)
            {
                db.TourGroups.Add(tourGroup);
                db.SaveChanges();
            }
            return RedirectToAction("AddGroup", "Admin");
        }

        [HttpPost]
        public ActionResult Finish(TourGroup tourGroup)
        {
            tourGroup.TourID = globalTour.ID;
            if (ModelState.IsValid)
            {
                db.TourGroups.Add(tourGroup);
                db.SaveChanges();
            }
            return RedirectToAction("Home", "Admin");
        }

        public ActionResult AddLocations()
        {
            ViewBag.TourName = globalTour.TourName;
            return View(db.Locations.ToList());
        }

        [HttpPost]
        public ActionResult AddLocations(Array ID)
        {
            if (ID.Length > 0)
            {
                foreach (var item in ID)
                {
                    TourDetail tourDetail = new TourDetail
                    {
                        ID = globalTour.ID,
                        LocationID = int.Parse(item.ToString()),
                        TourID = globalTour.ID
                    };
                    db.TourDetails.Add(tourDetail);
                    db.SaveChanges();
                }
                return RedirectToAction("AddGroup", "Admin");
            }
            return RedirectToAction("AddLocations", "Admin");
        }

        public ActionResult CreateTour()
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
                globalTour = tour;
                System.Diagnostics.Debug.WriteLine("TOUR ID: ", tour.TourID);
                return RedirectToAction("AddLocations", "Admin");
            }
            else
            {
                ViewBag.error = "Please fill in every field before submit.";
            }
            return RedirectToAction("CreateTour", "Admin");
        }

        public ActionResult Locations()
        {
            return RedirectToAction("Index", "Locations");
        }

        public ActionResult Groups()
        {
            return RedirectToAction("Index", "TourGroups");
        }

        public ActionResult Tours()
        {
            return RedirectToAction("Index", "AdminTours");
        }

        public ActionResult Users()
        {
            return RedirectToAction("Index", "Users");
        }

        public ActionResult Bills()
        {
            return RedirectToAction("Index", "Bills");
        }
    }
}