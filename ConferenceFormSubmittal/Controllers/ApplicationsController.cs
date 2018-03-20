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
        public ActionResult Create(int? ConferenceID)
        {
            if (!ConferenceID.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.Conference = db.Conferences.Find(ConferenceID);

            PopulateDropDownLists();
            return View();
        }

        private static List<Expense> expenseBatch = new List<Expense>();
        public JsonResult AddExpenses(List<Expense> expenses)
        {
            if (expenses == null)
            {
                expenses = new List<Expense>();
            }

            foreach (Expense expense in expenses)
            {
                expenseBatch.Add(expense);
            }

            return Json(expenseBatch.Count);
        }

        // POST: Applications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePost([Bind(Include = "ID,Rationale,ReplStaffReq,BudgetCode,AttendStartDate,AttendEndDate,DepartureDate,ReturnDate,PaymentTypeID,EmployeeID,ConferenceID,StatusID")] Application application)
        {
            if (ModelState.IsValid)
            {
                // add the expenses to the application
                foreach (Expense expense in expenseBatch)
                {
                    application.Expenses.Add(expense);
                }

                application.DateSubmitted = DateTime.Today;
                
                // insert the application
                db.Applications.Add(application);
                db.SaveChanges();

                // clear the expense batch
                expenseBatch.Clear();

                return RedirectToAction("Index");
            }

            PopulateDropDownLists(application);
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
            PopulateDropDownLists(application);
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
            PopulateDropDownLists(application);
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

            PopulateDropDownLists(application, application.Expenses.ToList());
            return View(application);
        }

        // POST: Applications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost([Bind(Include = "ID,Rationale,ReplStaffReq,BudgetCode,DateSubmitted,AttendStartDate,AttendEndDate,DepartureDate,ReturnDate,PaymentTypeID,ConferenceID")] Application application)
        {
            if (ModelState.IsValid)
            {
                db.Entry(application).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            PopulateDropDownLists(application);
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

        private void PopulateDropDownLists(Application application = null, List<Expense> expenses = null)
        {
            var pQuery = from p in db.PaymentTypes
                         orderby p.Description
                         select p;
            ViewBag.PaymentTypeID = new SelectList(pQuery, "ID", "Description", application?.PaymentTypeID);

            var eQuery = from e in db.ExpenseTypes
                            orderby e.Description
                            select e;
            ViewBag.ExpenseTypeID = new SelectList(eQuery, "ID", "Description");
            
            // how to have the current ExpenseType selected in each Expense's ddl?
            if (expenses != null)
            {
                // each Expense needs its own SelectList in the ViewBag
                foreach (Expense e in expenses)
                {
                    ViewData.Add("ExpenseTypes" + e.ID.ToString(), new SelectList(eQuery, "ID", "Description", e.ExpenseTypeID));
                }
            }
            
            var sQuery = from s in db.Statuses
                         orderby s.Description
                         select s;
            ViewBag.StatusID = new SelectList(sQuery, "ID", "Description");
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
