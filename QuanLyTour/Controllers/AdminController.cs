using QuanLyTour.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyTour.Controllers.Utils;

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
                    System.Diagnostics.Debug.WriteLine(MD5.Create("Admin"));
                    return Home();
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

        public ActionResult Locations()
        {
            return View(db.Locations.ToList());
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