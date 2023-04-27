using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Data;
using JobPortalLibrary.JobSeeker;
using System.IO;
using System.Reflection;
using JobPortalLibrary.Employer;
using Microsoft.Ajax.Utilities;
using System.Threading.Tasks;

namespace JobPortal.Controllers
{
    public class JobSeekerController : Controller
    {

        //--------------------------------Saurabh Start------------------//
        string seekercode;
        string month;
        string year;
        // GET: JobSeeker
        public ActionResult SeekerIndex()
        {

            return View();
        }
        public async Task <ActionResult> AllList()
        {
            var list = new List<string>() { "General", "Scheduled Caste(SC)", "Scheduled Tribe (ST)", "OBC - Creamy", "OBC - Non Creamy", "Other" };
            ViewBag.Castlist = list;
            var list1 = new List<string>() { "Single/unmarried", "Married", "Widowed", "Divorced", "Separated", "Other" };
            ViewBag.Maritiallist = list1;
            var list2 = new List<string>() { "2023", "2022", "2021", "2020", "2019", "2018", "2017", "2016", "2015", "2014", "2014", "2013", "2012", "2011", "2010", "2009", "2008", "2007", "2006", "2005", "2004", "2003", "2002", "2001", "2000", "1999", "1998", "1997", "1996", "1995", "1994", "1993", "1992", "1991", "1990" };
            ViewBag.YearList = list2;
            var list3 = new List<string>() { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            ViewBag.Monthlist = list3;
            var list4 = new List<string>() { "15 Days or less", "1 Month", "2 Month", "3 Month", "Serving Notice Period" };
            ViewBag.Noticelist = list4;
            var list5 = new List<string>() { "0 Month", "1 Month", "2 Month", "3 Month", "4 Month", "5 Month", "6 Month", "7 Month", "8 Month", "9 Month", "10 Month", "11 Month" };
            ViewBag.EXMonth = list5;
            var list6 = new List<string>() { "0 Years","1 Years", "2 Years", "3 Years", "4 Years", "5 Years", "6 Years", "7 Years", "8 Years", "9 Years", "10 Years", "11 Years", "12 Years", "13 Years", "14 Years", "15 Years", "16 Years", "17 Years", "18 Years", "19 Years", "20 Years", "21 Years", "22 Years", "23 Years", "24 Years", "25 Years", "26 Years", "27 Years", "28 Years", "29 Years", "30 Years", "30+ Years"};
            ViewBag.EXYear = list6;
            return await Task.Run(()=> View());
        }
        public async Task<ActionResult> CityBind()
        {
            BALSeeker objuser = new BALSeeker();
            DataSet ds = new DataSet();
            ds = objuser.CityBind();
            List<SelectListItem> citylist = new List<SelectListItem>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                citylist.Add(new SelectListItem
                {
                    Text = dr["location"].ToString(),
                    Value = dr["CityId"].ToString()
                });
            }

            ViewBag.CityId = new SelectList(citylist, "Value", "Text");
            return await Task.Run(() => View());
        }
        public async Task<ActionResult> Language()
        {
            BALSeeker objuser = new BALSeeker();
            DataSet ds = new DataSet();
            ds = objuser.Language();
            List<SelectListItem> Countrylist = new List<SelectListItem>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Countrylist.Add(new SelectListItem
                {
                    Text = dr["Language"].ToString(),
                    Value = dr["LanguageId"].ToString()
                });
            }

            ViewBag.LanguageId = new SelectList(Countrylist, "Value", "Text");
            return await Task.Run(() => View());
        }
        [HttpGet]
        public async Task<ActionResult> SeekerDetailsPhoto()
        {
            if (Session["SeekerCode"] != null)
            {
                string seekercode = Session["SeekerCode"].ToString();
                SeekerUser obj = new SeekerUser();
                obj.Seekercode = seekercode;
                BALSeeker obj1 = new BALSeeker();
                SqlDataReader dr;
                dr = obj1.SeekerDetails(obj);
                while (dr.Read())
                {
                    obj.Seekercode = dr["Seekercode"].ToString();
                    obj.ProfileIMG = dr["ProfileIMG"].ToString();

                }
                ViewBag.ProfileIMG = obj.ProfileIMG;
                dr.Close();
                return await Task.Run(() => View(obj));
            }
            else
            {
                return await Task.Run(() => View("Register", "Account"));
            }
        }
        [HttpPost]
        public async Task<ActionResult> SeekerDetailsPhoto(HttpPostedFileBase Photo)
        {
            if (Session["SeekerCode"] != null)
            {
                string seekercode = Session["SeekerCode"].ToString();

                SeekerUser obj = new SeekerUser();
                obj.Seekercode = seekercode;
                BALSeeker objsave = new BALSeeker();
                if (Photo != null && Photo.ContentLength > 0)
                {
                    string image = Path.GetFileName(Photo.FileName);
                    string path = Server.MapPath("~/Content/images");
                    string imgpath = Path.Combine(path, image);
                    Photo.SaveAs(imgpath);
                    obj.ProfileIMG = image;
                    objsave.updateIMG(obj);
                }
                return await Task.Run(() => RedirectToAction("SeekerDetails"));
            }
            else
            {
                return await Task.Run(() => View("Register", "Account"));
                
            }
        }

