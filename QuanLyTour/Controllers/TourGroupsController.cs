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
        public ActionResult Index()
        {
            var tourGroups = db.TourGroups.Include(t => t.Tour);
            return View(viewLink("Index.cshtml"), tourGroups.ToList());
        }

        // GET: TourGroups/Details/5
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

        // GET: TourGroups/Create
        public ActionResult Create()
        {
            ViewBag.TourID = new SelectList(db.Tours, "ID", "TourName");
            return View(viewLink("Create.cshtml"));
        }

        // POST: TourGroups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,TourGroupID,Name,Description,LeaveDate,ReturnDate,TourID,Status,NumberOfPeople")] TourGroup tourGroup)
        {
            if (ModelState.IsValid)
            {
                db.TourGroups.Add(tourGroup);
                db.SaveChanges();
                return RedirectToAction(viewLink("Index.cshtml"));
            }

            ViewBag.TourID = new SelectList(db.Tours, "ID", "TourName", tourGroup.TourID);
            return View(viewLink("Create.cshtml"), tourGroup);
        }

        // GET: TourGroups/Edit/5
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

        // POST: TourGroups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,TourGroupID,Name,Description,LeaveDate,ReturnDate,TourID,Status,NumberOfPeople")] TourGroup tourGroup)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tourGroup).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction(viewLink("Index.cshtml"));
            }
            ViewBag.TourID = new SelectList(db.Tours, "ID", "TourName", tourGroup.TourID);
            return View(viewLink("Edit.cshtml"), tourGroup);
        }

        // GET: TourGroups/Delete/5
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

        // POST: TourGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TourGroup tourGroup = db.TourGroups.Find(id);
            db.TourGroups.Remove(tourGroup);
            db.SaveChanges();
            return RedirectToAction(viewLink("Index.cshtml"));
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
