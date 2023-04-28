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
        string seekerCode;
        string IMG;
        string employeerCode;
        
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
            if (ModelState.IsValid)
            {
                Category();
                BALAccount objs = new BALAccount();
                SqlDataReader drs;
                drs = objs.forgetpasswords(obj);
                BALAccount obje = new BALAccount();
                SqlDataReader dre;
                dre = obje.forgetpasswordE(obj);
                BALAccount objA = new BALAccount();
                SqlDataReader drA;
                drA = objA.Adminemail(obj);
                if (drs.HasRows || dre.HasRows || drA.HasRows)
                {
                    ViewBag.Message = "Account already created..!";
                    return await Task.Run(() => View("Register"));
                }
                else
                {
                    Code();
                    obj.DateOfRegistration = DateTime.Now;
                    if (obj.Category == "I am Seeker")
                    {
                        obj.Seekercode = seekerCode;
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
                        return RedirectToAction("SeekerDetails", "JobSeeker");
                    }
                    if (obj.Category == "I am Employeer")
                    {
                        obj.Employercode = employeerCode;

                        BALAccount objsave = new BALAccount();
                        objsave.EmployeerRegister(obj);
                        Session["Employercode"] = obj.Code;
                        Session["EmployerName"] = obj.SeekerName;
                        return RedirectToAction("EmployeerIndex", "Employer");
                    }
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
                seekerCode = ID + seekercode;

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
                employeerCode = ID + employeercode;

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
            Category();
            return await Task.Run(() => View());

        }
        [HttpPost]
        public async Task<ActionResult> Login(AccountUser log)
        {
            Category();
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

            if(dr.HasRows == false) 
            {
                BALAccount objE = new BALAccount();
                SqlDataReader drE;
                drE = obj.LoginEmp(log);

                if (drE.HasRows)
                {
                    while (drE.Read())
                    {

                        employeerCode = drE["Employercode"].ToString();
                        employername = drE["EmployerName"].ToString();
                        TempData["MessageLogin"] = "Login Successfully...!!";
                        Session["Employercode"] = employeerCode;
                        Session["EmployerName"] = employername;
                        return RedirectToAction("EmployeerIndex","Employer");
                    }
                    drE.Close();
                }
                if( drE.HasRows == false)
                {
                    BALAccount objA = new BALAccount();
                    SqlDataReader drA;
                    drA = obj.LoginAdmin(log);

                    if (drA.HasRows)
                    {
                        while (drA.Read())
                        {

                            //employeerCode = drE["Employercode"].ToString();
                            //employername = drE["EmployerName"].ToString();
                            //TempData["MessageLogin"] = "Login Successfully...!!";
                            //Session["Employercode"] = employeerCode;
                            //Session["EmployerName"] = employername;
                            return RedirectToAction("view", "Admin");
                        }
                        drE.Close();
                    }
                    else
                    {
                        TempData["Message"] = "Please Enter Correct EmailId And Password....!!!";
                        return await Task.Run(() => RedirectToAction("Login","Account"));
                    }
                }

            }
            else
            {
                TempData["Message"] = "Please Enter Correct EmailId And Password....!!!";
                return await Task.Run(() => RedirectToAction("Login", "Account"));
            }
            return await Task.Run(() => RedirectToAction("Login", "Account"));


        }
        //------------------------------------ Muskan End----------------------------------//

    }
}