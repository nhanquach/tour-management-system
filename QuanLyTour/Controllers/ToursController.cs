using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QuanLyTour.DAL;
using QuanLyTour.Models;

namespace QuanLyTour.Controllers
{
    public class ToursController : Controller
    {
        private TourContext db = new TourContext();

        // GET: Tours
        public ActionResult Index()
        {
            return View(db.Tours.ToList());
        }

        // GET: Tours/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tour tour = db.Tours.Find(id);
            //var tourDetail = (from d in db.TourDetails where d.TourID.Equals(tour.TourID) select d).ToList(); 
            if (tour == null)
            {
                return HttpNotFound();
            }
            
            return View(tour);
        }

        public ActionResult Search(String q)
        {
            IQueryable<Tour> result = null;
            if (!String.IsNullOrEmpty(q))
            {
                result = from r in db.Tours where r.TourName.Contains(q) select r;
            }
            else
            {
                result = from r in db.Tours select r;
            }
            
            ViewBag.q = q;
            return View(result.ToList());
        }
        
        public ActionResult BookATour(int? id, int? groupId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TourGroup tourGroup = db.TourGroups.Find(groupId);
            ViewBag.tour = db.Tours.Find(id);
            if (tourGroup == null)
            {
                return HttpNotFound();
            }

            return View(tourGroup);
        }

        public void ConfirmBooking(int userId, int id, int groupId)
        {
            var tour = db.Tours.Find(id);

            var bill = new Bill {
                UserID = userId,
                TourGroupID = groupId,
                TourID = id,
                TourPrice = tour.TourPrice.ToString()
            };
            db.Bills.Add(bill);
            db.SaveChanges();
            /*
             public int ID { get; set; }
        public int CustomerID { get; set; }
        public int TourGroupID { get; set; }
        public int TourID { get; set; }
        public String TourPrice { get; set; }
        db.Tours.Add(tour);
             */

        }
    }
}