        [HttpPost]
        public async Task<ActionResult> SeekerDetailsPopup(SeekerUser obj)
        {
            if (Session["SeekerCode"] != null)
            {
                string seekercode = Session["SeekerCode"].ToString();

                obj.LanguageId = string.Join(",", obj.LanguageList);
                obj.Seekercode = seekercode;
                obj.ContactNo = Convert.ToInt64(obj.AlternateContactNo);
                obj.Pincode = Convert.ToInt32(obj.Pincode1);
                BALSeeker objsave = new BALSeeker();
                objsave.PersonalDetails(obj);
                return await Task.Run(() => RedirectToAction("SeekerDetails"));
            }
            else
            {
                return await Task.Run(() => View("Register", "Account"));
            }
        }
        public async Task<ActionResult> SeekerDetails()
        {
            if (Session["SeekerCode"] != null)
            {
                SeekerUser obj = new SeekerUser();
                string seekercode = Session["SeekerCode"].ToString();
                obj.Seekercode = seekercode;
                CompleteProfile();
                CityBind();
                AllList();
            
                BALSeeker obj1 = new BALSeeker();
                SqlDataReader dr;
                dr = obj1.SeekerDetails(obj);
                while (dr.Read())
                {
                    obj.Seekercode = dr["Seekercode"].ToString();
                    obj.ProfileSummary = dr["ProfileSummary"].ToString();
                    obj.SeekerName = dr["SeekerName"].ToString();
                    obj.AlternateContactNo = dr["AlternateContactNo"].ToString();
                    obj.DOB1 = dr["DOB"].ToString();
                    obj.Gender = dr["Gender"].ToString();
                    obj.CorrespondenceAddress = dr["CorrespondenceAddress"].ToString();
                    obj.PermanantAddress = dr["PermanentAddress"].ToString();
                    obj.Pincode1 = dr["Pincode"].ToString();
                    obj.CityId1 = dr["CityId"].ToString();
                    obj.Physically = dr["PhysicallyChallenged"].ToString();
                    obj.LanguageId = dr["LanguageId"].ToString();
                    obj.CasteCategory = dr["CasteCategory"].ToString();
                    obj.MaritalStatus = dr["MaritalStatus"].ToString();
                    obj.City = dr["CityName"].ToString();
                    obj.ProfileIMG = dr["ProfileIMG"].ToString();
            }
            dr.Close();
            
            if(obj.LanguageId != null && obj.LanguageId != "")
                {
                    var languageid = obj.LanguageId;
                    char[] seperator = { ',' };
                    string[] language = null;
                    language = languageid.Split(seperator);
                    string languages1 = null;
                    string languages2 = null;

                    for (int i = 0; i < language.Length; i++)
                    {
                        BALSeeker objbal = new BALSeeker();
                        DataTable dt = new DataTable();
                        dt = objbal.LanguageShow(Convert.ToInt32(language[i]));
                        languages1 = dt.Rows[0][1].ToString();


                        if (i == language.Length - 1)
                        {
                            languages2 = string.Concat(languages2, languages1);
                        }
                        else
                        {
                            languages2 = string.Concat(languages2, languages1, ",");
                        }
                        obj.Language = languages2;
                    }


                }
                else { obj.Language = ""; }

            //ViewBag.CityId = obj.CityId;
            //ViewBag.CityName = obj.City;
            return View(obj);
            }

            else
            {
                return await Task.Run(() => View("Register", "Account"));
            }
        }
        [HttpGet]
        public async Task<ActionResult>SeekerDetailsPopup()
        {
            if (Session["SeekerCode"] != null)
            {
                SeekerUser obj = new SeekerUser();
                string seekercode = Session["SeekerCode"].ToString();
                obj.Seekercode = seekercode;
                Language();
                CityBind();
                AllList();
               
                BALSeeker obj1 = new BALSeeker();
                SqlDataReader dr;
                dr = obj1.SeekerDetails(obj);
                while (dr.Read())
                {
                    obj.Seekercode = dr["Seekercode"].ToString();
                    obj.ProfileSummary = dr["ProfileSummary"].ToString();
                    obj.SeekerName = dr["SeekerName"].ToString();
                    obj.AlternateContactNo = dr["AlternateContactNo"].ToString();
                    obj.DOB1 = dr["DOB"].ToString();
                    obj.Gender = dr["Gender"].ToString();
                    obj.CorrespondenceAddress = dr["CorrespondenceAddress"].ToString();
                    obj.PermanantAddress = dr["PermanentAddress"].ToString();
                    obj.Pincode1 = dr["Pincode"].ToString();
                    obj.CityId1 = dr["CityId"].ToString();
                    obj.Physically = dr["PhysicallyChallenged"].ToString();
                    obj.LanguageId = dr["LanguageId"].ToString();
                    obj.CasteCategory = dr["CasteCategory"].ToString();
                    obj.MaritalStatus = dr["MaritalStatus"].ToString();
                    obj.City = dr["CityName"].ToString();
                    obj.ProfileIMG = dr["ProfileIMG"].ToString();
                }
                dr.Close();
                return await Task.Run(() => View(obj));
            }
            else 
            {
                return await Task.Run(() => View("Register", "Account"));
            }
        }
        //------------------------EducationDetails--------------------//

        public async Task<ActionResult> EducationDetails()
        {
            if (Session["SeekerCode"] != null)
            {
                CompleteProfile();
                string seekercode = Session["SeekerCode"].ToString();
                SeekerUser obj = new SeekerUser();
                obj.Seekercode = seekercode;
                    BALSeeker obj1 = new BALSeeker();
                    SqlDataReader dr;
                    dr = obj1.GetEducationDetails(obj);
                    while (dr.Read())
                    {
                        obj.Seekercode = dr["Seekercode"].ToString();
                        obj.SeekerName = dr["SeekerName"].ToString();
                        obj.ProfileIMG = dr["ProfileIMG"].ToString();

                        obj.SSC = dr["SSC"].ToString();
                        obj.SSCBoard = dr["SSCBoard"].ToString();
                        obj.SSCPassingYear = dr["SSCPassingYear"].ToString();
                        obj.SSCmarks = dr["SSCmarks"].ToString();
                        obj.SSCBoard1 = dr["SSCD"].ToString();

                        obj.HSC = dr["HSC"].ToString();
                        obj.HSCBoard = dr["HSCBoard"].ToString();
                        obj.HSCPassingYear = dr["HSCPassingYear"].ToString();
                        obj.HSCmarks = dr["HSCmarks"].ToString();
                        obj.HSCBoard1 = dr["HSCQ"].ToString();

                        obj.UG = dr["UG"].ToString();
                        obj.UGDegree = dr["UGDegree"].ToString();
                        obj.UGSpecialization = dr["UGDegree"].ToString();
                        obj.UGUniversity = dr["UGUniversity"].ToString();
                        obj.UGmarks = dr["UGMarks"].ToString();
                        obj.UGPassingYear = dr["UGPassingYear"].ToString();
                        obj.UGCourseType = "  -" + dr["UGCourseType"].ToString();
                        obj.UG1 = dr["UGQ"].ToString();
                        obj.UGDegree1 = dr["UGD"].ToString();
                        obj.UGSpecialization1 = dr["UGSp"].ToString();

                        obj.Graduation = dr["Graduation"].ToString();
                        obj.GraduationDegree = dr["GraduationDegree"].ToString();
                        obj.GraduationSpecialization = dr["GraduationSpecialization"].ToString();
                        obj.GraduationUniversity = dr["GraduationUniversity"].ToString();
                        obj.Graduationmarks = dr["Graduationmarks"].ToString();
                        obj.GraduationPassingYear = dr["GraduationPassingYear"].ToString();
                        obj.GCourseType = "  -" + dr["GCourseType"].ToString();
                        obj.G1 = dr["GRQ"].ToString();
                        obj.GDegree1 = dr["GRD"].ToString();
                        obj.GSpecialization1 = dr["GRS"].ToString();

                        obj.PG = dr["PG"].ToString();
                        obj.PGDegree = dr["PGDegree"].ToString();
                        obj.PGSpecialization = dr["PGSpecialization"].ToString();
                        obj.PGUniversity = dr["PGUniversity"].ToString();
                        obj.PGmarks = dr["PGmarks"].ToString();
                        obj.PGPassingYear = dr["PGPassingYear"].ToString();
                        obj.PGCourseType = "  -" + dr["PGCourseType"].ToString();
                        obj.PG1 = dr["PGQ"].ToString();
                        obj.PGDegree1 = dr["PGD"].ToString();
                        obj.PGSpecialization1 = dr["PGS"].ToString();



                    }

                    ViewBag.SSC = obj.SSC;
                    ViewBag.SSCBoard = obj.SSCBoard;
                    ViewBag.SSCDegree = obj.SSCBoard1;

                    ViewBag.HSC = obj.HSC;
                    ViewBag.HSCBoard = obj.HSCBoard;
                    ViewBag.HSCDegree = obj.SSCBoard1;

                    ViewBag.UG = obj.UG;
                    ViewBag.UG1 = obj.UG1;
                    ViewBag.UGDegree = obj.UGDegree;
                    ViewBag.UGDegree1 = obj.UGDegree1;
                    ViewBag.UGSpecialization = obj.UGSpecialization;
                    ViewBag.UGSpecialization1 = obj.UGSpecialization1;

                    ViewBag.Graduation = obj.Graduation;
                    ViewBag.G1 = obj.G1;
                    ViewBag.GraduationDegree = obj.GraduationDegree;
                    ViewBag.GDegree1 = obj.GDegree1;
                    ViewBag.GraduationSpecialization = obj.GraduationSpecialization;
                    ViewBag.GSpecialization1 = obj.GSpecialization1;

                    ViewBag.PG = obj.PG;
                    ViewBag.PG1 = obj.PG1;
                    ViewBag.PGDegree = obj.PGDegree;
                    ViewBag.PGDegree1 = obj.PGDegree1;
                    ViewBag.PGSpecialization = obj.PGSpecialization;
                    ViewBag.PGSpecialization1 = obj.PGSpecialization1;

                    dr.Close();
                return await Task.Run(() => View(obj));
            }

            else
            {
                return await Task.Run(() => View("Register", "Account"));
            }
        }
      
