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

        // GET: TourGroups
        public ActionResult Index()
        {
            return View(db.TourGroup.ToList());
        }

        // GET: TourGroups/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TourGroup tourGroup = db.TourGroup.Find(id);
            if (tourGroup == null)
            {
                return HttpNotFound();
            }
            return View(tourGroup);
        }

        // GET: TourGroups/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TourGroups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Description,LeaveDate,ReturnDate,TourID,Status")] TourGroup tourGroup)
        {
            if (ModelState.IsValid)
            {
                db.TourGroup.Add(tourGroup);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tourGroup);
        }

        // GET: TourGroups/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TourGroup tourGroup = db.TourGroup.Find(id);
            if (tourGroup == null)
            {
                return HttpNotFound();
            }
            return View(tourGroup);
        }

        // POST: TourGroups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Description,LeaveDate,ReturnDate,TourID,Status")] TourGroup tourGroup)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tourGroup).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tourGroup);
        }

        // GET: TourGroups/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TourGroup tourGroup = db.TourGroup.Find(id);
            if (tourGroup == null)
            {
                return HttpNotFound();
            }
            return View(tourGroup);
        }

        // POST: TourGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TourGroup tourGroup = db.TourGroup.Find(id);
            db.TourGroup.Remove(tourGroup);
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
