using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using JobPortalLibrary.Admin;
using JobPortalLibrary.JobSeeker;

namespace JobPortal.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
        
            return View();
        }
        public ActionResult Create()
        {

            return View();
        }
        public ActionResult AdminIndex()
        {

            return View();
        }
    }
}