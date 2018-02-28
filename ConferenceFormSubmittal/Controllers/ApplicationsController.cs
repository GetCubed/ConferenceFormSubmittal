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
    public class ApplicationsController : Controller
    {
        private CFSEntities db = new CFSEntities();

        // GET: Applications
        public ActionResult Index()
        {
            var applications = db.Applications.Include(a => a.Conference).Include(a => a.Employee).Include(a => a.Status);
            return View(applications.ToList());
        }

        // GET: Applications
        public ActionResult IndexAdmin()
        {
            var applications = db.Applications.Include(a => a.Conference).Include(a => a.Employee).Include(a => a.Status);
            return View(applications.ToList());
        }

        // GET: Applications/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Application application = db.Applications.Find(id);
            if (application == null)
            {
                return HttpNotFound();
            }
            return View(application);
        }

        // GET: Applications/Create
        public ActionResult Create()
        {
            ViewBag.ConferenceID = new SelectList(db.Conferences, "ID", "Name");
            ViewBag.EmployeeID = new SelectList(db.Employees, "ID", "FirstName");
            ViewBag.StatusID = new SelectList(db.Statuses, "ID", "Description");
            return View();
        }

        // POST: Applications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Rationale,ReplStaffReq,BudgetCode,DateSubmitted,Feedback,EmployeeID,ConferenceID,StatusID")] Application application)
        {
            if (ModelState.IsValid)
            {
                db.Applications.Add(application);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ConferenceID = new SelectList(db.Conferences, "ID", "Name", application.ConferenceID);
            ViewBag.EmployeeID = new SelectList(db.Employees, "ID", "FirstName", application.EmployeeID);
            ViewBag.StatusID = new SelectList(db.Statuses, "ID", "Description", application.StatusID);
            return View(application);
        }

        // GET: Applications/Edit/5
        public ActionResult Evaluate(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Application application = db.Applications.Find(id);
            if (application == null)
            {
                return HttpNotFound();
            }
            ViewBag.ConferenceID = new SelectList(db.Conferences, "ID", "Name", application.ConferenceID);
            ViewBag.EmployeeID = new SelectList(db.Employees, "ID", "FirstName", application.EmployeeID);
            ViewBag.StatusID = new SelectList(db.Statuses, "ID", "Description", application.StatusID);
            return View(application);
        }


        // POST: Applications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Evaluate([Bind(Include = "ID,Rationale,ReplStaffReq,BudgetCode,DateSubmitted,Feedback,EmployeeID,ConferenceID,StatusID")] Application application)
        {
            if (ModelState.IsValid)
            {
                db.Entry(application).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ConferenceID = new SelectList(db.Conferences, "ID", "Name", application.ConferenceID);
            ViewBag.EmployeeID = new SelectList(db.Employees, "ID", "FirstName", application.EmployeeID);
            ViewBag.StatusID = new SelectList(db.Statuses, "ID", "Description", application.StatusID);
            return View(application);
        }



        // GET: Applications/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Application application = db.Applications.Find(id);
            if (application == null)
            {
                return HttpNotFound();
            }
            ViewBag.ConferenceID = new SelectList(db.Conferences, "ID", "Name", application.ConferenceID);
            ViewBag.EmployeeID = new SelectList(db.Employees, "ID", "FirstName", application.EmployeeID);
            ViewBag.StatusID = new SelectList(db.Statuses, "ID", "Description", application.StatusID);
            return View(application);
        }

        // POST: Applications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Rationale,ReplStaffReq,BudgetCode,DateSubmitted,Feedback,EmployeeID,ConferenceID,StatusID")] Application application)
        {
            if (ModelState.IsValid)
            {
                db.Entry(application).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ConferenceID = new SelectList(db.Conferences, "ID", "Name", application.ConferenceID);
            ViewBag.EmployeeID = new SelectList(db.Employees, "ID", "FirstName", application.EmployeeID);
            ViewBag.StatusID = new SelectList(db.Statuses, "ID", "Description", application.StatusID);
            return View(application);
        }

        // GET: Applications/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Application application = db.Applications.Find(id);
            if (application == null)
            {
                return HttpNotFound();
            }
            return View(application);
        }

        // POST: Applications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Application application = db.Applications.Find(id);
            db.Applications.Remove(application);
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
