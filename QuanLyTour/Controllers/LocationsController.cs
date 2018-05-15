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
    public class LocationsController : Controller
    {
        private TourContext db = new TourContext();

        public String viewLink(String view)
        {
            string defaultLink = "~/Views/Admin/Locations/";
            return defaultLink + view;
        }

        public ActionResult Index(string q)
        {
            var locations = from l in db.Locations select l;
            if (!String.IsNullOrEmpty(q))
            {
                var query = q.ToUpper();
                locations = db.Locations.Where(
                    l => l.LocationName.ToUpper().Contains(query) 
                    || l.LocationDescription.ToUpper().Contains(query)
                    );
            }
            return View( viewLink("Index.cshtml"), locations.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Location location = db.Locations.Find(id);
            if (location == null)
            {
                return HttpNotFound();
            }
            return View(viewLink("Details.cshtml"), location);
        }

        public ActionResult Create()
        {
            return View(viewLink("Create.cshtml"));
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,LocationID,LocationName,LocationDescription,Country")] Location location)
        {
            if (ModelState.IsValid)
            {
                db.Locations.Add(location);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(viewLink("Create.cshtml"), location);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Location location = db.Locations.Find(id);
            if (location == null)
            {
                return HttpNotFound();
            }
            return View(viewLink("Edit.cshtml"), location);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,LocationID,LocationName,LocationDescription,Country")] Location location)
        {
            if (ModelState.IsValid)
            {
                db.Entry(location).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(location);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Location location = db.Locations.Find(id);
            if (location == null)
            {
                return HttpNotFound();
            }
            return View(viewLink("Edit.cshtml"), location);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Location location = db.Locations.Find(id);
            db.Locations.Remove(location);
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
