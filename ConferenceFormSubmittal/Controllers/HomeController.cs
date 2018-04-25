using ConferenceFormSubmittal.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ConferenceFormSubmittal.Controllers
{
    public class HomeController : Controller
    {
        private CFSEntities db = new CFSEntities();

        public ActionResult Index()
        {
            // submissions would only be shown for admins
            ViewBag.NewApplications = db.Applications.Where(a => a.Status.Description == "Submitted").Count();

            // the email address criteria would need to be changed to the current user's.
            // for now it's Fred
            // this is simply a demonstration of what notifications might look like, if time was taken to properly implement the functionality
            ViewBag.Denied = db.Applications.Where(a => a.Employee.Email == "fflintstone@outlook.com" && a.Status.Description == "Denied").Count();
            ViewBag.Approved = db.Applications.Where(a => a.Employee.Email == "fflintstone@outlook.com" && a.Status.Description == "Approved").Count();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}