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
    public class ConferencesController : Controller
    {
        private CFSEntities db = new CFSEntities();

        // GET: Conferences
        public ActionResult Index(string sortDirection, string sortField, string actionButton, 
            int? conferenceID, string conferenceName, string locationName, DateTime? startDate, DateTime? endDate, int? page)
        {
            IQueryable<Conference> conferences = db.Conferences;
            ViewBag.Filtering = "";

            ViewBag.ConferenceID = new SelectList(db.Conferences, "ID", "Name");
            ViewBag.Location = new SelectList(db.Conferences, "Location", "Location");

            if (!String.IsNullOrEmpty(conferenceName))
            {
                conferences = conferences.Where(p => p.Name.ToUpper().Contains(conferenceName.ToUpper()));
                ViewBag.Filtering = " in";//Flag filtering
                ViewBag.LastConferenceID = conferenceName;
            }

            if (!String.IsNullOrEmpty(locationName))
            {
                conferences = conferences.Where(p => p.Location.ToUpper().Contains(locationName.ToUpper()));
                ViewBag.Filtering = " in";//Flag filtering
                ViewBag.LastLocation = locationName;
            }

            if (startDate.HasValue && endDate.HasValue)
            {
                conferences = conferences.Where(p => p.StartDate > startDate && p.EndDate < endDate);
                ViewBag.Filtering = " in";//Flag filtering
                ViewBag.LastStartDate = startDate;
                ViewBag.LastEndDate = endDate;
            }
            else if (startDate.HasValue && !endDate.HasValue)
            {
                conferences = conferences.Where(p => p.StartDate > startDate);
                ViewBag.Filtering = " in";//Flag filtering
                ViewBag.LastStartDate = startDate;
            }
            else if (!startDate.HasValue && endDate.HasValue)
            {
                conferences = conferences.Where(p => p.EndDate < endDate);
                ViewBag.Filtering = " in";//Flag filtering
                ViewBag.LastEndDate = endDate;
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
            if (sortField == "Location")//Sorting by Location
            {
                if (String.IsNullOrEmpty(sortDirection))
                {
                    conferences = conferences
                        .OrderBy(c => c.Location);
                }
                else
                {
                    conferences = conferences.OrderByDescending(c => c.Location);
                }
            }
            else if (sortField == "Registration Cost")//Sorting by Registration Cost
            {
                if (String.IsNullOrEmpty(sortDirection))
                {
                    conferences = conferences
                        .OrderBy(c => c.RegistrationCost);
                }
                else
                {
                    conferences = conferences.OrderByDescending(c => c.RegistrationCost);
                }
            }
            else if (sortField == "Start Date")//Sorting by Start Date
            {
                if (String.IsNullOrEmpty(sortDirection))
                {
                    conferences = conferences
                        .OrderBy(c => c.StartDate);
                }
                else
                {
                    conferences = conferences.OrderByDescending(c => c.StartDate);
                }
            }
            else if (sortField == "End Date")//Sorting by End Date
            {
                if (String.IsNullOrEmpty(sortDirection))
                {
                    conferences = conferences
                        .OrderBy(c => c.EndDate);
                }
                else
                {
                    conferences = conferences.OrderByDescending(c => c.EndDate);
                }
            }
            else //By default sort by Conference
            {
                if (String.IsNullOrEmpty(sortDirection))
                {
                    conferences = conferences
                        .OrderBy(c => c.Name);
                }
                else
                {
                    conferences = conferences.OrderByDescending(c => c.Name);
                }
            }
            
            ViewBag.sortField = sortField;
            ViewBag.sortDirection = sortDirection;

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View(conferences.ToPagedList(pageNumber, pageSize));
        }

        // GET: Conferences/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Conference conference = db.Conferences.Find(id);
            if (conference == null)
            {
                return HttpNotFound();
            }
            return View(conference);
        }

        // GET: Conferences/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Conferences/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Byte[] RowVersion, [Bind(Include = "ID,Name,Location,RegistrationCost,StartDate,EndDate")] Conference conference)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(conference).State = EntityState.Added;
                    db.Entry(conference).OriginalValues["RowVersion"] = RowVersion;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

            }
            catch (DbUpdateConcurrencyException ex)
            {
                ModelState.AddModelError(string.Empty, "The record you attempted to edit " + "was modified by another user. Please go back and refresh");
            }

            return View(conference);
        }

        // GET: Conferences/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Conference conference = db.Conferences.Find(id);
            if (conference == null)
            {
                return HttpNotFound();
            }
            return View(conference);
        }

        // POST: Conferences/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Byte[] RowVersion, [Bind(Include = "ID,Name,Location,RegistrationCost,StartDate,EndDate")] Conference conference)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(conference).State = EntityState.Modified;
                    db.Entry(conference).OriginalValues["RowVersion"] = RowVersion;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

            }
            catch (DbUpdateConcurrencyException ex)
            {
                ModelState.AddModelError(string.Empty, "The record you attempted to edit " + "was modified by another user. Please go back and refresh");
            }

            return View(conference);
        }

        // GET: Conferences/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Conference conference = db.Conferences.Find(id);
            if (conference == null)
            {
                return HttpNotFound();
            }
            return View(conference);
        }

        // POST: Conferences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Conference conference = db.Conferences.Find(id);
            db.Conferences.Remove(conference);
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
