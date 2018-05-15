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
    public class TourGroupsController : Controller
    {
        private TourContext db = new TourContext();
        public String viewLink(String view)
        {
            string defaultLink = "~/Views/Admin/TourGroups/";
            return defaultLink + view;
        }

        // GET: TourGroups
        public ActionResult Index(String q)
        {
            //var tourGroups = db.TourGroups.Include(t => t.Tour);
            var tourGroups = from t in db.TourGroups select t;
            if (!String.IsNullOrEmpty(q))
            {
                var query = q.ToUpper();
                tourGroups = db.TourGroups.Where(
                    t => t.Name.ToUpper().Contains(query)
                || t.Tour.TourName.ToUpper().Contains(query)
                || t.LeaveDate.ToString().Contains(query)
                || t.Status.ToString().ToUpper().Contains(query) 
                || t.NumberOfPeople.ToString().Contains(query)
                );
            }
            return View(viewLink("Index.cshtml"), tourGroups.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TourGroup tourGroup = db.TourGroups.Find(id);
            if (tourGroup == null)
            {
                return HttpNotFound();
            }
            return View(viewLink("Details.cshtml"), tourGroup);
        }

        public ActionResult Create()
        {
            ViewBag.TourID = new SelectList(db.Tours, "ID", "TourName");
            return View(viewLink("Create.cshtml"));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,TourGroupID,Name,Description,LeaveDate,ReturnDate,TourID,Status,NumberOfPeople")] TourGroup tourGroup)
        {
            if (ModelState.IsValid)
            {
                db.TourGroups.Add(tourGroup);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TourID = new SelectList(db.Tours, "ID", "TourName", tourGroup.TourID);
            return View(viewLink("Create.cshtml"), tourGroup);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TourGroup tourGroup = db.TourGroups.Find(id);
            if (tourGroup == null)
            {
                return HttpNotFound();
            }
            ViewBag.TourID = new SelectList(db.Tours, "ID", "TourName", tourGroup.TourID);
            return View(viewLink("Edit.cshtml"), tourGroup);
        }

        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,TourGroupID,Name,Description,LeaveDate,ReturnDate,TourID,Status,NumberOfPeople")] TourGroup tourGroup)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tourGroup).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TourID = new SelectList(db.Tours, "ID", "TourName", tourGroup.TourID);
            return View(viewLink("Edit.cshtml"), tourGroup);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TourGroup tourGroup = db.TourGroups.Find(id);
            if (tourGroup == null)
            {
                return HttpNotFound();
            }
            return View(viewLink("Delete.cshtml"), tourGroup);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TourGroup tourGroup = db.TourGroups.Find(id);
            db.TourGroups.Remove(tourGroup);
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
