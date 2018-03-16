using QuanLyTour.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyTour.Controllers
{
    public class HomeController : Controller
    {
        private TourContext db = new TourContext();
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Search(String q)
        {
            return RedirectToAction("Search", "Tours", new { q = q });
            //var tours = from tour in db.Tour select tour;
            //if (!String.IsNullOrEmpty(q))
            //{
            //    tours = tours.Where(t => t.TourName.Contains(q));
            //}
            //return View(tours.ToList());
        }
    }
}