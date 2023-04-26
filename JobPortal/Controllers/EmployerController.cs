using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using JobPortalLibrary.Employer;
using JobPortalLibrary.Admin;
using System.IO;
using System.Text.RegularExpressions;
using System.Reflection;
using static System.Net.WebRequestMethods;
using System.Threading.Tasks;

namespace JobPortal.Controllers
{
    public class EmployerController : Controller
    {

        //---------------------------mahesh start-------------------------//
        public string fileName;
        public string jobseekercode;
        // GET: Employer
        public ActionResult Index()
        {

            return View();
        }
        public ActionResult EmployeerIndex()
        {

            return View();
        }
        [HttpGet]
        public ActionResult Actions()
        {

            return PartialView("Actions");
        }


        public async Task <ActionResult> jobgrid(string searchtext)
        {
            if (Session["Employercode"] != null)
            {
                string Employercode = Session["Employercode"].ToString();



                    BALEmployer bal = new BALEmployer();
                    DataSet ds = new DataSet();

                    ds = bal.getjobgrid();

                    EmployerUser user = new EmployerUser();
                    List<EmployerUser> jobgridlst = new List<EmployerUser>();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        EmployerUser objuser = new EmployerUser();
                        objuser.JobTitle = ds.Tables[0].Rows[i]["JobTitle"].ToString();
                        objuser.Address = ds.Tables[0].Rows[i]["Address"].ToString();
                        objuser.Status = ds.Tables[0].Rows[i]["Status"].ToString();
                        objuser.PostJobCode = ds.Tables[0].Rows[i]["PostJobCode"].ToString();
                        objuser.ApplicationReceived = Convert.ToInt32(ds.Tables[0].Rows[i]["ApplicationReceived"].ToString());
                        jobgridlst.Add(objuser);



                    }
                    user.joblist = jobgridlst;
                    var result = jobgridlst.Where(a => a.Address.ToLower().Contains(searchtext));
                return await Task.Run(() => View(user));
            }
            else
            {
                return await Task.Run(() => View("Register", "Account"));
            }
           
        }
        public Object JobApplyedStudent(EmployerUser obj, string postjobcode)
        {
            EmployerUser objuser = new EmployerUser();
            objuser.PostJobCode = postjobcode;
            BALEmployer objbal = new BALEmployer();
            DataSet ds = new DataSet();
            ds = objbal.getapplyjobstud(obj);
            List<EmployerUser> jobgridlst = new List<EmployerUser>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                EmployerUser objuser1 = new EmployerUser();
                objuser1.PostJobCode = ds.Tables[0].Rows[i]["PostJobCode"].ToString();
                objuser1.Seekercode = ds.Tables[0].Rows[i]["JobSeekerCode"].ToString();
                objuser1.ViewResume = ds.Tables[0].Rows[i]["ResumePDF"].ToString();
                objuser1.AppliedJobId = Convert.ToInt32(ds.Tables[0].Rows[i]["AppliedJobID"].ToString());
                objuser1.SeekerName = ds.Tables[0].Rows[i]["SeekerName"].ToString();
                objuser1.ApplyDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["AppliedDate"].ToString());
                objuser1.Status = ds.Tables[0].Rows[i]["Status"].ToString();
                jobgridlst.Add(objuser1);
            }
            objuser.joblist = jobgridlst;

            BALEmployer objuser2 = new BALEmployer();
            DataSet ds5 = new DataSet();
            ds5 = objuser2.getactivecount(obj);
            var active = ds5.Tables[0].Rows[0]["Active"].ToString();
            ViewBag.Active = active;

            DataSet ds1 = new DataSet();
            ds1 = objuser2.getawaitingreveiwcount(obj);
            var awaitingreview = ds1.Tables[0].Rows[0]["AwaitingReview"].ToString();
            ViewBag.Awaitingreview = awaitingreview;

            DataSet ds2 = new DataSet();
            ds2 = objuser2.getreviewcount(obj);
            var Review = ds2.Tables[0].Rows[0]["Review"].ToString();
            ViewBag.Review = Review;

            DataSet ds3 = new DataSet();
            ds3 = objuser2.getcontactingcount(obj);
            var Contacting = ds3.Tables[0].Rows[0]["contacting"].ToString();
            ViewBag.contacting = Contacting;

            DataSet ds4 = new DataSet();
            ds4 = objuser2.gethirecount(obj);
            var Hire = ds4.Tables[0].Rows[0]["hire"].ToString();
            ViewBag.hire = Hire;


            DataSet ds6 = new DataSet();
            ds6 = objuser2.getrejectedcount(obj);
            var Rejected = ds6.Tables[0].Rows[0]["rejected"].ToString();
            ViewBag.Rejected = Rejected;




            return objuser;
        }
        public async Task <ActionResult> Jobapplystud(EmployerUser obj, string postjobcode)
        {
            if (Session["Employercode"] != null)
            {
                string Employercode = Session["Employercode"].ToString();

                Object objuser = JobApplyedStudent(obj, postjobcode);
                return await Task.Run(() => View(objuser));
            }
            else
            {
                return await Task.Run(() => View("Register", "Account"));
            }

        }



        public JsonResult Approve(EmployerUser obj, string postjobcode)
        {
            string Status = "";
            obj.StatusId = 9;
            string PostJobCode = postjobcode;
            BALEmployer user = new BALEmployer();
            user.getaprove(obj);
            // object objuser = JobApplyedStudent(postjobcode);
            // return RedirectToAction("Jobapplystud");
            BALEmployer objbal = new BALEmployer();
            DataSet dt = new DataSet();
            dt = objbal.getstatus(obj);
            for (int i = 0; i < dt.Tables[0].Rows.Count; i++)
            {
                Status = dt.Tables[0].Rows[i]["Status"].ToString();
            }
            return Json(Status, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Reject(EmployerUser obj)
        {
            string status = "";

            obj.StatusId = 10;
            BALEmployer objuser = new BALEmployer();
            objuser.getaprove(obj);
            BALEmployer objbal = new BALEmployer();
            DataSet dt = new DataSet();
            dt = objbal.getstatus(obj);
            for (int i = 0; i < dt.Tables[0].Rows.Count; i++)
            {
                status = dt.Tables[0].Rows[i]["Status"].ToString();
            }
            return Json(status, JsonRequestBehavior.AllowGet);

        }
        public async Task <ActionResult> JobDetails(EmployerUser obj, string PostJobCode)
        {
            if (Session["Employercode"] != null)
            {
                string Employercode = Session["Employercode"].ToString();
                // EmployerUser obj = new EmployerUser();
                obj.PostJobCode = PostJobCode;
                BALEmployer obj1 = new BALEmployer();
                SqlDataReader dr;
                dr = obj1.JobDetails(obj);
                while (dr.Read())
                {
                    //obj.PostJobCode = dr["PostJobCode"].ToString();
                    obj.CompanyName = dr["CompanyName"].ToString();
                    obj.ContactPerson = dr["ContactPerson"].ToString();
                    obj.JobTitle = dr["JobTitle"].ToString();
                    obj.JobDescription = dr["JobDescription"].ToString();
                    obj.JobCategory = dr["JobCategory"].ToString();
                    obj.OpportunityType = dr["OpportunityType"].ToString();
                    obj.WorkingShifts = dr["WorkingShifts"].ToString();
                    obj.NoOfOpenings = dr["NoOfOpenings"].ToString();
                    obj.Address = dr["Address"].ToString();
                    obj.Salary = dr["Salary"].ToString();
                    obj.TotalExperience = dr["TotalExperience"].ToString();
                    //obj.ExpectedJoiningDate =Convert.ToDateTime(dr["JoiningDate"].ToString());
                    //obj.ApplicationEndDate = Convert.ToDateTime(dr["JoiningDate"].ToString());
                }
                dr.Close();
                return await Task.Run(() => PartialView(obj));
            }
            else
            {
                return await Task.Run(() => View("Register", "Account"));
            }


        }
        public async Task <ActionResult> viewphonedetails(EmployerUser obj)
        {
            if (Session["Employercode"] != null)
            {
                string Employercode = Session["Employercode"].ToString();

                EmployerUser objuser = new EmployerUser();
                BALEmployer objbal = new BALEmployer();
                DataSet ds = new DataSet();
                ds = objbal.getphonedetails(obj);
                objuser.SeekerName = ds.Tables[0].Rows[0]["SeekerName"].ToString();
                objuser.ContactNo = Convert.ToInt64(ds.Tables[0].Rows[0]["ContactNo"].ToString());
                objuser.Alternativecontact = Convert.ToInt64(ds.Tables[0].Rows[0]["AlternateContactNo"].ToString());
                return await Task.Run(() => PartialView(objuser));
            }
            else
            {
                return await Task.Run(() => View("Register", "Account"));
            }



        }

        public async Task <ActionResult> Deletecandidate(EmployerUser obj)
        {
            if (Session["Employercode"] != null)
            {
                string Employercode = Session["Employercode"].ToString();
                // EmployerUser objuser = new EmployerUser();
                BALEmployer objbal = new BALEmployer();
                objbal.deletecandidates(obj);
                return await Task.Run(() => RedirectToAction("Jobapplystud"));
            }
            else
            {
                return await Task.Run(() => View("Register", "Account"));
            }

        }

        public async Task <ActionResult> Addcandidate(string postjobcode)
        {
            if (Session["Employercode"] != null)
            {
                string Employercode = Session["Employercode"].ToString();
                getEducation();
                return await Task.Run(() => View());
            }
            else
            {
                return await Task.Run(() => View("Register", "Account"));
            }


           

        }
        public void getEducation()
        {
            EmployerUser objuser = new EmployerUser();
            BALEmployer objbal = new BALEmployer();
            DataSet ds = new DataSet();
            ds = objbal.geteducation();
            List<SelectListItem> Educationlist = new List<SelectListItem>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Educationlist.Add(new SelectListItem
                {
                    Text = ds.Tables[0].Rows[i][1].ToString(),
                    Value = ds.Tables[0].Rows[i][0].ToString()
                });
            };
            ViewBag.Educationlist = Educationlist;


        }
        public JsonResult getdegree(EmployerUser obj)
        {
            BALEmployer objbal = new BALEmployer();
            DataSet ds2 = new DataSet();
            ds2 = objbal.getdegree(obj);
            List<SelectListItem> Educationlist = new List<SelectListItem>();
            for (int i = 0; i < ds2.Tables[0].Rows.Count; i++)
            {
                Educationlist.Add(new SelectListItem
                {
                    Text = ds2.Tables[0].Rows[i][2].ToString(),
                    Value = ds2.Tables[0].Rows[i][0].ToString()
                });


            }
            return Json(Educationlist, JsonRequestBehavior.AllowGet);




        }
        public JsonResult getspecialization(EmployerUser obj)
        {
            BALEmployer objbal = new BALEmployer();
            DataSet ds = new DataSet();
            ds = objbal.getspecialization(obj);
            List<SelectListItem> Educationlist = new List<SelectListItem>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Educationlist.Add(new SelectListItem
                {
                    Text = ds.Tables[0].Rows[i][2].ToString(),
                    Value = ds.Tables[0].Rows[i][0].ToString()
                });

            }
            return Json(Educationlist, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public async Task <ActionResult> Addcandidate(EmployerUser obj, HttpPostedFileBase myFile /*string postjobcode*/)
        {
            if (Session["Employercode"] != null)
            {
                string Employercode = Session["Employercode"].ToString();

                // string FullName = obj.FullName;
                // string EmailId = obj.EmailId;
                // string Password = obj.Password;
                // DateTime DOB = obj.DOB;
                // string Address = obj.Address;
                // int QualificationId = obj.QualificationId;
                // int DegreeId = obj.DegreeId;
                // int SpecializationId = obj.SpecializationId;
                // string univercity = obj.Univercity;
                //// string file = myFile.ToString();
                BALEmployer objbal = new BALEmployer();
                DataSet ds = new DataSet();
                ds = objbal.getseekercode();
                //int jobseekercode = Convert.ToInt32(ds.Tables[0].Rows[0]["jobseekercode"].ToString());
                string jscode = ds.Tables[0].Rows[0]["jobseekercode"].ToString();
                string[] digit = Regex.Split(jscode, @"\D+");
                for (int i = 1; i < digit.Length; i++)
                {

                    int num = Convert.ToInt32(digit[1]);

                    int increaseid = Convert.ToInt32(num) + 1;
                    jobseekercode = "JSC" + increaseid.ToString();
                }
                if (myFile != null && myFile.ContentLength > 0)
                {
                    fileName = Path.GetFileName(myFile.FileName);
                    string paths = Server.MapPath("~/Content/images");
                    string ResumePath = Path.Combine(paths, fileName);
                    myFile.SaveAs(ResumePath);
                    //user.resume = fileName;

                }
                //jobseekercode = obj.Seekercode;
                //fileName = obj.Resume;
                objbal.saveseekerdetail(obj, jobseekercode, fileName);
                int statusid = 1018;
                obj.StatusId = statusid;
                // float isdelete = 0;
                objbal.applyjob(obj, jobseekercode, statusid, fileName);
                if (obj.QualificationId == 1)
                {
                    objbal.savesscdetail(obj, jobseekercode);
                }
                else if (obj.QualificationId == 2)
                {
                    objbal.savehscdetail(obj, jobseekercode);
                }
                else if (obj.QualificationId == 3 && obj.DegreeId == 97)
                {
                    objbal.saveundergraduation(obj, jobseekercode);
                }
                else if (obj.QualificationId == 3 && obj.DegreeId != 97)
                {
                    objbal.savegraduationdetail(obj, jobseekercode);
                }
                else if (obj.QualificationId == 4)
                {
                    objbal.savepgdetail(obj, jobseekercode);
                }

                return await Task.Run(() => RedirectToAction("jobgrid"));

            }
            else
            {
                return await Task.Run(() => View("Register", "Account"));
            }
        }
        public async Task <ActionResult> RepostJob(EmployerUser obj, string PostJobCode)
        {
            if (Session["Employercode"] != null)
            {
                string Employercode = Session["Employercode"].ToString();
                //EmployerUser obj = new EmployerUser();
                obj.PostJobCode = PostJobCode;
                BALEmployer obj1 = new BALEmployer();
                SqlDataReader dr;
                dr = obj1.JobDetails(obj);
                while (dr.Read())
                {
                    //obj.PostJobCode = dr["PostJobCode"].ToString();
                    obj.CompanyId = Convert.ToInt32(dr["CompanyId"].ToString());
                    obj.CompanyName = dr["CompanyName"].ToString();
                    obj.ContactPerson = dr["ContactPerson"].ToString();
                    obj.JobTitle = dr["JobTitle"].ToString();
                    obj.JobDescription = dr["JobDescription"].ToString();
                    obj.JobCategory = dr["JobCategory"].ToString();
                    obj.OpportunityType = dr["OpportunityType"].ToString();
                    obj.WorkingShifts = dr["WorkingShifts"].ToString();
                    obj.NoOfOpenings = dr["NoOfOpenings"].ToString();
                    obj.Address = dr["Address"].ToString();
                    obj.Salary = dr["Salary"].ToString();
                    obj.TotalExperience = dr["TotalExperience"].ToString();
                    obj.ExpectedJoiningDate = Convert.ToDateTime(dr["ExpectedJoiningDate"].ToString());
                    obj.ApplicationEndDate = Convert.ToDateTime(dr["ApplicationEndDate"].ToString());
                    obj.RequiredQualificationId = dr["RequiredQualificationId"].ToString();
                }
                dr.Close();


                return await Task.Run(() => View(obj));
            }
            else
            {
                return await Task.Run(() => View("Register", "Account"));
            }
        }
        //---------------------------mahesh end-------------------------//

    }
}




