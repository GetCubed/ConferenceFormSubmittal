﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
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
                        Expense e = expenses[0];

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

                        db.SaveChanges();
                        dbContextTransaction.Commit();

                        result += e.ID.ToString();
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