        [HttpGet]
        public async Task<ActionResult> ADDEducationDetails()
        {
            AllList();
            Qualification();
            return await Task.Run(() => View());
        }
        [HttpPost]
        public async Task<ActionResult> DeleteSSC(SeekerUser obj)
        {
            if (Session["SeekerCode"] != null)
            {
                string seekercode = Session["SeekerCode"].ToString();

                obj.Seekercode = seekercode;

                BALSeeker objsave = new BALSeeker();
                if (obj.SSC == "1")
                {
                    obj.QualificationId = 5;
                    obj.DegreeId = 1;

                    objsave.AddSSC(obj);
                }

                return await Task.Run(() => RedirectToAction("EducationDetails"));
            }

            else
            {
                return await Task.Run(() => View("Register", "Account"));
            }
        }
        [HttpPost]
        public async Task<ActionResult> DeleteHSC(SeekerUser obj)
        {
            if (Session["SeekerCode"] != null)
            {
                string seekercode = Session["SeekerCode"].ToString();
                
                obj.Seekercode = seekercode;
                BALSeeker objsave = new BALSeeker();
                if (obj.HSC == "2")
                {
                    obj.QualificationId = 5;
                    obj.DegreeId = 1;
                    objsave.AddHSC(obj);
                }

                return  await Task.Run(() => RedirectToAction("EducationDetails"));
            }

            else
            {
                return await Task.Run(() => View("Register", "Account"));
            }
        }
        [HttpPost]
        public async Task<ActionResult> DeleteUG(SeekerUser obj)
        {
            if (Session["SeekerCode"] != null)
            {
                string seekercode = Session["SeekerCode"].ToString();

                obj.Seekercode = seekercode;
                BALSeeker objsave = new BALSeeker();
                if (obj.UG == "3" && obj.UGDegree == "97")
                {
                    obj.QualificationId = 5;
                    obj.DegreeId = 1;
                    obj.SpecializationId = 1;
                    objsave.AddUG(obj);
                }

                return await Task.Run(() => RedirectToAction("EducationDetails"));
            }

            else
            {
                return await Task.Run(() => View("Register", "Account"));
            }
        }
        [HttpPost]
        public async Task<ActionResult> DeleteGraduation(SeekerUser obj)
        {
            if (Session["SeekerCode"] != null)
            {
                string seekercode = Session["SeekerCode"].ToString();

                obj.Seekercode = seekercode;
                BALSeeker objsave = new BALSeeker();
                if (obj.Graduation == "3" && obj.GraduationDegree != "97")
                {
                    obj.QualificationId = 5;
                    obj.DegreeId = 1;
                    obj.SpecializationId = 1;
                    objsave.AddGraduation(obj);
                }

                return await Task.Run(() => RedirectToAction("EducationDetails"));
            }

            else
            {
                return await Task.Run(() => View("Register", "Account"));
            }
        }
        [HttpPost]
        public async Task<ActionResult> DeletePG(SeekerUser obj)
        {
            if (Session["SeekerCode"] != null)
            {
                string seekercode = Session["SeekerCode"].ToString();

                obj.Seekercode = seekercode;
                BALSeeker objsave = new BALSeeker();
                if (obj.PG == "4")
                {
                    obj.QualificationId = 5;
                    obj.DegreeId = 1;
                    obj.SpecializationId = 1;
                    objsave.AddPG(obj);
                }

                return await Task.Run(() => RedirectToAction("EducationDetails"));
            }

            else
            {
                return await Task.Run(() => View("Register", "Account"));
            }
        }
        [HttpPost]
        public async Task<ActionResult> ADDEducationDetails(SeekerUser obj)
        {
            if (Session["SeekerCode"] != null)
            {
                string seekercode = Session["SeekerCode"].ToString();

                obj.Seekercode = seekercode;

                BALSeeker objsave = new BALSeeker();
                if (obj.QualificationId == 1)
                {
                    objsave.AddSSC(obj);
                }
                if (obj.QualificationId == 2)
                {
                    objsave.AddHSC(obj);
                }
                if (obj.QualificationId == 3 && obj.DegreeId == 97)
                {
                    objsave.AddUG(obj);
                }
                if (obj.QualificationId == 3 && obj.DegreeId != 97)
                {
                    objsave.AddGraduation(obj);
                }         
                if (obj.QualificationId == 4)
                {
                    objsave.AddPG(obj);
                }

                return await Task.Run(() => RedirectToAction("EducationDetails"));
            }

            else
            {
                return await Task.Run(() => View("Register", "Account"));
            }
        }
        [HttpPost]
         public async Task<ActionResult> Qualification()
        {
            BALSeeker objuser = new BALSeeker();
            DataSet ds = new DataSet();
            ds = objuser.Qualification();
            List<SelectListItem> Qualificationlist = new List<SelectListItem>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Qualificationlist.Add(new SelectListItem
                {
                    Text = dr["Qualification"].ToString(),
                    Value = dr["QualificationId"].ToString()
                });
            }

