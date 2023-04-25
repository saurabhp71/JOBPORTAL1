using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using JobPortalLibrary.Controller;


namespace JobPortal.Controllers
{
    public class AccountController : Controller
    {
        //--------------------------------------Saurabh Start--------------------------------//
        // GET: Account
        string SeekerCode;
        string IMG;
        string EmployeerCode;
        
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<ActionResult> Register()
        {
            Category();
            AccountUser user = new AccountUser();
            return await Task.Run(() => View(user));
        }
        public async Task<ActionResult> Category()
        {
            var list = new List<string>() { "I am Seeker", "I am Employeer" };
            ViewBag.list = list;
            return await Task.Run(() => View());
        }
        [HttpPost]
        public async Task<ActionResult> Register(AccountUser obj)
        {
            Category();
            if (ModelState.IsValid)
            {
                Code();
                obj.DateOfRegistration = DateTime.Now;
                if (obj.Category == "I am Seeker")
                {
                    obj.Code = SeekerCode;
                    obj.ResumePDF = "null";
                   
                    BALAccount objsave = new BALAccount();
                    objsave.SeekerRegister(obj);
                    objsave.EducationDetails(obj);
                    objsave.EmploymentDetails(obj);
                    objsave.ProjectDetails(obj);
                    objsave.JobAlertSave(obj);
                    Session["SeekerCode"] = obj.Code;
                    Session["SeekerName"] = obj.SeekerName;
                    Session["ProfileIMG"] = IMG;
                    return RedirectToAction("SeekerDetails","JobSeeker");
                }
                if (obj.Category == "I am Employeer")
                {
                    obj.Code = EmployeerCode;

                    BALAccount objsave = new BALAccount();
                    objsave.EmployeerRegister(obj);
                    Session["Employercode"] = obj.Code;
                    Session["EmployerName"] = obj.SeekerName;
                    return RedirectToAction("EmployeerIndex","Employer");
                }
               
            }
            return await Task.Run(() => View("Register"));
        }
        public async Task<ActionResult> Code()
        {
            AccountUser obj = new AccountUser();
            BALAccount obj1 = new BALAccount();
            SqlDataReader dr;
            dr = obj1.SeekerCode();
            while (dr.Read())
            {
                int seekercode = Convert.ToInt32(dr["SeekerId"].ToString());
                IMG = dr["ProfileIMG"].ToString();
                seekercode = seekercode + 1;
                string ID = "JSC00";
                SeekerCode = ID + seekercode;

            }
            dr.Close();
            BALAccount obj2 = new BALAccount();
            SqlDataReader dt;
            dt = obj2.EmployeerCode();
            while (dt.Read())
            {
                int employeercode = Convert.ToInt32(dt["EmployeerId"].ToString());
                employeercode = employeercode + 1;
                string ID = "EMP00";
                EmployeerCode = ID + employeercode;

            }
            dt.Close();

            return await Task.Run(() => View(obj));
        }
        //--------------------------------------Saurabh End--------------------------------//



        //------------------------------------ Muskan Start----------------------------------//
        //-------------------------loginSeeker&employer--------------------------------------------------------------------------------------------------------------------------
        [HttpGet]
        public async Task<ActionResult> Login()
        {
            return await Task.Run(() => View());

        }
        [HttpPost]
        public async Task<ActionResult> Login(AccountUser log)
        {

            string seekercode = null;
            string seekername = null;
            string employername = null;
          //  string ProfileIMG = null;
            BALAccount obj = new BALAccount();
            SqlDataReader dr;
            dr = obj.Login(log);
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    seekercode = dr["SeekerCode"].ToString();
                    seekername = dr["SeekerName"].ToString();
                    log.ProfileIMG = dr["ProfileIMG"].ToString();
                    TempData["MessageLogin"] = "Login Successfully....!!!";
                    Session["SeekerCode"] = seekercode;
                    Session["SeekerName"] = seekername;
                    Session["ProfileIMG"] = log.ProfileIMG;
                    return RedirectToAction("SeekerIndex", "JobSeeker");
                }
            }

            else
            {
                BALAccount objE = new BALAccount();
                SqlDataReader drE;
                drE = obj.LoginEmp(log);

                if (drE.HasRows)
                {
                    while (drE.Read())
                    {

                        EmployeerCode = drE["Employercode"].ToString();
                        employername = drE["EmployerName"].ToString();
                        TempData["MessageLogin"] = "Login Successfully...!!";
                        Session["Employercode"] = EmployeerCode;
                        Session["EmployerName"] = employername;
                        return RedirectToAction("EmployeerIndex","Employer");
                    }
                    drE.Close();
                }
                TempData["Message"] = "Please Enter Correct EmailId And Password....!!!";
            }
            return await Task.Run(() => View("Login"));


        }
        //------------------------------------ Muskan End----------------------------------//

    }
}