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
        }
    }
}