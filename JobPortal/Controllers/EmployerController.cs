using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using JobPortalLibrary.Employer;
using JobPortalLibrary.Admin;

namespace JobPortal.Controllers
{
    public class EmployerController : Controller
    {
        // GET: Employer
        public ActionResult Index()
        {
            
            return View();
        }
        public ActionResult EmployeerIndex()
        {

            return View();
        }
    }
}