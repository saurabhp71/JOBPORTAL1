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
using System.Net.Mail;
using System.Text;
using System.Web.UI.WebControls;

namespace JobPortal.Controllers
{
    public class EmployerController : Controller
    {

        //---------------------------mahesh start-------------------------//
        public string fileName;
        string Code;
        public string jobseekercode;
        string Applylists;
        string applyList;
        string emailList;
        string postjobcode;
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


        public async Task<ActionResult> jobgrid(string searchtext)
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
                return await Task.Run(() => View("Login", "Account"));
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
                //objuser1.ApplyDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["AppliedDate"].ToString());

                DateTime ApplyDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["AppliedDate"].ToString());
                objuser1.ADate = ApplyDate.ToShortDateString();


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
        public async Task<ActionResult> Jobapplystud(EmployerUser obj, string postjobcode)
        {
            if (Session["Employercode"] != null)
            {
                string Employercode = Session["Employercode"].ToString();

                Object objuser = JobApplyedStudent(obj, postjobcode);
                return await Task.Run(() => View(objuser));
            }
            else
            {
                return await Task.Run(() => View("Login", "Account"));
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
        public async Task<ActionResult> JobDetails(EmployerUser obj, string PostJobCode)
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
                    obj.ExpectedJoiningDate1 = dr["ExpectedJoiningDate"].ToString();
                    obj.ApplicationEndDate1 = dr["ApplicationEndDate"].ToString();
                }
                dr.Close();
                obj.PostJobCode = PostJobCode;
                Session["url"] = HttpContext.Request.Url.AbsoluteUri;
                return await Task.Run(() => PartialView(obj));
            }
            else
            {
                return await Task.Run(() => View("Login", "Account"));
            }


        }
        public async Task<ActionResult> viewphonedetails(EmployerUser obj)
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
                return await Task.Run(() => View("Login", "Account"));
            }



        }

        public async Task<ActionResult> Deletecandidate(EmployerUser obj)
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
                return await Task.Run(() => View("Login", "Account"));
            }

        }

        public async Task<ActionResult> Addcandidate(string postjobcode)
        {
            if (Session["Employercode"] != null)
            {
                string Employercode = Session["Employercode"].ToString();
                getEducation();
                return await Task.Run(() => View());
            }
            else
            {
                return await Task.Run(() => View("Login", "Account"));
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
        public JsonResult getdegree(int Educationid,EmployerUser obj)
        {
            obj.QualificationId = Educationid;
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
        public JsonResult getspecialization(int DegreeId,EmployerUser obj)
        {
            obj.DegreeId = DegreeId;
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
        public async Task<ActionResult> Addcandidate(EmployerUser obj, HttpPostedFileBase myFile /*string postjobcode*/)
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
                return await Task.Run(() => View("Login", "Account"));
            }
        }
        [HttpGet]
        public async Task<ActionResult> RepostJob(EmployerUser obj, string PostJobCode)
        {
            if (Session["Employercode"] != null)
            {
                JobLocation();
                QualificationRequire();
                AllList();
                //  JobCategory();
                JobBenefits();
                Company();
                string Employercode = Session["Employercode"].ToString();
                //EmployerUser obj = new EmployerUser();
                obj.PostJobCode = PostJobCode;
                BALEmployer obj1 = new BALEmployer();
                SqlDataReader dr;
                dr = obj1.JobDetails(obj);
                while (dr.Read())
                {
                    //obj.PostJobCode = dr["PostJobCode"].ToString();
                    //obj.CompanyId = Convert.ToInt32(dr["CompanyId"].ToString());
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
                return await Task.Run(() => View("Login", "Account"));
            }


        }
        [HttpPost]
        public async Task<ActionResult> RepostJob(EmployerUser ObjEmployerUser)
        {
            if (Session["Employercode"] != null)
            {
                string employerCode = Session["Employercode"].ToString();
                ObjEmployerUser.Employercode = employerCode;
                PostJob_Code();
                ObjEmployerUser.PostJobCode = Code;
                ObjEmployerUser.ApplicationStartDate = DateTime.Now;
                ObjEmployerUser.City = string.Join(",", ObjEmployerUser.Locationlist);
                ObjEmployerUser.RequireQualification = string.Join(",", ObjEmployerUser.QualificationList);
                ObjEmployerUser.JobBenifit = string.Join(",", ObjEmployerUser.JobBenifitList);
                BALEmployer objB = new BALEmployer();
                if (ObjEmployerUser.SalaryType == "Fixed")
                {
                    objB.Editjob(ObjEmployerUser);
                    return await Task.Run(() => RedirectToAction("jobgrid"));
                }
                if (ObjEmployerUser.SalaryType == "Flexible")
                {
                    ObjEmployerUser.Salary = ObjEmployerUser.Min + " - " + ObjEmployerUser.Max;
                    objB.Editjob(ObjEmployerUser);
                    return await Task.Run(() => RedirectToAction("jobgrid"));
                }
                if (ObjEmployerUser.SalaryType == "Not Disclosed")
                {
                    ObjEmployerUser.Salary = "Not Disclosed";
                    objB.Editjob(ObjEmployerUser);
                    return await Task.Run(() => RedirectToAction("jobgrid"));
                }


            }

            else
            {
                return await Task.Run(() => RedirectToAction("RepostJob"));

            }
            return await Task.Run(() => View("Login", "Account"));
        }

        //---------------------------mahesh end-------------------------//
        //-----------------------------Mitali Start--------------------------//
        [HttpGet]
        public async Task<ActionResult> JobRegister()
        {
            if (Session["Employercode"] != null)
            {
                string EmployerCode = Session["Employercode"].ToString();


                JobLocation();
                QualificationRequire();
                AllList();
                //  JobCategory();
                JobBenefits();
                Company();

                return await Task.Run(() => View());
            }
            else
            {
                return await Task.Run(() => View("Login", "Account"));
            }
        }

        [HttpPost]
        public async Task<ActionResult> JobRegister(EmployerUser ObjEmployerUser)
        {
            if (Session["Employercode"] != null)
            {
                string employerCode = Session["Employercode"].ToString();
                ObjEmployerUser.Employercode = employerCode;
                PostJob_Code();
                ObjEmployerUser.PostJobCode = Code;
                ObjEmployerUser.ApplicationStartDate = DateTime.Now;
                ObjEmployerUser.City = string.Join(",", ObjEmployerUser.Locationlist);
                ObjEmployerUser.RequireQualification = string.Join(",", ObjEmployerUser.QualificationList);
                ObjEmployerUser.JobBenifit = string.Join(",", ObjEmployerUser.JobBenifitList);
                BALEmployer objB = new BALEmployer();
                if (ObjEmployerUser.SalaryType == "Fixed")
                {
                    objB.JobRegister(ObjEmployerUser);
                    return await Task.Run(() => RedirectToAction("JobRegister"));
                }
                if (ObjEmployerUser.SalaryType == "Flexible")
                {
                    ObjEmployerUser.Salary = ObjEmployerUser.Min + " - " + ObjEmployerUser.Max;
                    objB.JobRegister(ObjEmployerUser);
                    return await Task.Run(() => RedirectToAction("JobRegister"));
                }
                if (ObjEmployerUser.SalaryType == "Not Disclosed")
                {
                    ObjEmployerUser.Salary = "Not Disclosed";
                    objB.JobRegister(ObjEmployerUser);
                    return await Task.Run(() => RedirectToAction("JobRegister"));
                }


            }

            else
            {
                return await Task.Run(() => RedirectToAction("JobRegister"));

            }
            return await Task.Run(() => View("Login", "Account"));
        }
        public async Task<ActionResult> PostJob_Code()
        {

            EmployerUser objB = new EmployerUser();

            BALEmployer objC = new BALEmployer();
            SqlDataReader dr;
            dr = objC.PostJobCode();
            while (dr.Read())
            {

                int PostJobcode = Convert.ToInt32(dr["PostJobId"].ToString());
                PostJobcode = PostJobcode + 1;
                string PostJobId = "PJC";
                Code = PostJobId + PostJobcode;
                //objB.PostJobCode = PostJobcode;


            }
            dr.Close();
            return await Task.Run(() => View());
        }
        public async Task<ActionResult> Company()                              // regular dropdown
        {
            if (Session["Employercode"] != null)
            {
                string EmployerCode = Session["Employercode"].ToString();


                BALEmployer objB = new BALEmployer();
                EmployerUser obj = new EmployerUser();
                obj.Employercode = EmployerCode;
                DataSet ds = objB.GetCompany(obj);
                List<SelectListItem>
                Companylist = new List<SelectListItem>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Companylist.Add(new SelectListItem
                    {
                        Text = dr["CompanyName"].ToString(),
                        Value = dr["CompanyId"].ToString()
                    });
                }
                ViewBag.CompanyId = Companylist;
                return await Task.Run(() => View());
            }
            else
            {
                return await Task.Run(() => View("Login", "Account"));
            }
        }
        public async Task<ActionResult> JobBenefits()                        //multiselect dropdown
        {
            BALEmployer objB = new BALEmployer();
            EmployerUser obj = new EmployerUser();
            DataSet ds = objB.GetJobBenifits();
            List<SelectListItem>
            Benifitlist = new List<SelectListItem>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Benifitlist.Add(new SelectListItem
                {
                    Text = dr["JobBenefit"].ToString(),
                    Value = dr["JobBenefitId"].ToString()
                });
            }
            ViewBag.JobBenifit = new SelectList(Benifitlist, "Value", "Text");
            return await Task.Run(() => View());
        }
        //[HttpGet]
        public JsonResult JobCategory(int companyid)                              // regular dropdown
        {
            BALEmployer objB = new BALEmployer();
            DataSet ds = objB.GetJobCategory(companyid);
            List<SelectListItem>
            Categorylist = new List<SelectListItem>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Categorylist.Add(new SelectListItem
                {
                    Text = dr["JobCategory"].ToString(),
                    Value = dr["JobCategoryId"].ToString()
                });

            }

            return Json(Categorylist, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> JobLocation()                        //multiselect dropdown
        {
            BALEmployer objB = new BALEmployer();
            EmployerUser obj = new EmployerUser();
            DataSet ds = objB.GetLocation();
            List<SelectListItem>
            Locationlist = new List<SelectListItem>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Locationlist.Add(new SelectListItem
                {
                    Text = dr["Location"].ToString(),
                    Value = dr["CityId"].ToString()
                });
            }
            ViewBag.City = new SelectList(Locationlist, "Value", "Text");
            return await Task.Run(() => View());

        }

        public async Task<ActionResult> AllList()                                                                // hardcoded dropdown
        {
            var list = new List<string>() { "In Office", "Work from Home", "On Field", "Hybrid" };
            ViewBag.JobTypelist = list;
            var list1 = new List<string>() { "Day", "Night", "US", "12hrs" };
            ViewBag.WorkingShiftlist = list1;
            var list2 = new List<string>() { "Per Year", "Per Month", "Per Week", "Per Day" };
            ViewBag.SalaryRangeList = list2;
            //var list3 = new List<string>() { "Per Year", "Per Month", "Per Week", "Per Day" };
            //ViewBag.Fixsalarylist = list3;
            return await Task.Run(() => View());
        }

        public async Task<ActionResult> QualificationRequire()                        //multiselect dropdown
        {
            BALEmployer objB = new BALEmployer();
            EmployerUser obj = new EmployerUser();
            DataSet ds = objB.QualificationRequired();
            List<SelectListItem>
            Qualificationlist = new List<SelectListItem>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Qualificationlist.Add(new SelectListItem
                {
                    Text = dr["RequireQualification"].ToString(),
                    Value = dr["RequireQualificationId"].ToString()
                });
            }
            ViewBag.Requirequalification = new SelectList(Qualificationlist, "Value", "Text");
            return await Task.Run(() => View());
        }
        //-----------------------------------------Mitali End----------------------------------//
        //-----------------------------------------Kartik Start--------------------------------//
        public async Task<ActionResult> CompanyRegistration()
        {
            if (Session["Employercode"] != null)
            {
                string EmployerCode = Session["Employercode"].ToString();

                CompanyCategory();
                KTJobLocation();
                return await Task.Run(() => View());

            }

            else
            {
                return await Task.Run(() => View("Login", "Account"));
            }

        }
        [HttpPost]
        public async Task<ActionResult> CompanyRegistration(EmployerUser objEmployerUser, HttpPostedFileBase CompanyLogo)
        {
            //obj.Employercode = "EMP0015";
            if (Session["Employercode"] != null)
            {
                string employercode = Session["Employercode"].ToString();
                objEmployerUser.Employercode = employercode;
                if (CompanyLogo != null && CompanyLogo.ContentLength > 0)
                {
                    string image = Path.GetFileName(CompanyLogo.FileName);
                    string imgpath = Path.Combine(Server.MapPath("~/Content/Photos"), image);
                    CompanyLogo.SaveAs(imgpath);
                }
                objEmployerUser.RegistrationDate = DateTime.Now;

                BALEmployer obj1 = new BALEmployer();
                obj1.KTCompanyRegisteration(objEmployerUser);
                return await Task.Run(() => RedirectToAction("CompanyRegistration"));
            }

            else
            {
                return await Task.Run(() => View("Login", "Account"));
            }


        }

        [HttpPost]
        public JsonResult CompanyCategory()
        {
            BALEmployer objUser = new BALEmployer();
            DataSet ds = new DataSet();
            ds = objUser.KTCompanyCategoryBind();
            List<SelectListItem> Industrylist = new List<SelectListItem>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Industrylist.Add(new SelectListItem
                {
                    Text = dr["IndustryName"].ToString(),
                    Value = dr["IndustryId"].ToString()
                });
            }
            ViewBag.Industry = Industrylist;
            return Json(Industrylist, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> KTJobLocation()
        {
            BALEmployer objUser = new BALEmployer();
            DataSet ds = new DataSet();
            ds = objUser.Locationbind();
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                list.Add(new SelectListItem
                {
                    Text = dr["Location"].ToString(),
                    Value = dr["CityId"].ToString()
                });
            }
            ViewBag.City = new SelectList(list, "Value", "Text");
            return await Task.Run(() => View());
        }

        //-----------------------------------------Kartik End----------------------------------//
        //-----------------------------------------sachin start----------------------------------//
        
        [HttpGet]
        public JsonResult GetJobCategory()
        {

            BALEmployer objBal = new BALEmployer();
            DataSet ds = new DataSet();
            ds = objBal.JobCategory();
            List<SelectListItem> CategoryList = new List<SelectListItem>();
            foreach (DataRow dr in ds.Tables[2].Rows)
            {
                CategoryList.Add(new SelectListItem
                {
                    Text = dr["JobCategory"].ToString(),
                    Value = dr["JobCategoryId"].ToString()
                });
            }
            ViewBag.Category = CategoryList;
            return Json(CategoryList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetJobTitle(int JobCatId)
        {
            BALEmployer objBal = new BALEmployer();
            DataSet ds = new DataSet();
            ds = objBal.GetJobTitle(JobCatId);
            List<SelectListItem> JobtitleList = new List<SelectListItem>();
            foreach (DataRow dr in ds.Tables[1].Rows)
            {
                JobtitleList.Add(new SelectListItem
                {
                    Text = dr["JobTitle"].ToString(),
                    Value = dr["JobCategoryId"].ToString()
                });
            }
            return Json(JobtitleList, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult JobLocationSM()
        {
            BALEmployer objUser = new BALEmployer();
            DataSet ds = new DataSet();
            ds = objUser.GetJobLocation();
            List<SelectListItem> CityList = new List<SelectListItem>();
            foreach (DataRow dr in ds.Tables[1].Rows)

            {
                CityList.Add(new SelectListItem
                {
                    Text = dr["CityName"].ToString(),
                    Value = dr["CityId"].ToString()

                });
            }
            ViewBag.City = CityList;
            return Json(CityList, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public async Task<ActionResult> FindResume()
        {
            if (Session["Employercode"] != null)
            {
                GetJobCategory();
                JobLocationSM();
                int cityid = Convert.ToInt32(Session["city"]);
                if (cityid == 0)
                {
                    return await Task.Run(() => View());
                }
                else
                {
                    int cid = Convert.ToInt32(Session["city"]);
                    string jtitle = (Session["jobtitle"].ToString());
                    BALEmployer objE = new BALEmployer();
                    DataSet ds = new DataSet();
                    ds = objE.GetFindResume(cid, jtitle);
                    EmployerUser obj = new EmployerUser();
                    EmployerUser objuser = new EmployerUser();
                    List<EmployerUser> ListUser = new List<EmployerUser>();
                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    {
                        EmployerUser obju = new EmployerUser();
                        obju.Seekercode = ds.Tables[1].Rows[i]["JobSeekerCode"].ToString();
                        obju.SeekerName = ds.Tables[1].Rows[i]["SeekerName"].ToString();
                        obju.TotalExperience = ds.Tables[1].Rows[i]["TotalExperience"].ToString();
                        obju.RequireQualification = ds.Tables[1].Rows[i]["RequireQualification"].ToString();
                        obju.JobType = ds.Tables[1].Rows[i]["JobType"].ToString();
                        obju.ResumePDF = ds.Tables[1].Rows[i]["ResumePDF"].ToString();

                        ListUser.Add(obju);

                    }
                    ViewBag.ResumePDF = obj.ResumePDF;


                    objuser.ListUser = ListUser;
                    return await Task.Run(() => View(objuser));
                }
            }
            else
            {
                return await Task.Run(() => View());
            }

        }

        [HttpPost]
        public async Task<ActionResult> FindResume(FormCollection objcoll,EmployerUser obj1)
        {
            if (Session["Employercode"] != null)
            {
                JobLocationSM();
                GetJobCategory();
                
                obj1.CityId1= Convert.ToInt32(objcoll["cityid"].ToString());
                obj1.JobTitle = objcoll["jobtitle"].ToString();
                Session["city"] = obj1.CityId;
                Session["jobtitle"] = obj1.JobTitle;
                BALEmployer objE = new BALEmployer();
                DataSet ds = new DataSet();
                ds = objE.GetFindResume(obj1.CityId1, obj1.JobTitle);

                EmployerUser objuser = new EmployerUser();
                List<EmployerUser> ListUser = new List<EmployerUser>();
                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    EmployerUser obju = new EmployerUser();
                    obju.Seekercode = ds.Tables[1].Rows[i]["JobSeekerCode"].ToString();
                    obju.SeekerName = ds.Tables[1].Rows[i]["SeekerName"].ToString();
                    obju.TotalExperience = ds.Tables[1].Rows[i]["TotalExperience"].ToString();
                    obju.RequireQualification = ds.Tables[1].Rows[i]["RequireQualification"].ToString();
                    obju.JobType = ds.Tables[1].Rows[i]["JobType"].ToString();
                    obju.ResumePDF = ds.Tables[1].Rows[i]["ResumePDF"].ToString();

                    ListUser.Add(obju);

                }
                ViewBag.ResumePDF = obj1.ResumePDF;


                objuser.ListUser = ListUser;
                return await Task.Run(() => View(objuser));
            }
            else
            {
                return await Task.Run(() => View());
            }
        }

        [HttpGet]
        public async Task<ActionResult> JobSeekerDetails(string Seekercode)
        {
            //if (Session["Employercode"] != null)
            //{
                EmployerUser obju = new EmployerUser();
                obju.Seekercode = Seekercode;
                BALEmployer obj1 = new BALEmployer();
                SqlDataReader dr;
                dr = obj1.SeekerDetails(obju);
            EmployerUser obj = new EmployerUser();
            while (dr.Read())
                {
               
                //   obj.Seekercode =dr["Seekercode"].ToString();
                obj.Seekercode = dr["JobSeekerCode"].ToString();
                obj.SeekerName = dr["SeekerName"].ToString();
                    obj.EmailId = dr["EmailId"].ToString();
                    obj.ContactNo = Convert.ToInt64(dr["ContactNo"].ToString());
                    obj.City = dr["CityName"].ToString();
                    obj.ResumePDF = dr["ResumePDF"].ToString();
                
               
            }
                dr.Close();
            ViewBag.ResumePDF = obj.ResumePDF;
            return await Task.Run(() => PartialView(obj));
            //}
            //else
            //{
            //    return await Task.Run(() => View());
            //}

        }
        public ActionResult UpdateApporval(EmployerUser obj)
        {

            obj.StatusId = 9;
            BALEmployer obj1 = new BALEmployer();
            obj1.Update(obj);
            var Status = new { data = "success" };
            return Json(Status, JsonRequestBehavior.AllowGet);
            //return RedirectToAction("FindResume");

        }
        public ActionResult UpdateHold(EmployerUser obj)
        {
            obj.StatusId = 3;
            BALEmployer obj1 = new BALEmployer();
            obj1.Update(obj);
            var Status = new { data = "Hold" };
            // return RedirectToAction("FindResume");
            return Json(Status, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateRejected(EmployerUser obj)
        {
            obj.StatusId = 10;
            BALEmployer obj1 = new BALEmployer();
            obj1.Update(obj);
            var Status = new { data = "Reject" };
            return Json(Status, JsonRequestBehavior.AllowGet);
            // return RedirectToAction("FindResume");
        }

        //-----------------------------------------Ashish start----------------------------------//

        public ActionResult AllRoundsGrid(string postjobcode)
        {
            if (Session["Employercode"] != null)
            {
                if (TempData.ContainsKey("postjobcode"))
                    postjobcode = TempData["postjobcode"].ToString(); // returns "Applylist" 
                ViewBag.postjobcode = postjobcode;
                TempData["postjobcode"] = postjobcode;
               
                EmployerUser obj = new EmployerUser();
            
            obj.PostJobCode = postjobcode;
            BALEmployer ObjBal = new BALEmployer();
            DataSet ds = new DataSet();
            ds = ObjBal.AllRoundsGrid(obj);
            EmployerUser objDetails = new EmployerUser();
            List<EmployerUser> lstUserDt1 = new List<EmployerUser>();
            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                EmployerUser obju = new EmployerUser();
                    obju.PostJobCode = ds.Tables[1].Rows[i]["PostJobCode"].ToString();
                    obju.JobSeekerName = ds.Tables[1].Rows[i]["SeekerName"].ToString();
                obju.Status = ds.Tables[1].Rows[i]["Status"].ToString();
                obju.SeekerEmail = ds.Tables[1].Rows[i]["EmailId"].ToString();
                obju.AppliedJobId = Convert.ToInt32(ds.Tables[1].Rows[i]["AppliedJobID"].ToString());

                lstUserDt1.Add(obju);
            }
            objDetails.LstUser = lstUserDt1;
            return View(objDetails);
            }
            else
            {
                return View("Login", "Account");
            }

        }
        public async Task<ActionResult> RoundsGrid()
        {
            if (Session["Employercode"] != null)
            {
                if (TempData.ContainsKey("postjobcode"))
                    postjobcode = TempData["postjobcode"].ToString(); // returns "Applylist" 
                ViewBag.postjobcode = postjobcode;
                TempData["postjobcode"] = postjobcode;
                EmployerUser obj = new EmployerUser();

                obj.PostJobCode = postjobcode;
                BALEmployer ObjBal = new BALEmployer();
                DataSet ds = new DataSet();
                ds = ObjBal.RoundsGrid(obj);
                EmployerUser objDetails = new EmployerUser();
                List<EmployerUser> lstUserDt1 = new List<EmployerUser>();
                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    EmployerUser obju = new EmployerUser();

                    obju.JobSeekerName = ds.Tables[1].Rows[i]["SeekerName"].ToString();
                    obju.Status = ds.Tables[1].Rows[i]["Status"].ToString();
                    obju.SeekerEmail = ds.Tables[1].Rows[i]["EmailId"].ToString();
                    obju.AppliedJobId = Convert.ToInt32(ds.Tables[1].Rows[i]["AppliedJobId"].ToString());

                    lstUserDt1.Add(obju);
                }
                objDetails.LstUser = lstUserDt1;
                return await Task.Run(() => View(objDetails));
            }
            else
            {
                return await Task.Run(() => View("Login", "Account"));
            }

        }

        [HttpPost]
        public async Task<ActionResult> ShowRounds()
        {
            if (Session["Employercode"] != null)
            {
                BALEmployer ObjBal = new BALEmployer();
                DataSet ds = new DataSet();
                ds = ObjBal.ShowRounds();
                EmployerUser objDetails = new EmployerUser();
                List<EmployerUser> lstUserDt1 = new List<EmployerUser>();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    EmployerUser obju = new EmployerUser();

                    obju.RoundName = ds.Tables[0].Rows[i]["RoundName"].ToString();

                    lstUserDt1.Add(obju);
                }
                objDetails.LstUser = lstUserDt1;
                return await Task.Run(() => View(objDetails));
            }
            else
            {
                return await Task.Run(() => View("Login", "Account"));
            }

        }
        [HttpGet]
        public async Task<ActionResult> CreateRoundwithDetails(string Applylist, string Email)
        {
            if (Session["Employercode"] != null)
            {
                var list = new List<string>() { "Online Interview ", "Walk In", "Phone Interview" };
                ViewBag.list = list;
                EmployerUser obj = new EmployerUser();
                //     ViewBag.applyList = Applylist;
                TempData["applyList"] = Applylist;
                TempData["emailList"] = Email;
                return await Task.Run(() => PartialView("CreateRoundwithDetails", obj));
            }
            else
            {
                return await Task.Run(() => View("Login", "Account"));
            }
        }
        [HttpPost]
        public async Task<ActionResult> CreateRoundwithDetails(EmployerUser obj)
        {
            if (Session["Employercode"] != null)
            {

                if (TempData.ContainsKey("applyList"))
                applyList = TempData["applyList"].ToString(); // returns "Applylist" 
            obj.Applycodelist = applyList;
            int StatusId = 6;
            obj.StatusId = StatusId;

            BALEmployer objB = new BALEmployer();
            objB.CreateRoundwithDetails(obj);

            //if (TempData.ContainsKey("emailList"))
            //    emailList = TempData["emailList"].ToString(); // returns "Applylist" 
            //var emailid = emailList;
            ////  char[] seperator = { ',' };
            ////  string[] email = null;
            ////   email = emailid.Split(seperator);
            //string[] email = emailid.Split(',');
            // obj.SeekerEmail = email.ToString();
            //for (int i = 0; i < email.Length; i++)
            //{ 
            //if (ModelState.IsValid)
            //{
            // string Employercode = Session["EmployerCode"].ToString();
            var applylist = applyList;
            string[] aplst = applylist.Split(',');
            EmployerUser objE = new EmployerUser();
            BALEmployer obj1 = new BALEmployer();
            SqlDataReader dr;

            List<string> lstUserDt1 = new List<string>();
            for (int i = 0; i < aplst.Length; i++)
            {
                int AppliedJID = Convert.ToInt32(aplst[i]);
                dr = obj1.GetEmail(AppliedJID);


                while (dr.Read())
                {

                    objE.SeekerEmail = dr["EmailId"].ToString();
                    lstUserDt1.Add(objE.SeekerEmail);
                }
                dr.Close();

            }
            BALEmployer obj2 = new BALEmployer();
            SqlDataReader dr1;

            for (int i = 0; i < aplst.Length; i++)
            {
                int AppliedJID = Convert.ToInt32(aplst[i]);
                dr1 = obj2.GetCompanyName(AppliedJID);
                while (dr1.Read())
                {
                    objE.CompanyName = dr1["CompanyName"].ToString();
                    objE.HRName = dr1["HRName"].ToString();
                    objE.HRNumber = dr1["HRNumber"].ToString();
                    //  lstUserDt1.Add(objE.CompanyName);
                }
                dr1.Close();
            }

            MailMessage mail = new MailMessage();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<b>Dear Candidate,</b>");
            sb.AppendFormat("<br />");
            sb.AppendFormat("<br />");
            sb.AppendFormat("<br />");
            sb.AppendFormat("<b>Greetings From</b>" + " " + objE.CompanyName + ",");
            sb.AppendFormat("<br />");
            sb.AppendFormat("<br />");
            sb.AppendLine("This has reference to your job application applied, We are pleased to inform you that your profile has been shortlisted.");
            sb.AppendFormat("<br />");
            sb.AppendFormat("<br />");
            sb.AppendLine("<b>Round Name:</b> " + obj.RoundName);
            sb.AppendFormat("<br />");
            sb.AppendLine("<b>Interview Date:</b> " + obj.InterviewDate);
            sb.AppendFormat("<br />");
            sb.AppendLine("<b>Interview Timing From:</b> " + obj.StartTime);
            sb.AppendLine("<b>To:</b> " + obj.EndTime);
            sb.AppendFormat("<br />");
            sb.AppendLine("<b>Instructions:</b> " + obj.Comment);
            sb.AppendFormat("<br />");
            sb.AppendLine("<b>InteviewType:</b> " + obj.InterviewType);
            sb.AppendFormat("<br />");
            sb.AppendFormat("<br />");
            sb.AppendLine("<b>Interview Address:</b> " + obj.InterviewAddress);
            sb.AppendFormat("<br />");
            sb.AppendFormat("<br />");
            sb.AppendFormat("<br />");
            sb.AppendFormat("<br />");
            sb.AppendLine("Thanks and Regards, ");
            sb.AppendFormat("<br />");
            sb.AppendLine("<b>Hr Name :</b>" + objE.HRName);
            sb.AppendFormat("<br />");
            sb.AppendLine("<b>Hr Number:</b> " + objE.HRNumber);
            //  mail.Body = sb.ToString();

            mail.From = new MailAddress("8624077183a@gmail.com");
            mail.Subject = "Interview Invitation";
            //  string Address = obj.InterviewAddress;
            // string Time = obj.StartTime.ToString();
            mail.Body = sb.ToString();
            foreach (string n in lstUserDt1)
            {
                //     string EmailId = email[i];
                mail.To.Add(new MailAddress(n));
            }
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential("8624077183a@gmail.com", "mamuijmxmeiiybje"); // Enter seders User name and password  
            smtp.EnableSsl = true;
            smtp.Send(mail);
            // return View("RoundsGridR5", obj);

            return await Task.Run(() => RedirectToAction("RoundsGridR5"));
        }
         else
            {
                return await Task.Run(() => View("Login", "Account"));
            }

}
        public async Task<ActionResult> UpdateRound1list(string Applylist)
        {
            if (Session["Employercode"] != null)
            {
                EmployerUser obj = new EmployerUser();
                string temp = Applylist;
                obj.StatusId = 11;
                string[] AppliedJobID = temp.Split(',');
                BALEmployer objB = new BALEmployer();
                for (int i = 0; i < AppliedJobID.Length; i++)
                {
                    int AppliedJID = Convert.ToInt32(AppliedJobID[i]);
                    objB.UpdateRound1(AppliedJID,obj);
                }

                return await Task.Run(() => RedirectToAction("RoundsGrid"));
            }
            else
            {
                return await Task.Run(() => View("Login", "Account"));
            }
        }
        public async Task<ActionResult> RejectatRound1list(string Applylist)
        {
            if (Session["Employercode"] != null)
            {
                EmployerUser obj = new EmployerUser();
            string temp = Applylist;
            obj.StatusId = 17;
            string[] AppliedJobID = temp.Split(',');
            BALEmployer objB = new BALEmployer();
            for (int i = 0; i < AppliedJobID.Length; i++)
            {
                int AppliedJID = Convert.ToInt32(AppliedJobID[i]);
                objB.UpdateRound1(AppliedJID,obj);
            }

            return await Task.Run(() => RedirectToAction("RoundsGrid"));
        }
        else
            {
                return await Task.Run(() => View("Login", "Account"));
            }
}
        public async Task<ActionResult> UpdateRound1(int AppliedJobId)
        {
            if (Session["Employercode"] != null)
            {
                EmployerUser obj = new EmployerUser();
                obj.AppliedJobId = AppliedJobId;
                obj.StatusId = 12;
                BALEmployer objB = new BALEmployer();
                objB.UpdateRound1(obj.AppliedJobId,obj);

                return await Task.Run(() => RedirectToAction("RoundsGrid"));
            }
            else
            {
                return await Task.Run(() => View("Login", "Account"));
            }
        }
        public async Task<ActionResult> RejectatRound1(int AppliedJobId)
        {
            if (Session["Employercode"] != null)
            {
                EmployerUser obj = new EmployerUser();
            obj.AppliedJobId = AppliedJobId;
            obj.StatusId = 17;
            BALEmployer objB = new BALEmployer();
            objB.UpdateRound1(obj.AppliedJobId, obj);

            return await Task.Run(() => RedirectToAction("RoundsGrid"));
        }
          else
            {
                return await Task.Run(() => View("Login", "Account"));
            }
}
        public async Task<ActionResult> RoundsGridR2()
        {
            if (Session["Employercode"] != null)
            {
                if (TempData.ContainsKey("postjobcode"))
                    postjobcode = TempData["postjobcode"].ToString(); // returns "Applylist" 
                ViewBag.postjobcode = postjobcode;
                TempData["postjobcode"] = postjobcode;
                EmployerUser obj = new EmployerUser();
            
            obj.PostJobCode = postjobcode;
            BALEmployer ObjBal = new BALEmployer();
            DataSet ds = new DataSet();
            ds = ObjBal.RoundsGridR2(obj);
            EmployerUser objDetails = new EmployerUser();
            List<EmployerUser> lstUserDt1 = new List<EmployerUser>();
            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                EmployerUser obju = new EmployerUser();

                obju.JobSeekerName = ds.Tables[1].Rows[i]["SeekerName"].ToString();
                obju.Status = ds.Tables[1].Rows[i]["Status"].ToString();
                obju.SeekerEmail = ds.Tables[1].Rows[i]["EmailId"].ToString();
                obju.AppliedJobId = Convert.ToInt32(ds.Tables[1].Rows[i]["AppliedJobId"].ToString());

                lstUserDt1.Add(obju);
            }
            objDetails.LstUser = lstUserDt1;
            return await Task.Run(() => View(objDetails));
            }
            else
            {
                return await Task.Run(() => View("Login", "Account"));
            }

        }
        public async Task<ActionResult> UpdateRound2list(string Applylist)
        {
            if (Session["Employercode"] != null)
            {
                EmployerUser obj = new EmployerUser();
            obj.StatusId = 11;
            string temp = Applylist;
            string[] AppliedJobID = temp.Split(',');
            BALEmployer objB = new BALEmployer();
            for (int i = 0; i < AppliedJobID.Length; i++)
            {
                int AppliedJID = Convert.ToInt32(AppliedJobID[i]);
                objB.UpdateRound1(AppliedJID,obj);
            }

            return await Task.Run(() => RedirectToAction("RoundsGridR2"));
        }
          else
            {
                return await Task.Run(() => View("Login", "Account"));
            }
}
        public async Task<ActionResult> RejectatRound2list(string Applylist)
        {
            if (Session["Employercode"] != null)
            {
                EmployerUser obj = new EmployerUser();
            obj.StatusId = 18;
            string temp = Applylist;
            string[] AppliedJobID = temp.Split(',');
            BALEmployer objB = new BALEmployer();
            for (int i = 0; i < AppliedJobID.Length; i++)
            {
                int AppliedJID = Convert.ToInt32(AppliedJobID[i]);
                objB.UpdateRound1(AppliedJID,obj);
            }

            return await Task.Run(() => RedirectToAction("RoundsGridR2"));
            }
            else
            {
                return await Task.Run(() => View("Login", "Account"));
            }
        }
        public async Task<ActionResult> UpdateRound2(int AppliedJobId)
        {
            if (Session["Employercode"] != null)
            {
                EmployerUser obj = new EmployerUser();
            obj.AppliedJobId = AppliedJobId;
            obj.StatusId = 13;
            BALEmployer objB = new BALEmployer();
            objB.UpdateRound1(obj.AppliedJobId, obj);

            return await Task.Run(() => RedirectToAction("RoundsGridR2"));
        }
          else
            {
                return await Task.Run(() => View("Login", "Account"));
            }
}
        public async Task<ActionResult> RejectatRound2(int AppliedJobId)
        {
            if (Session["Employercode"] != null)
            {
                EmployerUser obj = new EmployerUser();
            obj.AppliedJobId = AppliedJobId;
            obj.StatusId = 18;
            BALEmployer objB = new BALEmployer();
            objB.UpdateRound1(obj.AppliedJobId, obj);

            return await Task.Run(() => RedirectToAction("RoundsGridR2"));
            }
            else
            {
                return await Task.Run(() => View("Login", "Account"));
            }
        }
        public async Task<ActionResult> RoundsGridR3()

        {
            if (Session["Employercode"] != null)
            {
                if (TempData.ContainsKey("postjobcode"))
                    postjobcode = TempData["postjobcode"].ToString(); // returns "Applylist" 
                ViewBag.postjobcode = postjobcode;
                TempData["postjobcode"] = postjobcode;
                EmployerUser obj = new EmployerUser();
            
            obj.PostJobCode = postjobcode;
            BALEmployer ObjBal = new BALEmployer();
            DataSet ds = new DataSet();
            ds = ObjBal.RoundsGridR3(obj);
            EmployerUser objDetails = new EmployerUser();
            List<EmployerUser> lstUserDt1 = new List<EmployerUser>();
            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                EmployerUser obju = new EmployerUser();

                obju.JobSeekerName = ds.Tables[1].Rows[i]["SeekerName"].ToString();
                obju.Status = ds.Tables[1].Rows[i]["Status"].ToString();
                obju.SeekerEmail = ds.Tables[1].Rows[i]["EmailId"].ToString();
                obju.AppliedJobId = Convert.ToInt32(ds.Tables[1].Rows[i]["AppliedJobId"].ToString());

                lstUserDt1.Add(obju);
            }
            objDetails.LstUser = lstUserDt1;
            return await Task.Run(() => View(objDetails));
        }
          else
            {
                return await Task.Run(() => View("Login", "Account"));
            }

}
        public async Task<ActionResult> UpdateRound3list(string Applylist)
        {
            if (Session["Employercode"] != null)
            {
                EmployerUser obj = new EmployerUser();
            obj.StatusId = 11;
            string temp = Applylist;
            string[] AppliedJobID = temp.Split(',');
            BALEmployer objB = new BALEmployer();
            for (int i = 0; i < AppliedJobID.Length; i++)
            {
                int AppliedJID = Convert.ToInt32(AppliedJobID[i]);
                objB.UpdateRound1(AppliedJID,obj);
            }

            return await Task.Run(() => RedirectToAction("RoundsGridR3"));
            }
            else
            {
                return await Task.Run(() => View("Login", "Account"));
            }
        }
        public async Task<ActionResult> RejectatRound3list(string Applylist)
        {
            if (Session["Employercode"] != null)
            {
                EmployerUser obj = new EmployerUser();
            obj.StatusId = 19;
            string temp = Applylist;
            string[] AppliedJobID = temp.Split(',');
            BALEmployer objB = new BALEmployer();
            for (int i = 0; i < AppliedJobID.Length; i++)
            {
                int AppliedJID = Convert.ToInt32(AppliedJobID[i]);
                objB.UpdateRound1(AppliedJID,obj);
            }

            return await Task.Run(() => RedirectToAction("RoundsGridR3"));
        }
          else
            {
                return await Task.Run(() => View("Login", "Account"));
            }
}
        public async Task<ActionResult> UpdateRound3(int AppliedJobId)
        {
            if (Session["Employercode"] != null)
            {
                EmployerUser obj = new EmployerUser();
            obj.AppliedJobId = AppliedJobId;
            obj.StatusId = 14;
            BALEmployer objB = new BALEmployer();
            objB.UpdateRound1(obj.AppliedJobId, obj);

            return await Task.Run(() => RedirectToAction("RoundsGridR3"));
            }
            else
            {
                return await Task.Run(() => View("Login", "Account"));
            }
        }
        public async Task<ActionResult> RejectatRound3(int AppliedJobId)
        {
            if (Session["Employercode"] != null)
            {
                EmployerUser obj = new EmployerUser();
            obj.AppliedJobId = AppliedJobId;
            obj.StatusId = 19;
            BALEmployer objB = new BALEmployer();
            objB.UpdateRound1(obj.AppliedJobId, obj);

            return await Task.Run(() => RedirectToAction("RoundsGridR3"));
        }
          else
            {
                return await Task.Run(() => View("Login", "Account"));
            }
}
        public async Task<ActionResult> RoundsGridR4()
        {
            if (Session["Employercode"] != null)
            {
                if (TempData.ContainsKey("postjobcode"))
                    postjobcode = TempData["postjobcode"].ToString(); // returns "Applylist" 
                ViewBag.postjobcode = postjobcode;
                TempData["postjobcode"] = postjobcode;
                EmployerUser obj = new EmployerUser();
            
            obj.PostJobCode = postjobcode;
            BALEmployer ObjBal = new BALEmployer();
            DataSet ds = new DataSet();
            ds = ObjBal.RoundsGridR4(obj);
            EmployerUser objDetails = new EmployerUser();
            List<EmployerUser> lstUserDt1 = new List<EmployerUser>();
            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                EmployerUser obju = new EmployerUser();

                obju.JobSeekerName = ds.Tables[1].Rows[i]["SeekerName"].ToString();
                obju.Status = ds.Tables[1].Rows[i]["Status"].ToString();
                obju.SeekerEmail = ds.Tables[1].Rows[i]["EmailId"].ToString();
                obju.AppliedJobId = Convert.ToInt32(ds.Tables[1].Rows[i]["AppliedJobId"].ToString());

                lstUserDt1.Add(obju);
            }
            objDetails.LstUser = lstUserDt1;
            return await Task.Run(() => View(objDetails));
            }
            else
            {
                return await Task.Run(() => View("Login", "Account"));
            }

        }
        public async Task<ActionResult> UpdateRound4list(string Applylist)
        {
            if (Session["Employercode"] != null)
            {
                EmployerUser obj = new EmployerUser();
            obj.StatusId = 11;
            string temp = Applylist;
            string[] AppliedJobID = temp.Split(',');
            BALEmployer objB = new BALEmployer();
            for (int i = 0; i < AppliedJobID.Length; i++)
            {
                int AppliedJID = Convert.ToInt32(AppliedJobID[i]);
                objB.UpdateRound1(AppliedJID,obj);
            }

            return await Task.Run(() => RedirectToAction("RoundsGridR4"));
        }
          else
            {
                return await Task.Run(() => View("Login", "Account"));
            }
}
        public async Task<ActionResult> RejectatRound4list(string Applylist)
        {
            if (Session["Employercode"] != null)
            {
                EmployerUser obj = new EmployerUser();
            obj.StatusId = 20;
            string temp = Applylist;
            string[] AppliedJobID = temp.Split(',');
            BALEmployer objB = new BALEmployer();
            for (int i = 0; i < AppliedJobID.Length; i++)
            {
                int AppliedJID = Convert.ToInt32(AppliedJobID[i]);
                objB.UpdateRound1(AppliedJID,obj);
            }

            return await Task.Run(() => RedirectToAction("RoundsGridR4"));
            }
            else
            {
                return await Task.Run(() => View("Login", "Account"));
            }
        }
        public async Task<ActionResult> UpdateRound4(int AppliedJobId)
        {
            if (Session["Employercode"] != null)
            {
                EmployerUser obj = new EmployerUser();
            obj.AppliedJobId = AppliedJobId;
            obj.StatusId = 15;
            BALEmployer objB = new BALEmployer();
            objB.UpdateRound1(obj.AppliedJobId, obj);

            return await Task.Run(() => RedirectToAction("RoundsGridR4"));
        }
          else
            {
                return await Task.Run(() => View("Login", "Account"));
            }
}
        public async Task<ActionResult> RejectatRound4(int AppliedJobId)
        {
            if (Session["Employercode"] != null)
            {
                EmployerUser obj = new EmployerUser();
            obj.AppliedJobId = AppliedJobId;
            obj.StatusId = 20;
            BALEmployer objB = new BALEmployer();
            objB.UpdateRound1(obj.AppliedJobId, obj);

            return await Task.Run(() => RedirectToAction("RoundsGridR4"));
            }
            else
            {
                return await Task.Run(() => View("Login", "Account"));
            }
        }
        [HttpGet]
        public async Task<ActionResult> RoundsGridR5()
        {
            if (Session["Employercode"] != null)
            {
                if (TempData.ContainsKey("postjobcode"))
                    postjobcode = TempData["postjobcode"].ToString(); // returns "Applylist" 
                ViewBag.postjobcode = postjobcode;
                TempData["postjobcode"] = postjobcode;
                EmployerUser obj = new EmployerUser();
         
            obj.PostJobCode = postjobcode;
            BALEmployer ObjBal = new BALEmployer();
            DataSet ds = new DataSet();
            ds = ObjBal.RoundsGridR5(obj);
            EmployerUser objDetails = new EmployerUser();
            List<EmployerUser> lstUserDt1 = new List<EmployerUser>();
            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                EmployerUser obju = new EmployerUser();

                obju.JobSeekerName = ds.Tables[1].Rows[i]["SeekerName"].ToString();
                obju.Status = ds.Tables[1].Rows[i]["Status"].ToString();
                obju.SeekerEmail = ds.Tables[1].Rows[i]["EmailId"].ToString();
                obju.AppliedJobId = Convert.ToInt32(ds.Tables[1].Rows[i]["AppliedJobId"].ToString());

                lstUserDt1.Add(obju);
            }
            ViewBag.AppliedJobId = new SelectList(lstUserDt1, "Value", "Text");
            objDetails.LstUser = lstUserDt1;
            return await Task.Run(() => View(objDetails));
        }
          else
            {
                return await Task.Run(() => View("Login", "Account"));
            }
}
        [HttpGet]
        public async Task<ActionResult> UpdateRound5list(string Applylist)
        {
            if (Session["Employercode"] != null)
            {
                EmployerUser obj = new EmployerUser();
            obj.StatusId = 11;
            string temp = Applylist;
            string[] AppliedJobID = temp.Split(',');
            BALEmployer objB = new BALEmployer();
            for (int i = 0; i < AppliedJobID.Length; i++)
            {
                int AppliedJID = Convert.ToInt32(AppliedJobID[i]);
                objB.UpdateRound1(AppliedJID,obj);
            }

            return await Task.Run(() => RedirectToAction("RoundsGridR5"));
            }
            else
            {
                return await Task.Run(() => View("Login", "Account"));
            }
        }
        public async Task<ActionResult> RejectatRound5list(string Applylist)
        {
            if (Session["Employercode"] != null)
            {
                EmployerUser obj = new EmployerUser();
            obj.StatusId = 21;
            string temp = Applylist;
            string[] AppliedJobID = temp.Split(',');
            BALEmployer objB = new BALEmployer();
            for (int i = 0; i < AppliedJobID.Length; i++)
            {
                int AppliedJID = Convert.ToInt32(AppliedJobID[i]);
                objB.UpdateRound1(AppliedJID,obj);
            }

            return await Task.Run(() => RedirectToAction("RoundsGridR5"));
        }
          else
            {
                return await Task.Run(() => View("Login", "Account"));
            }
}
        public async Task<ActionResult> UpdateRound5(int AppliedJobId)
        {
            if (Session["Employercode"] != null)
            {
                EmployerUser obj = new EmployerUser();
            obj.AppliedJobId = AppliedJobId;
            obj.StatusId = 11;
            BALEmployer objB = new BALEmployer();
            objB.UpdateRound1(obj.AppliedJobId, obj);

            return await Task.Run(() => RedirectToAction("RoundsGridR5"));
            }
            else
            {
                return await Task.Run(() => View("Login", "Account"));
            }
        }
        public async Task<ActionResult> RejectatRound5(int AppliedJobId)
        {
            if (Session["Employercode"] != null)
            {
                EmployerUser obj = new EmployerUser();
            obj.AppliedJobId = AppliedJobId;
            obj.StatusId = 21;
            BALEmployer objB = new BALEmployer();
            objB.UpdateRound1(obj.AppliedJobId, obj);

            return await Task.Run(() => RedirectToAction("RoundsGridR5"));
        }
          else
            {
                return await Task.Run(() => View("Login", "Account"));
            }
}
        //-----------------------------------------Ashish End----------------------------------//
    }

}




