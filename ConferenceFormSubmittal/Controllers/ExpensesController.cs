using System;
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

        public JsonResult GetExpensesByApplication(int? applicationID)
        {
            if (applicationID == null)
            {
                return Json(new List<Expense>(), JsonRequestBehavior.AllowGet);
            }

            var expenses = db.Expenses
                .Where(e => e.ApplicationID == applicationID)
                .Select(e => new
                {
                    e.ID,
                    e.ExpenseTypeID,
                    type = e.ExpenseType.Description,
                    e.EstimatedCost,
                    e.ActualCost,
                    e.Rationale,
                    e.Files
                });

            return Json(expenses, JsonRequestBehavior.AllowGet);
        }

        // inserts or updates Expenses from the Application Edit view
        public JsonResult AddOrUpdateExpense(Expense expense)
        {
            if (expense == null)
            {
                return Json("Failure: null Expense");
            }

            string result = "success:";

            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    if (db.Expenses.Any(a => a.ID == expense.ID)) // if the Expense exists in the db, update it
                    {
                        var expenseToUpdate = db.Expenses.Find(expense.ID);
                        expenseToUpdate.Rationale = expense.Rationale;
                        expenseToUpdate.EstimatedCost = expense.EstimatedCost;
                        expenseToUpdate.ActualCost = expense.ActualCost;
                        expenseToUpdate.ExpenseTypeID = expense.ExpenseTypeID;
                    }
                    else // new Expense -- insert it
                    {
                        db.Expenses.Add(expense);
                    }

                    db.SaveChanges();
                    dbContextTransaction.Commit();

                    // add the ID of the inserted or updated record to the response
                    result += expense.ID.ToString();
                }
                catch (Exception)
                {
                    dbContextTransaction.Rollback();
                    result = "Failed to save changes. Refresh the page and try again. If the problem persists, please contact your database administrator.";
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
