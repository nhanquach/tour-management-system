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

        public ActionResult Index()
        {
            return View(db.Tours.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Tour tour = db.Tours.Find(id);

            var ListOfGroup = db.TourGroups.Where(g => g.TourID == id).ToArray();
            
            for(var i = 0; i < ListOfGroup.Length; i++)
            {
                var groupId = ListOfGroup[i].ID;
                var Seats = ListOfGroup[i].NumberOfPeople;
                var tickets = 0;

                var BookedSeats = db.Bills.Where(b => b.TourGroupID == groupId).ToArray();

                for (var j = 0; j < BookedSeats.Length; j++)
                {
                    tickets = tickets + BookedSeats[j].NumberOfTicket;
                }

                ListOfGroup[i].NumberOfPeople = Seats - tickets;
            }
                    
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

        [HttpPost]
        public ActionResult selectNumberOfTicket (int id, int groupId, int UserID, int numberOfTicket)
        {
            return RedirectToAction("ConfirmBooking", "Tours", new { UserID, id, groupId, tickets = numberOfTicket});
        }

        public ActionResult ConfirmBooking(int userId, int id, int groupId, int tickets)
        {
            var tour = db.Tours.Find(id);

            var bill = new Bill {
                UserID = userId,
                TourGroupID = groupId,
                TourID = id,
                TourPrice = tour.TourPrice.ToString(),
                NumberOfTicket = tickets,
            };
            db.Bills.Add(bill);
            db.SaveChanges();
            return RedirectToAction("Booked", bill);
        }

        public ActionResult Booked(Bill bill)
        {
            var tour = db.Tours.Find(bill.TourID);
            var tourGroup = db.TourGroups.Find(bill.TourGroupID);
            ViewBag.Tour = tour;
            ViewBag.TourGroup = tourGroup;
            ViewBag.Bill = bill;
            return View();
        }

        public ActionResult AllBookedTours()
        {
            return RedirectToAction("Home", "User");
        }
    }
}
