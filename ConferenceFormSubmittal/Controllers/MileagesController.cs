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
    public class MileagesController : Controller
    {
        private CFSEntities db = new CFSEntities();

        // GET: Mileages
        public ActionResult Index(string sortDirection, string sortField, string actionButton,
            int? employeeID, string employeeFirst, string employeeLast, string startAddress, 
            string endAddress, DateTime? startDate, DateTime? endDate, int? statusID, int? page)
        {
            PopulateDropDownLists();
            var mileages = db.Mileages.Include(m => m.Application).Include(m => m.Employee).Include(m => m.Status);
            ViewBag.Filtering = "";

            //this is ugly, needs alternative or to be removed
            ViewBag.StartAddress = new SelectList(db.Mileages, "StartAddress", "StartAddress");
            ViewBag.EndAddress = new SelectList(db.Mileages, "EndAddress", "EndAddress");
            
            if (employeeID.HasValue)
            {
                mileages = mileages.Where(p => p.EmployeeID == employeeID);
                ViewBag.Filtering = " in";//Flag filtering
                ViewBag.LastEmployeeID = employeeID;
            }
            if (!String.IsNullOrEmpty(employeeFirst))
            {
                mileages = mileages.Where(p => p.Employee.FirstName.ToUpper().Contains(employeeFirst.ToUpper()));
                ViewBag.Filtering = " in";//Flag filtering
                ViewBag.LastEmployeeFirst = employeeFirst;
            }
            if (!String.IsNullOrEmpty(employeeLast))
            {
                mileages = mileages.Where(p => p.Employee.LastName.ToUpper().Contains(employeeLast.ToUpper()));
                ViewBag.Filtering = " in";//Flag filtering
                ViewBag.LastEmployeeLast = employeeLast;
            }
            if (!String.IsNullOrEmpty(startAddress))
            {
                mileages = mileages.Where(p => p.StartAddress == startAddress);
                ViewBag.Filtering = " in";//Flag filtering
                ViewBag.LastStartAddress = startAddress;
            }
            if (!String.IsNullOrEmpty(endAddress))
            {
                mileages = mileages.Where(p => p.EndAddress == endAddress);
                ViewBag.Filtering = " in";//Flag filtering
                ViewBag.LastEndAddress = endAddress;
            }
            if (startDate.HasValue && endDate.HasValue)
            {
                mileages = mileages.Where(p => p.TravelDate > startDate && p.TravelDate < endDate);
                ViewBag.Filtering = " in";//Flag filtering
                ViewBag.LastStartDate = startDate;
                ViewBag.LastEndDate = endDate;
            }
            else if (startDate.HasValue && !endDate.HasValue)
            {
                mileages = mileages.Where(p => p.TravelDate > startDate);
                ViewBag.Filtering = " in";//Flag filtering
                ViewBag.LastStartDate = startDate;
            }
            else if (!startDate.HasValue && endDate.HasValue)
            {
                mileages = mileages.Where(p => p.TravelDate < endDate);
                ViewBag.Filtering = " in";//Flag filtering
                ViewBag.LastEndDate = endDate;
            }
            if (statusID.HasValue)
            {
                mileages = mileages.Where(p => p.StatusID == statusID);
                ViewBag.Filtering = " in";//Flag filtering
                ViewBag.LastStatusID = statusID;
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
            if (sortField == "Start Address")//Sorting by Start Location
            {
                if (String.IsNullOrEmpty(sortDirection))
                {
                    mileages = mileages
                        .OrderBy(c => c.StartAddress);
                }
                else
                {
                    mileages = mileages.OrderByDescending(c => c.StartAddress);
                }
            }
            else if (sortField == "End Address")//Sorting by End Location
            {
                if (String.IsNullOrEmpty(sortDirection))
                {
                    mileages = mileages
                        .OrderBy(c => c.EndAddress);
                }
                else
                {
                    mileages = mileages.OrderByDescending(c => c.EndAddress);
                }
            }
            else if(sortField == "Kilometres")//Sorting by Kilometres
            {
                if (String.IsNullOrEmpty(sortDirection))
                {
                    mileages = mileages
                        .OrderBy(c => c.Kilometres);
                }
                else
                {
                    mileages = mileages.OrderByDescending(c => c.Kilometres);
                }
            }
            else if (sortField == "Description")//Sorting by Status
            {
                if (String.IsNullOrEmpty(sortDirection))
                {
                    mileages = mileages
                        .OrderBy(c => c.StatusID);
                }
                else
                {
                    mileages = mileages.OrderByDescending(c => c.StatusID);
                }
            }
            else //By default sort by Travel Date
            {
                if (String.IsNullOrEmpty(sortDirection))
                {
                    mileages = mileages
                        .OrderBy(p => p.TravelDate);
                }
                else
                {
                    mileages = mileages
                        .OrderByDescending(p => p.TravelDate);
                }
            }

            ViewBag.sortField = sortField;
            ViewBag.sortDirection = sortDirection;

            int pageSize = 5;//Temp value, good value is like 10
            int pageNumber = (page ?? 1);

            return View(mileages.ToPagedList(pageNumber, pageSize));
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
            PopulateDropDownLists();
            //ViewBag.ConferenceID = new SelectList(db.Conferences, "ID", "Name");
            //so ConferenceID would need to display Conference Name       "ConferenceID"
            ViewBag.ApplicationID = new SelectList(db.Applications, "ID", "Rationale");
            ViewBag.EmployeeID = new SelectList(db.Employees, "ID", "FirstName");
            ViewBag.StatusID = new SelectList(db.Statuses, "ID", "Description");
            return View();
        }

        public JsonResult AddMileages(List<Mileage> mileages)
        {
            using (CFSEntities entities = new CFSEntities())
            {
                //Check for NULL.
                if (mileages == null)
                {
                    mileages = new List<Mileage>();
                }

                //ViewBag.ApplicationID = new SelectList(db.Applications, "ID", "Rationale");
                //ViewBag.EmployeeID = new SelectList(db.Employees, "ID", "FirstName");
                //ViewBag.StatusID = new SelectList(db.Statuses, "ID", "Description");

                //Loop and insert records.
                foreach (Mileage mileage in mileages)
                {
                    entities.Mileages.Add(mileage);
                }
                int insertedRecords = entities.SaveChanges();
                return Json(insertedRecords);
            }
        }

        // POST: Mileages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Create")]
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
            PopulateDropDownLists();
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

        private void PopulateDropDownLists(Mileage mileage = null)
        {
            //var aQuery = from p in db.Applications
            //             orderby p.ConferenceID
            //             select p;
            //ViewBag.ConferenceName = new SelectList(aQuery, "ID", "Name");

            var lQuery = from l in db.Sites
                         orderby l.Name
                         select l;
            ViewBag.Sites = new SelectList(lQuery, "Address", "Name");
            
            var sQuery = from p in db.Statuses
                         orderby p.Description
                         select p;
            ViewBag.StatusID = new SelectList(sQuery, "ID", "Description", mileage?.StatusID);
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
