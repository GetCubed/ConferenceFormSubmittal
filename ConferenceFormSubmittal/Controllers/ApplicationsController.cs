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
using System.Data.Entity.Infrastructure;
using PagedList;

namespace ConferenceFormSubmittal.Controllers
{
    public class ApplicationsController : Controller
    {
        private CFSEntities db = new CFSEntities();

        // GET: Applications
        public ActionResult Index(string sortDirection, string sortField, string actionButton,
            DateTime? startDate, DateTime? endDate, string conferenceName, int? statusID, int? page)
        {
            PopulateDropDownLists();
            var applications = db.Applications.Include(a => a.Conference).Include(a => a.Employee).Include(a => a.Status);
            ViewBag.Filtering = "";
            
            if (startDate.HasValue && endDate.HasValue)
            {
                applications = applications.Where(p => p.DateSubmitted > startDate && p.DateSubmitted < endDate);
                ViewBag.Filtering = " in";//Flag filtering
                ViewBag.LastStartDate = startDate;
                ViewBag.LastEndDate = endDate;
            }
            else if (startDate.HasValue && !endDate.HasValue)
            {
                applications = applications.Where(p => p.DateSubmitted > startDate);
                ViewBag.Filtering = " in";//Flag filtering
                ViewBag.LastStartDate = startDate;
            }
            else if (!startDate.HasValue && endDate.HasValue)
            {
                applications = applications.Where(p => p.DateSubmitted < endDate);
                ViewBag.Filtering = " in";//Flag filtering
                ViewBag.LastEndDate = endDate;
            }
            if (!String.IsNullOrEmpty(conferenceName))
            {
                applications = applications.Where(p => p.Conference.Name.ToUpper().Contains(conferenceName.ToUpper()));
                ViewBag.Filtering = " in";//Flag filtering
                ViewBag.LastConferenceName = conferenceName;
            }
            if (statusID.HasValue)
            {
                applications = applications.Where(p => p.StatusID == statusID);
                ViewBag.Filtering = " in";//Flag filtering
                ViewBag.LastStatusID = statusID;
            }
            if (ViewBag.Filtering == "")
            {
                string url = Request.Url.AbsoluteUri;
                if (url == "http://localhost:5824/Applications")
                {
                    statusID = 1;
                    applications = applications.Where(p => p.StatusID == statusID);
                }
            }

            if (!String.IsNullOrEmpty(actionButton))
            {
                //Reset paging if ANY button was pushed
                page = 1;

                if (actionButton != "Filter")//Change of sort is requested
                {
                    if (actionButton == sortField) //Reverse order on same field
                    {
                        sortDirection = String.IsNullOrEmpty(sortDirection) ? "desc" : "";
                    }
                    sortField = actionButton;//Sort by the button clicked
                }
            }
            if (sortField == "Status")//Sorting by Status
            {
                if (String.IsNullOrEmpty(sortDirection))
                {
                    applications = applications
                        .OrderBy(c => c.StatusID);
                }
                else
                {
                    applications = applications.OrderByDescending(c => c.StatusID);
                }
            }
            else if (sortField == "Conference Name")//Sorting by Conference Name
            {
                if (String.IsNullOrEmpty(sortDirection))
                {
                    applications = applications
                        .OrderBy(c => c.Conference.Name);
                }
                else
                {
                    applications = applications.OrderByDescending(c => c.Conference.Name);
                }
            }
            else //By default sort by Date Submitted
            {

                if (String.IsNullOrEmpty(sortDirection))
                {
                    applications = applications
                        .OrderBy(p => p.DateSubmitted);
                }
                else
                {
                    applications = applications
                        .OrderByDescending(p => p.DateSubmitted);
                }
            }

            ViewBag.sortField = sortField;
            ViewBag.sortDirection = sortDirection;

            int pageSize = 5;//Temp value, good value is like 10
            int pageNumber = (page ?? 1);

            return View(applications.ToPagedList(pageNumber, pageSize));
        }