            ViewBag.QualificationId = new SelectList(Qualificationlist, "Value", "Text");
            return await Task.Run(() => View());
        }
        [HttpPost]
        public JsonResult Degree(int Qualification_id)
        {
            BALSeeker objuser = new BALSeeker();
            DataSet ds = objuser.Degree(Qualification_id);
            List<SelectListItem> Degreelist = new List<SelectListItem>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Degreelist.Add(new SelectListItem { Text = dr["Degree"].ToString(), Value = dr["DegreeId"].ToString() });
            }
            // ViewBag.DegreeId = new SelectList(Degreelist, "Value", "Text");
            return Json(Degreelist, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public JsonResult Specialization(int Degree_id)
        {
            BALSeeker objuser = new BALSeeker();
            DataSet ds = objuser.Specialization(Degree_id);
            List<SelectListItem> Specializationlist = new List<SelectListItem>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Specializationlist.Add(new SelectListItem { Text = dr["Specialization"].ToString(), Value = dr["SpecializationId"].ToString() });
            }
            //  ViewBag.SpecializationId = new SelectList(Specializationlist, "Value", "Text");
            return Json(Specializationlist, JsonRequestBehavior.AllowGet);

        }
        //------Employment Details-----//
        public async Task<ActionResult> Skill()
        {
            BALSeeker objuser = new BALSeeker();
            DataSet ds = new DataSet();
            ds = objuser.Skill();
            List<SelectListItem> skilllist = new List<SelectListItem>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                skilllist.Add(new SelectListItem
                {
                    Text = dr["SkillName"].ToString(),
                    Value = dr["SkillId"].ToString()
                });
            }

            ViewBag.SkillName = new SelectList(skilllist, "Value", "Text");
            return await Task.Run(() => View());
        }

        public async Task<ActionResult> EmploymentDetails()
        {
            if (Session["SeekerCode"] != null)
            {
                CompleteProfile();
                string seekercode = Session["SeekerCode"].ToString();
                SeekerUser obj = new SeekerUser();
                obj.Seekercode = seekercode;
                BALSeeker obj1 = new BALSeeker();
                SqlDataReader dr;
                dr = obj1.GetEmploymentDetails(obj);
                while (dr.Read())
                {
                    obj.Seekercode = dr["Seekercode"].ToString();
                    obj.SeekerName = dr["SeekerName"].ToString();
                    obj.ProfileIMG = dr["ProfileIMG"].ToString();

                    obj.EmploymentCurrent = dr["CurrentEmployement"].ToString();
                    obj.Designation = dr["Designation"].ToString();
                    obj.EmployementType = dr["EmployementType"].ToString();
                    obj.CompanyName = dr["CompanyName"].ToString();
                    obj.JobProfile = dr["JobProfile"].ToString();
                    obj.NoticePeriod = dr["NoticePeriod"].ToString();
                    obj.JoiningDate = "  ." + dr["JoiningDate"].ToString();
                }
                dr.Close();
                return await Task.Run(() => View(obj));
            }
            else
            {
                return await Task.Run(() => View("Register", "Account"));
            }
        }
        [HttpGet]
        public async Task<ActionResult> EmploymentDetailsPopup()
        {
            if (Session["SeekerCode"] != null)
            {
                string seekercode = Session["SeekerCode"].ToString();

                Skill();
                AllList();
                SeekerUser obj = new SeekerUser();
                obj.Seekercode = seekercode;
                BALSeeker obj1 = new BALSeeker();
                SqlDataReader dr;
                dr = obj1.GetEmploymentDetails(obj);
                while (dr.Read())
                {
                    obj.Seekercode = dr["Seekercode"].ToString();
                    obj.EmploymentCurrent = dr["CurrentEmployement"].ToString();
                    obj.Designation = dr["Designation"].ToString();
                    obj.EmployementType = dr["EmployementType"].ToString();
                    obj.CompanyName = dr["CompanyName"].ToString();
                    obj.JobProfile = dr["JobProfile"].ToString();
                    obj.NoticePeriod = dr["NoticePeriod"].ToString();
                    obj.JoiningDate = "  ." + dr["JoiningDate"].ToString();
                }
                dr.Close();
                return await Task.Run(() => View(obj));
            }
            else
            {
                return await Task.Run(() => View("Register", "Account"));
            }
        }

        [HttpPost]
        public async Task<ActionResult> EmploymentDetails(SeekerUser obj)
        {
            if (Session["SeekerCode"] != null)
            {
                CompleteProfile();
                string seekercode = Session["SeekerCode"].ToString();

                obj.SkillName = string.Join(",", obj.SkillList);
                obj.Seekercode = seekercode;

                obj.JoiningDate = obj.Month + (" ") + obj.Year + " (" + obj.ExYear + " " + obj.ExMonth + ")";

                BALSeeker objsave = new BALSeeker();
                objsave.UpdateEmploymentDetails(obj);
                return await Task.Run(() => RedirectToAction("EmploymentDetails"));

            }
            else
            {
                return await Task.Run(() => View("Register", "Account"));
            }
        }
        //------Project Details-----//
        public async Task<ActionResult> ProjectDetails()
        {
            if (Session["SeekerCode"] != null)
            {
                CompleteProfile();
                string seekercode = Session["SeekerCode"].ToString();

                SeekerUser obj = new SeekerUser();
                obj.Seekercode = seekercode;
                BALSeeker obj1 = new BALSeeker();
                SqlDataReader dr;
                dr = obj1.GetProjectDetails(obj);
                while (dr.Read())
                {
                    obj.Seekercode = dr["Seekercode"].ToString();
                    obj.SeekerName = dr["SeekerName"].ToString();
                    obj.ProfileIMG = dr["ProfileIMG"].ToString();

                    obj.ProjectTitle = dr["ProjectTitle"].ToString();
                    obj.ClientName = dr["ClientName"].ToString();
                    obj.ProjectStatus = dr["ProjectStatus"].ToString();
                    obj.TotalExperience = dr["TotalExperience"].ToString();
                    obj.ProjectDetails = dr["ProjectDetails"].ToString();
                }
                dr.Close();
                return await Task.Run(() => View(obj));
            }
            else
            {
                return await Task.Run(() => View("Register", "Account"));
            }
        }
        [HttpGet]
        public async Task<ActionResult> ProjectDetailsPop()
        {
            if (Session["SeekerCode"] != null)
            {
                string seekercode = Session["SeekerCode"].ToString();

                SeekerUser obj = new SeekerUser();
                obj.Seekercode = seekercode;
                BALSeeker obj1 = new BALSeeker();
                SqlDataReader dr;
                dr = obj1.GetProjectDetails(obj);
                while (dr.Read())
                {
                    obj.Seekercode = dr["Seekercode"].ToString();
                    obj.ProjectTitle = dr["ProjectTitle"].ToString();
                    obj.ClientName = dr["ClientName"].ToString();
                    obj.ProjectStatus = dr["ProjectStatus"].ToString();
                    //   obj.TotalExperience = dr["TotalExperience"].ToString();
                    obj.ProjectDetails = dr["ProjectDetails"].ToString();
                }
                dr.Close();
                return await Task.Run(() => View(obj));

            }
            else
            {
                return await Task.Run(() => View("Register", "Account"));
            }
        }
        [HttpPost]
        public async Task<ActionResult> ProjectDetailsPop(SeekerUser obj)
        {
            if (Session["SeekerCode"] != null)
            {
                string seekercode = Session["SeekerCode"].ToString();

                obj.Seekercode = seekercode;

                if (obj.ProjectStatus == "In Progress")
                {
                    int nowyear = DateTime.Now.Year - obj.StartDate.Year;
                    int nowmonth = DateTime.Now.Month - obj.StartDate.Month;
                    obj.TotalExperience = nowyear + " Year     " + nowmonth + " Month";
                    BALSeeker objsave = new BALSeeker();
                    objsave.UpdateProjectDetails(obj);

                }

                if (obj.ProjectStatus == "Finished")
                {
                    int nowyear = obj.EdnDate.Year - obj.StartDate.Year;
                    int nowmonth = obj.EdnDate.Month - obj.StartDate.Month;
                    obj.TotalExperience = nowyear + " Year  " + nowmonth + " Month";
                    BALSeeker objsave = new BALSeeker();
                    objsave.UpdateProjectDetails(obj);

                }
                return await Task.Run(() => RedirectToAction("ProjectDetails"));
            }
            else
            {
                return await Task.Run(() => View("Register", "Account"));
            }
        }
        // -------------------------Career Profile--------------------------//
        [HttpGet]
        public async Task<ActionResult> Industry()
        {
            BALSeeker objuser = new BALSeeker();
            DataSet ds = new DataSet();
            ds = objuser.Industry();
            List<SelectListItem> industrylist = new List<SelectListItem>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                industrylist.Add(new SelectListItem
                {
                    Text = dr["IndustryName"].ToString(),
                    Value = dr["IndustryId"].ToString()
                });
            }

            ViewBag.IndustryID = new SelectList(industrylist, "Value", "Text");
            return await Task.Run(() => View());
        }
        [HttpGet]
        public JsonResult JobCategory(int Industry_id)
        {
            BALSeeker objuser = new BALSeeker();
            DataSet ds = objuser.JobCategory(Industry_id);
            List<SelectListItem> categorylist = new List<SelectListItem>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                categorylist.Add(new SelectListItem { Text = dr["JobCategory"].ToString(), Value = dr["JobCategoryId"].ToString() });
            }
            return Json(categorylist, JsonRequestBehavior.AllowGet);

        }

        public async Task<ActionResult> CareerProfile()
        {
            if (Session["SeekerCode"] != null)
            {
                CompleteProfile();
                string seekercode = Session["SeekerCode"].ToString();

                    SeekerUser obj = new SeekerUser();
                    obj.Seekercode = seekercode;
                    BALSeeker obj1 = new BALSeeker();
                    SqlDataReader dr;
                    dr = obj1.CareerProfile(obj);
                    while (dr.Read())
                    {
                        obj.Seekercode = dr["Seekercode"].ToString();
                        obj.SeekerName = dr["SeekerName"].ToString();
                        obj.ProfileIMG = dr["ProfileIMG"].ToString();

                        obj.IndustryID = Convert.ToInt32(dr["CurrentIndustry"].ToString());
                        obj.Industry = dr["IndustryName"].ToString();
                        obj.TotalExperience = dr["TotalExperience"].ToString();
                        obj.Location = dr["CurrentLocation"].ToString();
                        obj.JobCategoryId = Convert.ToInt32(dr["Category"].ToString());
                        obj.JobCategory = dr["JobCategory"].ToString();            
                        obj.EmailId = dr["AlertEmail"].ToString();
                        //  obj.ContactNo = Convert.ToInt32(dr["AlertPhone"].ToString());

                    }
                    dr.Close();
                if(obj.Location != null && obj.Location != "")
                {
                    var languageid = obj.Location;
                    char[] seperator = { ',' };
                    string[] language = null;
                    language = languageid.Split(seperator);
                    string languages1 = null;
                    string languages2 = null;

                    for (int i = 0; i < language.Length; i++)
                    {
                        BALSeeker objbal = new BALSeeker();
                        DataTable dt = new DataTable();
                        dt = objbal.PreferredLocation(Convert.ToInt32(language[i]));
                        languages1 = dt.Rows[0][2].ToString();


                        if (i == language.Length - 1)
                        {
                            languages2 = string.Concat(languages2, languages1);
                        }
                        else
                        {
                            languages2 = string.Concat(languages2, languages1, ",");
                        }
                        obj.Location = languages2;
                    }
                }
                else { obj.Location = null; }
                    ViewBag.IndustryID = obj.IndustryID;
                    ViewBag.Industry = obj.Industry;
                    ViewBag.JobCategoryId = obj.JobCategoryId;
                    ViewBag.JobCategory = obj.JobCategory;

                return await Task.Run(() => View(obj));
            }
            else
            {
                return await Task.Run(() => View("Register", "Account"));
            }
        }
        [HttpGet]
        public async Task<ActionResult> CareerProfilePopup()
        {
            if (Session["SeekerCode"] != null)
            {
                string seekercode = Session["SeekerCode"].ToString();

                    AllList();
                    CityBind();
                    Industry();
                    SeekerUser obj = new SeekerUser();
                    obj.Seekercode = seekercode;
                    BALSeeker obj1 = new BALSeeker();
                    SqlDataReader dr;
                    dr = obj1.CareerProfile(obj);
                    while (dr.Read())
                    {
                        obj.Seekercode = dr["Seekercode"].ToString();
                        //obj.IndustryID = Convert.ToInt32(dr["CurrentIndustry"].ToString());
                        obj.Industry = dr["IndustryName"].ToString();
                        obj.TotalExperience = dr["TotalExperience"].ToString();
                        obj.Location = dr["CurrentLocation"].ToString();
                        //obj.JobCategoryId = Convert.ToInt32(dr["Category"].ToString());
                        obj.JobCategory = dr["JobCategory"].ToString();
                        obj.EmailId = dr["AlertEmail"].ToString();
                        //  obj.ContactNo = Convert.ToInt32(dr["AlertPhone"].ToString());

                    }
                    dr.Close();
                return await Task.Run(() => View(obj));
            }
            else
            {
                return await Task.Run(() => View("Register", "Account"));
            }
        }
        [HttpPost]
        public async Task<ActionResult> CareerProfilePopup(SeekerUser obj)
        {
            if (Session["SeekerCode"] != null)
            {
                string seekercode = Session["SeekerCode"].ToString();

                obj.Location = string.Join(",", obj.LocationList);
                obj.Seekercode = seekercode;

                obj.TotalExperience = obj.Month + (" ") + obj.Year + " (" + obj.ExYear + " " + obj.ExMonth + ")";

                BALSeeker objsave = new BALSeeker();
                objsave.UpdateCareerProfile(obj);
                return await Task.Run(() => RedirectToAction("CareerProfile"));
            }
            else
            {
                return await Task.Run(() => View("Register", "Account"));
            }
        }
        public async Task<ActionResult> PreferredJob(string seekercode)
        {
            if (Session["SeekerCode"] != null)
            {
                seekercode = Session["SeekerCode"].ToString();

                SeekerUser obj = new SeekerUser();
                obj.Seekercode = seekercode;
                obj.Seekercode = seekercode;
                BALSeeker ObjBal = new BALSeeker();
                DataSet ds = new DataSet();
                ds = ObjBal.PreferredJob(obj);
                SeekerUser objDetails = new SeekerUser();
                List<SeekerUser> lstUserDt1 = new List<SeekerUser>();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    SeekerUser obju = new SeekerUser();

                    obju.JobTitle = ds.Tables[0].Rows[i]["JobTitle"].ToString();
                    obju.Location = ds.Tables[0].Rows[i]["JobLocation"].ToString();
                    obju.PostJobCode = ds.Tables[0].Rows[i]["PostJobCode"].ToString();
                    obju.CompanyName = ds.Tables[0].Rows[i]["CompanyName"].ToString();
                    obju.TotalExperience = ds.Tables[0].Rows[i]["TotalExperience"].ToString();
                    obju.CurrentSalary = ds.Tables[0].Rows[i]["Salary"].ToString();
                    obju.JobType = ds.Tables[0].Rows[i]["JobType"].ToString();

                    if(obju.Location != null && obju.Location != "")
                    {
                        var languageid = obju.Location;
                        char[] seperator = { ',' };
                        string[] language = null;
                        language = languageid.Split(seperator);
                        string languages1 = null;
                        string languages2 = null;

                        for (int k = 0; k < language.Length; k++)
                        {
                            BALSeeker objbal = new BALSeeker();
                            DataTable dt = new DataTable();
                            dt = objbal.PreferredLocation(Convert.ToInt32(language[k]));
                            languages1 = dt.Rows[0][2].ToString();


                            if (k == language.Length - 1)
                            {
                                languages2 = string.Concat(languages2, languages1);
                            }
                            else
                            {
                                languages2 = string.Concat(languages2, languages1, ",");
                            }
                            obju.Location = languages2;
                        }
                    }
                    else { obju.Location = null; }

                   
                    lstUserDt1.Add(obju);
                }
                objDetails.lstuser = lstUserDt1;

                return await Task.Run(() => View(objDetails));
            }
            else
            {
                return await Task.Run(() => View("Register", "Account"));
            }

        }
        public async Task<ActionResult> AllCompany()
        {         
            BALSeeker ObjBal = new BALSeeker();
            DataSet ds = new DataSet();
            ds = ObjBal.AllCompanys();
            SeekerUser objDetails = new SeekerUser();
            List<SeekerUser> lstUserDt1 = new List<SeekerUser>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                SeekerUser obju = new SeekerUser();

                obju.CompanyId = Convert.ToInt32(ds.Tables[0].Rows[i]["CompanyId"].ToString());
                obju.CompanyName = ds.Tables[0].Rows[i]["CompanyName"].ToString();
                obju.Slogan = ds.Tables[0].Rows[i]["Slogan"].ToString();
                obju.CompanyLogo = ds.Tables[0].Rows[i]["CompanyLogo"].ToString();
                obju.Rating = ds.Tables[0].Rows[i]["Rating"].ToString();
                obju.Follow = Convert.ToInt32(ds.Tables[0].Rows[i]["Follow"]);
                //obju.Review = ds.Tables[0].Rows[i]["Review"].ToString();

                lstUserDt1.Add(obju);
            }
            objDetails.lstuser = lstUserDt1;

            return await Task.Run(() => View(objDetails));

        }
        [HttpGet]
        public async Task<ActionResult> CompanysDetails(int id)
        {
            SeekerUser obj = new SeekerUser();
            obj.CompanyId = id;
            BALSeeker obj1 = new BALSeeker();
            SqlDataReader dr;
            dr = obj1.CompanysDetails(obj);
            while (dr.Read())
            {
                obj.CompanyId = Convert.ToInt32(dr["CompanyId"].ToString());
                obj.Employercode = dr["Employercode"].ToString();
                obj.CompanyName = dr["CompanyName"].ToString();
                obj.CompanyLogo = dr["CompanyLogo"].ToString();
                obj.Slogan = dr["Slogan"].ToString();
                obj.Rating = dr["Rating"].ToString();
                obj.Follow = Convert.ToInt32(dr["Follow"]);
                obj.AboutCompany = dr["AboutCompany"].ToString();
                obj.AboutCompany = dr["AboutCompany"].ToString();
                obj.NoOfEmployees = dr["NoOfEmployees"].ToString();
                obj.CompanyWebsite = dr["CompanyWebsite"].ToString();
               
            }
            dr.Close();
            return await Task.Run(() => View(obj));
        }
        [HttpPost]
        public async Task<ActionResult> FollowCompany( int Companyid)
        {
            if (Session["SeekerCode"] != null)
            {

                seekercode = Session["SeekerCode"].ToString();
                SeekerUser obj = new SeekerUser();
                obj.CompanyId = Companyid;
                obj.Seekercode = seekercode;

                BALSeeker obj1 = new BALSeeker();
                SqlDataReader dr;
                dr = obj1.CompanysDetails(obj);
                while (dr.Read())
                {
                    obj.CompanyId = Convert.ToInt32(dr["CompanyId"].ToString());
                    obj.Employercode = dr["Employercode"].ToString();
                    obj.Follow2 = dr["Follow1"].ToString();
                }
                dr.Close();
               if(obj.Follow2 == "True")
                {
                    obj.Follow1 = false;
                    obj1.FollowCompany(obj);
                    return await Task.Run(() => Json(new { status = "false"}));
                }
                else
                {
                    obj.Follow1 = true;
                    obj1.FollowCompany(obj);
                    return await Task.Run(() => Json(new { status = "true"}));
                }
               
                
            }                
            else
            {
                return await Task.Run(() => View("Register", "Account"));
            }
        }
        public async Task<ActionResult> CompleteProfile()
        {
            if (Session["SeekerCode"] != null)
            {
                seekercode = Session["SeekerCode"].ToString();

                int total = 0;
                SeekerUser obj = new SeekerUser();
                obj.Seekercode = seekercode;
                BALSeeker obj1 = new BALSeeker();
                SqlDataReader dr;
                dr = obj1.CompleteProfile(obj);
                while (dr.Read())
                {
                    obj.Seekercode = dr["Seekercode"].ToString();

                    obj.ProfileSummary = dr["ProfileSummary"].ToString();
                    if(obj.ProfileSummary != null && obj.ProfileSummary != "") { total += 2; } else { total += 0; };
                    obj.SeekerName = dr["SeekerName"].ToString();
                    if (obj.SeekerName != null && obj.SeekerName != "") { total += 2; } else { total += 0; };
                    obj.ContactNo1 = dr["ContactNo"].ToString();
                    if (obj.ContactNo1 != null && obj.ContactNo1 != "") { total += 2; } else { total += 0; };
                    obj.AlternateContactNo = dr["AlternateContactNo"].ToString();
                    if (obj.AlternateContactNo != null && obj.AlternateContactNo != "") { total += 2; } else { total += 0; };
                    obj.DOB1 = dr["DOB"].ToString();
                    if (obj.DOB1 != null && obj.DOB1 != "") { total += 2; } else { total += 0; };
                    obj.Gender = dr["Gender"].ToString();
                    if (obj.Gender != null && obj.Gender != "") { total += 2; } else { total += 0; };
                    obj.CorrespondenceAddress = dr["CorrespondenceAddress"].ToString();
                    if (obj.CorrespondenceAddress != null && obj.CorrespondenceAddress != "") { total += 2; } else { total += 0; };
                    obj.PermanantAddress = dr["PermanentAddress"].ToString();
                    if (obj.PermanantAddress != null && obj.PermanantAddress != "") { total += 2; } else { total += 0; };
                    obj.Pincode1 = dr["Pincode"].ToString();
                    if (obj.Pincode1 != null && obj.Pincode1 != "") { total += 2; } else { total += 0; };
                    obj.City = dr["CityId"].ToString();
                    if (obj.City != null && obj.City != "") { total += 2; } else { total += 0; };
                    obj.Physically = dr["PhysicallyChallenged"].ToString();
                    if (obj.Physically != null && obj.Physically != "") { total += 2; } else { total += 0; };
                    obj.LanguageId = dr["LanguageId"].ToString();
                    if (obj.LanguageId != null && obj.LanguageId != "" ) { total += 2; } else { total += 0; };
                    obj.CasteCategory = dr["CasteCategory"].ToString();
                    if (obj.CasteCategory != null && obj.CasteCategory != "") { total += 2; } else { total += 0; };
                    obj.MaritalStatus = dr["MaritalStatus"].ToString();
                    if (obj.MaritalStatus != null && obj.MaritalStatus != "") { total += 2; } else { total += 0; };
                    obj.ProfileIMG = dr["ProfileIMG"].ToString();
                    if (obj.ProfileIMG != null && obj.ProfileIMG != "") { total += 2; } else { total += 0; };


                    obj.SSC = dr["SSC"].ToString();
                    obj.HSC = dr["HSC"].ToString();
                    obj.UG = dr["UG"].ToString();            
                    obj.Graduation = dr["Graduation"].ToString();
                    obj.PG = dr["PG"].ToString();
                    if(obj.SSC != null && obj.SSC != "" && obj.SSC != "5" || obj.HSC != null && obj.HSC != "" && obj.HSC != "5" || obj.UG != null && obj.UG != "" && obj.UG != "5" || obj.Graduation != null && obj.Graduation != "" && obj.Graduation != "5" || obj.PG != null && obj.PG != "" && obj.PG != "5")
                    { total += 30; } else { total += 0; };


                    obj.EmploymentCurrent = dr["CurrentEmployement"].ToString();
                    if(obj.EmploymentCurrent != null && obj.EmploymentCurrent != "") { total += 20; } else { total += 0; };
                    obj.ProjectTitle = dr["ProjectTitle"].ToString();
                    if (obj.ProjectTitle != null && obj.ProjectTitle != "") { total += 20; } else { total += 0; };

                }
 
                dr.Close();
                ViewBag.Total = total;
                return await Task.Run(() => View(obj));
            }
            else
            {
                return await Task.Run(() => View("Register", "Account"));
            }
        }
        //------------------------------------Saurabh End---------------------------------//
        //------------------------------------Sanket Start-------------------------------//

        [HttpGet]
        public async Task<ActionResult> Search()
        {

            //string postJobCode = "PJC0001";
            //BALSeeker ObjBal = new BALSeeker();
            //DataSet ds = new DataSet();
            //ds = ObjBal.FindJobs(postJobCode);
            //SeekerUser objDetails = new SeekerUser();
            //objDetails.PostJobCode = postJobCode;
            //List<SeekerUser> lstUserDt1 = new List<SeekerUser>();
            //for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            //{
            //    SeekerUser obju = new SeekerUser();
            //    obju.JobTitle = ds.Tables[0].Rows[i]["JobTitle"].ToString();
            //    obju.JobLocation = ds.Tables[0].Rows[i]["JobLocation"].ToString();
            //    obju.SalaryRange = ds.Tables[0].Rows[i]["SalaryRange"].ToString();

            //    lstUserDt1.Add(obju);
            //}

            //objDetails.user = lstUserDt1;
            return await Task.Run(() => View());
        }
        [HttpPost]
        public async Task<ActionResult> Search(SeekerUser obj, string jobTitle, string jobLocation, string salary)

        {
            SeekerUser obj1 = new SeekerUser();

            obj.JobTitle = jobTitle;
            obj.Salary = salary;
            obj.JobLocation = jobLocation;
            BALSeeker ObjBal = new BALSeeker();
            DataSet ds = new DataSet();
            ds = ObjBal.FindJobs1(obj);
            SeekerUser objDetails = new SeekerUser();
            //objDetails.PostJobCode = "postJobCode";
            List<SeekerUser> lstUserDt1 = new List<SeekerUser>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                SeekerUser obju = new SeekerUser();
                obju.PostJobCode = ds.Tables[0].Rows[i]["PostJobCode"].ToString();
                obju.JobTitle = ds.Tables[0].Rows[i]["JobTitle"].ToString();
                obju.JobLocation = ds.Tables[0].Rows[i]["Location1"].ToString();
                obju.Salary = ds.Tables[0].Rows[i]["Salary"].ToString();
                obju.JobType = ds.Tables[0].Rows[i]["JobType"].ToString();
                obju.EndDate = ds.Tables[0].Rows[i]["ApplicationEndDate"].ToString();

                lstUserDt1.Add(obju);
            }

            objDetails.user = lstUserDt1;
            return await Task.Run(() => View(objDetails));
        }

        public async Task<ActionResult> SeekerSearch()
        {

            return View();
        }


        //-------------ApplyJobsDr--------------------------//

        [HttpGet]
        public async Task<ActionResult> ApplyJobs(SeekerUser obj, string postJobCode)
        {

            obj.PostJobCode = postJobCode;
            BALSeeker obj1 = new BALSeeker();
            SqlDataReader dr;
            dr = obj1.ApplyJobView(obj);
            while (dr.Read())
            {
                obj.PostJobCode = dr["PostJobCode"].ToString();
                obj.JobTitle = dr["JobTitle"].ToString();
                obj.CompanyName = dr["CompanyName"].ToString();
                obj.JobLocation = dr["Location1"].ToString();
                obj.JobType = dr["JobType"].ToString();
                obj.Salary = dr["Salary"].ToString();
                obj.JobDescription = dr["JobDescription"].ToString();
            }
            dr.Close();
            return await Task.Run(() => PartialView(obj));

        }

        //---------------SubmitPDF---------------------------------------//
        [HttpPost]
        public async Task<ActionResult> SubmitPDF(HttpPostedFileBase myFile)
        {

            SeekerUser obj = new SeekerUser();
            string seekercode = "JSC0001";
            obj.StatusId = 9;
            obj.Seekercode = seekercode;
            obj.AppliedDate = DateTime.Now;
            BALSeeker obj1 = new BALSeeker();


            if (myFile != null && myFile.ContentLength > 0)
            {

                string fileName = Path.GetFileName(myFile.FileName);
                string path = Server.MapPath("~/Content/PDF Files");
                string ResumePath = Path.Combine(path, fileName);
                myFile.SaveAs(ResumePath);
                obj.ResumePDF = fileName;
                obj1.SubmitPDF(obj);

                TempData["notice"] = "OK! The file is uploaded";

            }


            return await Task.Run(() => RedirectToAction("Search"));

        }

        //------------ApplicationGrid-----------------------------------------//

        [HttpGet]
        public async Task<ActionResult> ApplicationGrid(SeekerUser obj1)
        {
            string seekercode = "JSC0001";
            obj1.Seekercode = seekercode;
            SeekerUser objss = new SeekerUser();
            BALSeeker objUser = new BALSeeker();
            DataSet ds = new DataSet();
            ds = objUser.ApplicationGrid(obj1);
            SeekerUser objDetails = new SeekerUser();
            List<SeekerUser> LstUser = new List<SeekerUser>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                SeekerUser obj = new SeekerUser();
                //obj.Seekercode = ds.Tables[0].Rows[i]["Seekercode"].ToString();
                obj.JobTitle = ds.Tables[0].Rows[i]["JobTitle"].ToString();
                obj.AppliedJobID = Convert.ToInt32(ds.Tables[0].Rows[i]["AppliedJobID"].ToString());
                obj.AppliedDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["AppliedDate"].ToString());
                obj.CompanyName = ds.Tables[0].Rows[i]["CompanyName"].ToString();
                obj.ResumePDF = ds.Tables[0].Rows[i]["ResumePDF"].ToString();
                obj.Status = ds.Tables[0].Rows[i]["Status"].ToString();
                LstUser.Add(obj);
            }
            ViewBag.ResumePDF = obj1.ResumePDF;
            objDetails.user = LstUser;
            return await Task.Run(() => View(objDetails));
        }

        //-----------------DeleteApplication---------------------------------------//

        [HttpGet]
        public async Task<ActionResult> IsDelete(SeekerUser obj)
        {

            BALSeeker obj1 = new BALSeeker();
            obj1.IsDelete(obj);
            return await Task.Run(() => RedirectToAction("ApplicationGrid"));

        }

        //--------------------------SubmitApplication------------------------------//

        [HttpPost]
        public async Task<ActionResult> SubmitApplication(SeekerUser obj, int jobappliedid, int statusid)
        {
            SeekerUser objs = new SeekerUser();
            obj.AppliedJobID = jobappliedid;
            BALSeeker obj1 = new BALSeeker();
            SqlDataReader dr;
            dr = obj1.SubmitApplication(obj);
            while (dr.Read())
            {
                obj.StatusId = Convert.ToInt32(dr["StatusID"].ToString());
                obj.AppliedJobID = Convert.ToInt32(dr["AppliedJobID"].ToString());

            }
            dr.Close();
            return await Task.Run(() => PartialView("ApplyJobs"));
        }

        //--------------------DownloadPDF--------------------------//

        [HttpGet]
        public FileResult DownloadPDF(int seekerid)
        {

            SeekerUser obj = new SeekerUser();
            obj.SeekerId = seekerid;
            BALSeeker obj1 = new BALSeeker();
            SqlDataReader dr;
            dr = obj1.DownloadPDF(obj);

            while (dr.Read())
            {
                obj.SeekerId = Convert.ToInt32(dr["JobSeekerCode"].ToString());
            }
            dr.Close();

            List<SeekerUser> ObjFiles = new List<SeekerUser>(obj.SeekerId);

            var FileById = (from FC in ObjFiles
                            where FC.SeekerId.Equals(seekerid)
                            select new { FC.ResumePDF, FC.UploadFile }).ToList().FirstOrDefault();

            return File(FileById.UploadFile, "application/pdf", FileById.ResumePDF);

        }

        //------------------------------------Sanket End--------------------------------//
    }
}