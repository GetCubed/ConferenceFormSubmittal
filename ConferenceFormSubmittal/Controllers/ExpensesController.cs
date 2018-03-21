﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ConferenceFormSubmittal.DAL;
using ConferenceFormSubmittal.Models;

namespace ConferenceFormSubmittal.Controllers
{
    public class ExpensesController : Controller
    {
        private CFSEntities db = new CFSEntities();

        // GET: Expenses
        public ActionResult Index()
        {
            var expenses = db.Expenses.Include(e => e.Application).Include(e => e.ExpenseType);
            return View(expenses.ToList());
        }

        // GET: Expenses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Expense expense = db.Expenses.Find(id);
            if (expense == null)
            {
                return HttpNotFound();
            }
            return View(expense);
        }

        // GET: Expenses/Create
        public ActionResult Create()
        {
            ViewBag.ApplicationID = new SelectList(db.Applications, "ID", "Rationale");
            ViewBag.ExpenseTypeID = new SelectList(db.ExpenseTypes, "ID", "Description");
            ViewBag.StatusID = new SelectList(db.Statuses, "ID", "Description");
            return View();
        }

        // POST: Expenses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Rationale,EstimatedCost,ActualCost,ExpenseTypeID,ApplicationID")] Expense expense)
        {
            if (ModelState.IsValid)
            {
                db.Expenses.Add(expense);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ApplicationID = new SelectList(db.Applications, "ID", "Rationale", expense.ApplicationID);
            ViewBag.ExpenseTypeID = new SelectList(db.ExpenseTypes, "ID", "Description", expense.ExpenseTypeID);
            return View(expense);
        }

        // GET: Expenses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Expense expense = db.Expenses.Find(id);
            if (expense == null)
            {
                return HttpNotFound();
            }
            ViewBag.ExpenseTypeID = new SelectList(db.ExpenseTypes, "ID", "Description", expense.ExpenseTypeID);
            return View(expense);
        }

        // inserts or updates Expenses from the Application Edit view
        public JsonResult AddOrUpdateExpenses(List<Expense> expenses)
        {
            if (expenses == null)
            {
                expenses = new List<Expense>();
            }

            string result = "Success:";

            if (expenses.Count > 0)
            {
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        foreach (Expense e in expenses)
                        {
                            if (db.Expenses.Any(a => a.ID == e.ID)) // if the Expense exists in the db, update it
                            {
                                var expenseToUpdate = db.Expenses.Find(e.ID);
                                expenseToUpdate.Rationale = e.Rationale;
                                expenseToUpdate.EstimatedCost = e.EstimatedCost;
                                expenseToUpdate.ActualCost = e.ActualCost;
                                expenseToUpdate.ExpenseTypeID = e.ExpenseTypeID;
                            }
                            else // insert it
                            {
                                db.Expenses.Add(e);

                                
                            }
                        }

                        db.SaveChanges();
                        dbContextTransaction.Commit();

                        // we need to pass back the newly generated ids of rows that are inserted through the ApplicationEdit view
                        if (expenses.Count == 1)
                        {
                            result += expenses[0].ID.ToString();
                        }
                    }
                    catch (Exception)
                    {
                        dbContextTransaction.Rollback();

                        result = "Failed to save changes. Refresh the page and try again. If the problem persists, please contact your database administrator.";
                    }
                }
            }
            
            return Json(result);
        }

        public JsonResult UploadFiles(int? id)
        {
            if (id == null)
            {
                return Json("bad request");
            }
            Expense expense = db.Expenses
                .Include(e => e.Files)
                .Where(e => e.ID == id).SingleOrDefault();

            for (int i = 0; i < Request.Files.Count; i++)
            {
                HttpPostedFileBase f = Request.Files[i];

                int fileLength = f.ContentLength;
                string fileName = f.FileName;
                string mimeType = f.ContentType;

                if (!(fileName == "" || fileLength == 0))//Looks like we have a file!!!
                {
                    Stream fileStream = f.InputStream;
                    byte[] fileData = new byte[fileLength];
                    fileStream.Read(fileData, 0, fileLength);

                    Documentation newFile = new Documentation
                    {
                        FileContent = new FileContent
                        {
                            Content = fileData,
                            MimeType = mimeType
                        },
                        fileName = fileName
                    };
                    expense.Files.Add(newFile);
                }
            }

            db.SaveChanges();

            //string result = "";
            //foreach (Documentation d in expense.Files)
            //{
            //    result += d.ID + "," + d.fileName + ";";
            //}

            //return Json(result.TrimEnd(';'));

            return Json(Request.Files.Count + " files uploaded");
        }

        // POST: Expenses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(Expense expense)
        {
            if (ModelState.IsValid)
            {
                db.Entry(expense).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(expense);
        }

        // GET: Expenses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Expense expense = db.Expenses.Find(id);
            if (expense == null)
            {
                return HttpNotFound();
            }
            return View(expense);
        }

        // POST: Expenses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Expense expense = db.Expenses.Find(id);
            db.Expenses.Remove(expense);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public FileContentResult Download(int id)
        {
            var theFile = db.Files.Include(f => f.FileContent).Where(f => f.ID == id).SingleOrDefault();
            return File(theFile.FileContent.Content, theFile.FileContent.MimeType, theFile.fileName);
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