        // GET: Applications
        public ActionResult IndexAdmin(string sortDirection, string sortField, string actionButton,
            int? employeeID, string employeeFirst, string employeeLast, DateTime? startDate,
            DateTime? endDate, string conferenceName, int? statusID, int? page)
        {
            PopulateDropDownLists();
            var applications = db.Applications.Include(a => a.Conference).Include(a => a.Employee).Include(a => a.Status);
            ViewBag.Filtering = "";

            if (employeeID.HasValue)
            {
                applications = applications.Where(p => p.EmployeeID == employeeID);
                ViewBag.Filtering = " in";//Flag filtering
                ViewBag.LastEmployeeID = employeeID;
            }
            if (!String.IsNullOrEmpty(employeeFirst))
            {
                applications = applications.Where(p => p.Employee.FirstName.ToUpper().Contains(employeeFirst.ToUpper()));
                ViewBag.Filtering = " in";//Flag filtering
                ViewBag.LastEmployeeName = employeeFirst;
            }
            if (!String.IsNullOrEmpty(employeeLast))
            {
                applications = applications.Where(p => p.Employee.LastName.ToUpper().Contains(employeeLast.ToUpper()));
                ViewBag.Filtering = " in";//Flag filtering
                ViewBag.LastEmployeeName = employeeLast;
            }
            if (startDate.HasValue && endDate.HasValue)
            {
                applications = applications.Where(p => p.DateSubmitted > startDate && p.DateSubmitted < endDate);
                ViewBag.Filtering = " in";//Flag filtering
                ViewBag.LastStartDate = startDate;
                ViewBag.LastEndDate = endDate;
            }
            else if (startDate.HasValue && !endDate.HasValue)
            {
                applications = applications.Where(p => p.DateSubmitted > startDate);
                ViewBag.Filtering = " in";//Flag filtering
                ViewBag.LastStartDate = startDate;
            }
            else if (!startDate.HasValue && endDate.HasValue)
            {
                applications = applications.Where(p => p.DateSubmitted < endDate);
                ViewBag.Filtering = " in";//Flag filtering
                ViewBag.LastEndDate = endDate;
            }
            if (!String.IsNullOrEmpty(conferenceName))
            {
                applications = applications.Where(p => p.Conference.Name.ToUpper().Contains(conferenceName.ToUpper()));
                ViewBag.Filtering = " in";//Flag filtering
                ViewBag.LastConferenceName = conferenceName;
            }
            if (statusID.HasValue)
            {
                applications = applications.Where(p => p.StatusID == statusID);
                ViewBag.Filtering = " in";//Flag filtering
                ViewBag.LastStatusID = statusID;
            }
            if (ViewBag.Filtering == "")
            {
                string url = Request.Url.AbsoluteUri;
                if (url == "http://localhost:5824/Applications/IndexAdmin")
                {
                    statusID = 1;
                    applications = applications.Where(p => p.StatusID == statusID);
                }
            }

            if (!String.IsNullOrEmpty(actionButton))
            {
                //Reset paging if ANY button was pushed
                page = 1;

                if (actionButton != "Filter")//Change of sort is requested
                {
                    if (actionButton == sortField) //Reverse order on same field
                    {
                        sortDirection = String.IsNullOrEmpty(sortDirection) ? "desc" : "";
                    }
                    sortField = actionButton;//Sort by the button clicked
                }
            }
            if (sortField == "Employee")//Sorting by Employee Name
            {
                if (String.IsNullOrEmpty(sortDirection))
                {
                    applications = applications
                        .OrderBy(c => c.Employee.LastName)
                        .ThenBy(p => p.Employee.FirstName);
                }
                else
                {
                    applications = applications
                        .OrderByDescending(c => c.Employee.LastName)
                        .ThenByDescending(c => c.Employee.FirstName);
                }
            }
            else if (sortField == "Status")//Sorting by Status
            {
                if (String.IsNullOrEmpty(sortDirection))
                {
                    applications = applications
                        .OrderBy(c => c.StatusID);
                }
                else
                {
                    applications = applications.OrderByDescending(c => c.StatusID);
                }
            }
            else if (sortField == "Conference Name")//Sorting by Conference Name
            {
                if (String.IsNullOrEmpty(sortDirection))
                {
                    applications = applications
                        .OrderBy(c => c.Conference.Name);
                }
                else
                {
                    applications = applications.OrderByDescending(c => c.Conference.Name);
                }
            }
            else //By default sort by Date Submitted
            {
                if (String.IsNullOrEmpty(sortDirection))
                {
                    applications = applications
                        .OrderBy(p => p.DateSubmitted);
                }
                else
                {
                    applications = applications
                        .OrderByDescending(p => p.DateSubmitted);
                }
            }

            ViewBag.sortField = sortField;
            ViewBag.sortDirection = sortDirection;

            int pageSize = 5;//Temp value, good value is like 10
            int pageNumber = (page ?? 1);

            return View(applications.ToPagedList(pageNumber, pageSize));
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
                return RedirectToAction("Index", "Conferences");
            }

            ViewBag.Conference = db.Conferences.Find(ConferenceID);

            PopulateDropDownLists();
            return View();
        }

        private static List<Expense> expenseBatch = new List<Expense>();
        // Add expenses to the expenseBatch List for use in Applications/Create
        public JsonResult AddExpenses(List<Expense> expenses)
        {
            expenseBatch.Clear();

            if (expenses == null)
            {
                expenses = new List<Expense>();
            }

            foreach (Expense expense in expenses)
            {
                expenseBatch.Add(expense);
            }

            return Json(expenseBatch.Count + " expense(s) added.");
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

                if (application.StatusID == 1)
                {
                    application.DateSubmitted = DateTime.Today;
                }
                
                // insert the application
                db.Applications.Add(application);
                db.SaveChanges();

                // clear the expense batch
                expenseBatch.Clear();

                // show details of the newly created application
                return RedirectToAction("Details/" + application.ID);
            }

            PopulateDropDownLists(application);
            return View(application);
        }

        // GET: Applications/Evaluate/5
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
        public JsonResult EvaluatePost(Application a, string status)
        {
            try
            {
                Application application = db.Applications.Find(a.ID);

                application.StatusID = db.Statuses.Where(s => s.Description == status).SingleOrDefault().ID;
                application.Feedback = a.Feedback;
                application.PaymentTypeID = a.PaymentTypeID;
                application.BudgetCode = a.BudgetCode;

                db.SaveChanges();
                return Json("success");
            }
            catch (Exception)
            {
                return Json("Failed to save changes to the database. Refresh the page and try again. If the problem persists, contact your database administrator.");
            }
        }
        
        // GET: Applications/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Application application = db.Applications
                .Include(a => a.Expenses)
                .Include("Expenses.Files")
                .Where(a => a.ID == id).SingleOrDefault();

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
        public ActionResult EditPost([Bind(Include = "ID,Rationale,ReplStaffReq,BudgetCode,DateSubmitted,AttendStartDate,AttendEndDate,DepartureDate,ReturnDate,PaymentTypeID,ConferenceID,StatusID")] Application application)
        {
            if (ModelState.IsValid)
            {
                // set DateSubmitted when they submit the Application
                if (application.DateSubmitted == null && application.StatusID == 1)
                {
                    application.DateSubmitted = DateTime.Today;
                }

                db.Entry(application).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details/" + application.ID);
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
