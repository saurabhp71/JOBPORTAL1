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
using GoogleAuthentication.Services;
using System.Web.Security;
using Microsoft.Ajax.Utilities;
using System.Net.Mail;
using System.Text;

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
              //  IMG = dr["ProfileIMG"].ToString();
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
            var clientId = "501741889827-ii7134kvr58e69q69cc2g6uqf867ttqc.apps.googleusercontent.com";
            var url = "http://localhost:56291/Account/Googlelogin";
            var clientsecret = "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";
            var response = GoogleAuth.GetAuthUrl(clientId, url);
            ViewBag.response = response;
            return View();

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
                    return RedirectToAction("EmployeerIndex", "Employer");
                }
                drE.Close();
             }
            BALAccount objA = new BALAccount();
            SqlDataReader drA;
            drA = obj.LoginAdmin(log);

            if (drA.HasRows)
            {
                while (drE.Read())
                {

                    employeerCode = drE["Employercode"].ToString();
                    employername = drE["EmployerName"].ToString();
                    TempData["MessageLogin"] = "Login Successfully...!!";
                    Session["Employercode"] = employeerCode;
                    Session["EmployerName"] = employername;
                    return RedirectToAction("EmployeerIndex", "Employer");
                }
                drE.Close();
            }
           //if(dr.HasRows == false && drE.HasRows == false && drA.HasRows == false)
           // {
           //     TempData["Message"] = "Please Enter Correct EmailId And Password....!!!";
           // }
          
            return await Task.Run(() => View("Register"));

        }
        public ActionResult ForgotPassword(string EmailId)
        {
            AccountUser objaccount = new AccountUser();
            objaccount.EmailId = EmailId;
            BALAccount obj = new BALAccount();
            SqlDataReader dr;
            dr = obj.forgetpasswords(objaccount);

            BALAccount objE = new BALAccount();
            SqlDataReader drE;
            drE = objE.forgetpasswordE(objaccount);

            if (dr.Read() == true)
            {
                string Email = dr["EmailId"].ToString();
                string Password = dr["Password"].ToString();
                if (Email == EmailId)
                {
                    string from = "8624077183a@gmail.com";
                    string to = EmailId;
                    MailMessage msg = new MailMessage(from, to);
                    string mailbody = "your recovery password is" + Password;
                    msg.Subject = "JobPortal Password";
                    msg.Body = mailbody;
                    msg.BodyEncoding = Encoding.UTF8;
                    msg.IsBodyHtml = true;
                    SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                    System.Net.NetworkCredential a = new System.Net.NetworkCredential("8624077183a@gmail.com", "mamuijmxmeiiybje");
                    client.EnableSsl = true;
                    client.UseDefaultCredentials = true;
                    client.Credentials = a;

                    try
                    {
                        client.Send(msg);
                        TempData["passwordMessage"] = "Password send to your register email...!!";
                        TempData["Message"] = "Password send to your register mail..!!";
                    }
                    catch (Exception)
                    {
                        TempData["InternetMessage"] = "Check Internet Connection..!!";
                        TempData["Message"] = "Check Internet Connection..!!";
                    }
                }
            }
            else if (drE.Read() == true)
            {
                string EmailE = drE["EmailId"].ToString();
                string PasswordE = drE["Password"].ToString();
                if (EmailE == EmailId)
                {
                    string from = "8624077183a@gmail.com";
                    string to = EmailE;
                    MailMessage msg = new MailMessage(from, to);
                    string mailbody = "your recovery password is" + PasswordE;
                    msg.Subject = "JobPortal Password";
                    msg.Body = mailbody;
                    msg.BodyEncoding = Encoding.UTF8;
                    msg.IsBodyHtml = true;
                    SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                    System.Net.NetworkCredential a = new System.Net.NetworkCredential("8624077183a@gmail.com", "mamuijmxmeiiybje");
                    client.EnableSsl = true;
                    client.UseDefaultCredentials = true;
                    client.Credentials = a;

                    try
                    {
                        client.Send(msg);
                        TempData["passwordMessage"] = "Password send to your register email...!!";
                        TempData["Message"] = "Password send to your register mail..!!";
                    }
                    catch (Exception)
                    {
                        TempData["InternetMessage"] = "Check Internet Connection..!!";
                        TempData["Message"] = "Check Internet Connection..!!";
                    }
                    try
                    {

                        TempData["InternetMessage"] = "Check Internet Connection..!!";
                        TempData["Message"] = "Check Internet Connection..!!";
                    }
                    catch
                    {
                        TempData["CheckMessage"] = "Check Internet Connection";
                        TempData["Message"] = "Check Internet Connection";
                    }
                }

                return RedirectToAction("Login");

            }

            return View();
        }

        //------------------------------------ Muskan End----------------------------------//

    }
}