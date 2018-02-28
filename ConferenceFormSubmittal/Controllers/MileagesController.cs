using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ConferenceFormSubmittal.DAL;
using ConferenceFormSubmittal.Models;

namespace ConferenceFormSubmittal.Controllers
{
    public class MileagesController : Controller
    {
        private CFSEntities db = new CFSEntities();

        // GET: Mileages
        public ActionResult Index()
        {
            var mileages = db.Mileages.Include(m => m.Application).Include(m => m.Employee).Include(m => m.Status);
            return View(mileages.ToList());
        }

        // GET: Mileages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mileage mileage = db.Mileages.Find(id);
            if (mileage == null)
            {
                return HttpNotFound();
            }
            return View(mileage);
        }

        // GET: Mileages/Create
        public ActionResult Create()
        {
            ViewBag.ApplicationID = new SelectList(db.Applications, "ID", "Rationale");
            ViewBag.EmployeeID = new SelectList(db.Employees, "ID", "FirstName");
            ViewBag.StatusID = new SelectList(db.Statuses, "ID", "Description");
            return View();
        }

        // POST: Mileages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,TravelDate,StartAddress,EndAddress,Kilometres,Feedback,StatusID,EmployeeID,ApplicationID")] Mileage mileage)
        {
            if (ModelState.IsValid)
            {
                db.Mileages.Add(mileage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ApplicationID = new SelectList(db.Applications, "ID", "Rationale", mileage.ApplicationID);
            ViewBag.EmployeeID = new SelectList(db.Employees, "ID", "FirstName", mileage.EmployeeID);
            ViewBag.StatusID = new SelectList(db.Statuses, "ID", "Description", mileage.StatusID);
            return View(mileage);
        }

        // GET: Mileages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mileage mileage = db.Mileages.Find(id);
            if (mileage == null)
            {
                return HttpNotFound();
            }
            ViewBag.ApplicationID = new SelectList(db.Applications, "ID", "Rationale", mileage.ApplicationID);
            ViewBag.EmployeeID = new SelectList(db.Employees, "ID", "FirstName", mileage.EmployeeID);
            ViewBag.StatusID = new SelectList(db.Statuses, "ID", "Description", mileage.StatusID);
            return View(mileage);
        }

        // POST: Mileages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,TravelDate,StartAddress,EndAddress,Kilometres,Feedback,StatusID,EmployeeID,ApplicationID")] Mileage mileage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mileage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ApplicationID = new SelectList(db.Applications, "ID", "Rationale", mileage.ApplicationID);
            ViewBag.EmployeeID = new SelectList(db.Employees, "ID", "FirstName", mileage.EmployeeID);
            ViewBag.StatusID = new SelectList(db.Statuses, "ID", "Description", mileage.StatusID);
            return View(mileage);
        }

        // GET: Mileages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mileage mileage = db.Mileages.Find(id);
            if (mileage == null)
            {
                return HttpNotFound();
            }
            return View(mileage);
        }

        // POST: Mileages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Mileage mileage = db.Mileages.Find(id);
            db.Mileages.Remove(mileage);
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
