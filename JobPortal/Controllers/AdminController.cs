using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using JobPortalLibrary.Admin;
using JobPortalLibrary.JobSeeker;
using System.Threading.Tasks;

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

        ///------------AdminDashbord-------------------///
        public ActionResult view()
        {
            AdminUser obj = new AdminUser();
            BALAdmin obj1 = new BALAdmin();
            SqlDataReader dr;
            dr = obj1.RPTotalJobsPosted();
            while (dr.Read())
            {
                @ViewBag.count = dr["ApprovalJobs"].ToString();
            }
            dr.Close();

            BALAdmin obj2 = new BALAdmin();
            SqlDataReader dt;
            dt = obj2.RPTotalSeekerRegister();
            while (dt.Read())
            {
                @ViewBag.Seeker = dt["seekerRegister"].ToString();
            }
            dt.Close();

            BALAdmin obj3 = new BALAdmin();
            SqlDataReader de;
            de = obj3.RPTotalEmployerRegister();
            while (de.Read())
            {
                @ViewBag.Employer = de["Employer"].ToString();
            }
            de.Close();

            BALAdmin obj4 = new BALAdmin();
            SqlDataReader da;
            da = obj4.RPTotalApplicationAppvalAndReject();
            while (da.Read())
            {
                @ViewBag.Application = da["Applications"].ToString();
            }
            da.Close();
            return View();
        }

        //----------------------------------Admin Jov Application---------------------------------------------/

        [HttpGet]
        public async Task<ActionResult> ApplicationApprovel()
        {
            BALAdmin obj = new BALAdmin();
            DataSet ds = new DataSet();
            ds = obj.RPJobStatusApprovle();
            AdminUser objuser = new AdminUser();
            List<AdminUser> users1 = new List<AdminUser>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                AdminUser obju = new AdminUser();
                obju.PostJobCode = ds.Tables[0].Rows[i]["PostJobCode"].ToString();
                obju.JobTitle = ds.Tables[0].Rows[i]["JobTitle"].ToString();
                obju.JobDescription = ds.Tables[0].Rows[i]["JobDescription"].ToString();
                DateTime date = Convert.ToDateTime(ds.Tables[0].Rows[i]["ApplicationStartDate"].ToString());
                obju.Rdate = date.ToShortDateString();

                //obju.ApplicationStartDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["ApplicationStartDate"].ToString());
                //obju.StatusId = Convert.ToInt32(ds.Tables[0].Rows[i]["StatusId"].ToString());
                obju.Status = ds.Tables[0].Rows[i]["Status"].ToString();

                users1.Add(obju);
            }
            objuser.Users = users1;
            return await Task.Run(() => View(objuser));
        }

        public ActionResult Update(AdminUser obj, string PostJobCode)
        {
            obj.StatusId = 9;
            //obj.PostJobCode = PostJobCode;
            //if (ViewBag.Approve == true)
            //{
            //    obj.StatusId = 9;
            //}
            //else
            //{
            //    obj.StatusId = 10;
            //}
            BALAdmin obj1 = new BALAdmin();
            obj1.RPUpdateJobStatusApprovle(obj);
            return RedirectToAction("ApplicationApprovel");
        }
        public ActionResult UpdateReject(AdminUser obj)
        {
            obj.StatusId = 10;

            BALAdmin obj1 = new BALAdmin();
            obj1.RPUpdateJobStatusApprovle(obj);
            return RedirectToAction("ApplicationApprovel");
        }


        [HttpGet]
        public async Task<ActionResult> Details(string PostJobCode)
        {
            AdminUser obj = new AdminUser();
            obj.PostJobCode = PostJobCode;
            BALAdmin obj1 = new BALAdmin();
            SqlDataReader dr;
            dr = obj1.RPJobDetails(obj);
            while (dr.Read())
            {
                obj.PostJobCode = dr["PostJobCode"].ToString();
                obj.JobTitle = dr["JobTitle"].ToString();
                obj.JobDescription = dr["JobDescription"].ToString();
                obj.ApplicationStartDate = Convert.ToDateTime(dr["ApplicationStartDate"].ToString());
                //obju.StatusId = Convert.ToInt32(ds.Tables[0].Rows[i]["StatusId"].ToString());
                obj.Status = dr["Status"].ToString();

            }
            dr.Close();
            return await Task.Run(() => PartialView(obj));

        }
        [HttpGet]
        public async Task<ActionResult> RejectionReason(string PostJobCode)
        {
            AdminUser obj = new AdminUser();
            obj.PostJobCode = PostJobCode;
            BALAdmin obj1 = new BALAdmin();
            SqlDataReader dr;
            dr = obj1.RPJobDetails(obj);
            while (dr.Read())
            {
                obj.PostJobCode = dr["PostJobCode"].ToString();
                obj.JobTitle = dr["JobTitle"].ToString();
                obj.JobDescription = dr["JobDescription"].ToString();
                obj.ApplicationStartDate = Convert.ToDateTime(dr["ApplicationStartDate"].ToString());
                //obju.StatusId = Convert.ToInt32(ds.Tables[0].Rows[i]["StatusId"].ToString());
                obj.Status = dr["Status"].ToString();

            }
            dr.Close();
            return await Task.Run(() => PartialView(obj));

        }
        //[HttpGet]
        //public ActionResult RejectionReason1()
        //{
        //    return View();
        //}
        [HttpPost]
        public ActionResult RejectionReason1(AdminUser obj)
        {
            obj.StatusId = 10;
            BALAdmin obj1 = new BALAdmin();
            obj1.RPUpdatejobRejectionReason(obj);
            return RedirectToAction("ApplicationApprovel");
        }

        //-----------------------------------Admin Company Reviews-----------------------------------------------------------/
        [HttpGet]
        public async Task<ActionResult> RPCompanyGridview()
        {
            //AdminUser obj1 = new AdminUser();
            //obj1.ReviewId = reviewId;
            BALAdmin obj = new BALAdmin();
            DataSet ds = new DataSet();
            ds = obj.RPCompanyGridview();
            AdminUser objuser = new AdminUser();
            List<AdminUser> users1 = new List<AdminUser>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                AdminUser obju = new AdminUser();
                obju.ReviewId = Convert.ToInt32(ds.Tables[0].Rows[i]["ReviewId"].ToString());
                obju.CompanyName = ds.Tables[0].Rows[i]["CompanyName"].ToString();
                DateTime date = Convert.ToDateTime(ds.Tables[0].Rows[i]["RegistrationDate"].ToString());
                obju.Rdate = date.ToShortDateString();
                obju.AboutCompany = ds.Tables[0].Rows[i]["AboutCompany"].ToString();
                obju.Rating = ds.Tables[0].Rows[i]["Rating"].ToString();
                obju.Review = ds.Tables[0].Rows[i]["Review"].ToString();
                obju.Status = ds.Tables[0].Rows[i]["Status"].ToString();

                users1.Add(obju);
            }
            objuser.Users = users1;
            return await Task.Run(() => View(objuser));
        }
        [HttpGet]
        public ActionResult RPCompanyIsDelete(AdminUser obj, int ReviewId)
        {
            // obj.isDelete = 1;
            obj.ReviewId = ReviewId;
            BALAdmin obj1 = new BALAdmin();
            obj1.RPCompanyIsDelete(obj);
            return RedirectToAction("RPCompanyGridview");
        }
        public ActionResult RPCompanyReviewStatusApprove(AdminUser obj)
        {
            obj.StatusId = 9;

            BALAdmin obj1 = new BALAdmin();
            obj1.RPCompanyReviewStatusUpdate(obj);
            return RedirectToAction("RPCompanyGridview");
        }
        public ActionResult RPCompanyReviewStatusReject(AdminUser obj)
        {
            obj.StatusId = 10;

            BALAdmin obj1 = new BALAdmin();
            obj1.RPCompanyReviewStatusUpdate(obj);
            return RedirectToAction("RPCompanyGridview");
        }
        [HttpGet]
        public async Task<ActionResult> RPCompanyGridviewDetails(int ReviewId)
        {
            AdminUser obj = new AdminUser();
            obj.ReviewId = ReviewId;
            BALAdmin obj1 = new BALAdmin();
            SqlDataReader dr;
            dr = obj1.RPCompanyGridviewDetails(obj);
            while (dr.Read())
            {
                obj.ReviewId = Convert.ToInt32(dr["ReviewId"].ToString());
                obj.CompanyName = dr["CompanyName"].ToString();
                DateTime date = Convert.ToDateTime(dr["RegistrationDate"].ToString());
                obj.Rdate = date.ToShortDateString();
                //obj.RegistrationDate = Convert.ToDateTime(dr["RegistrationDate"].ToString());
                obj.AboutCompany = dr["AboutCompany"].ToString();
                obj.Rating = dr["Rating"].ToString();
                obj.Review = dr["Review"].ToString();
                obj.Status = dr["Status"].ToString();

            }
            dr.Close();
            return await Task.Run(() => PartialView(obj));

        }

        // --------------------Admin--Employers-----------//

        public async Task<ActionResult> RPAdminEmployeGrid()
        {
            //var list = new List<string>() { "Active","Inactive","Hold" };
            //ViewBag.list = list;
            // dropdownlist();
            RPGetStatus();
            BALAdmin obj = new BALAdmin();
            DataSet ds = new DataSet();
            ds = obj.RPAdminEmployeGrid();
            AdminUser objuser = new AdminUser();
            List<AdminUser> users1 = new List<AdminUser>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                AdminUser obju = new AdminUser();
                obju.Employercode = ds.Tables[0].Rows[i]["EmployerCode"].ToString();
                obju.EmployerName = ds.Tables[0].Rows[i]["EmployerName"].ToString();
                obju.PaymentMode = ds.Tables[0].Rows[i]["PaymentMode"].ToString();
                obju.Plans = ds.Tables[0].Rows[i]["Plans"].ToString();
                string date = (ds.Tables[0].Rows[i]["SubscriptionDate"].ToString());
                DateTime SubscriptionD = Convert.ToDateTime(date);
                obju.SubscriptionDate = Convert.ToDateTime(SubscriptionD.ToShortDateString());
                //DateTime date = Convert.ToDateTime(ds.Tables[0].Rows[i]["SubscriptionDate"].ToString());
                //obju.Rdate = date.ToShortDateString();
                obju.PlanDuration = ds.Tables[0].Rows[i]["PlanDuration"].ToString();
                obju.Status = ds.Tables[0].Rows[i]["Status"].ToString();

                users1.Add(obju);
            }
            objuser.Users = users1;
            //ViewBag.Status = new List<SelectListItem>()
            //{
            //    new SelectListItem(){Value="Select",Text="Select"},
            //    new SelectListItem(){Value="Active",Text="Active"},
            //    new SelectListItem(){Value="InActive",Text="InActive"},
            //     new SelectListItem(){Value="Hold",Text="Hold"}
            //};
            return await Task.Run(() => View(objuser));
        }
        public ActionResult dropdownlist()
        {


            //var items = new List<string>(){ "Active","Inactive","Hold"};
            //ViewBag.Status = new List<SelectListItem>()
            //{
            //    new SelectListItem(){Value= null,Text="Select"},
            //    new SelectListItem(){Value="Active",Text="Active"},
            //    new SelectListItem(){Value="InActive",Text="InActive"},
            //     new SelectListItem(){Value="Hold",Text="Hold"}
            //};

            //List<SelectListItem> items = new List<SelectListItem>();

            //items.Add(new SelectListItem { Text = "Select", Value = "Select" });
            //items.Add(new SelectListItem { Text = "Active", Value = "Active" });
            //items.Add(new SelectListItem { Text = "In Active", Value = "In Active" });
            //items.Add(new SelectListItem { Text = "Hold", Value = "Hold" });



            return View();
        }
        public void RPGetStatus()
        {
            BALAdmin objbal = new BALAdmin();
            DataSet ds = new DataSet();
            ds = objbal.RPGetStatus();
            List<SelectListItem> countryList = new List<SelectListItem>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                countryList.Add(new SelectListItem
                {
                    Text = ds.Tables[0].Rows[i][1].ToString(),
                    Value = ds.Tables[0].Rows[i][0].ToString()
                });
                ViewBag.slist = countryList;

            }
        }

        public ActionResult EmployerStatus(AdminUser obj)
        {
            if (obj.StatusId == 1)
            {
                BALAdmin obj1 = new BALAdmin();
                obj1.RPPaymentStatusUpdate(obj);
            }
            if (obj.StatusId == 2)
            {
                BALAdmin obj1 = new BALAdmin();
                obj1.RPPaymentStatusUpdate(obj);
            }
            if (obj.StatusId == 3)
            {
                BALAdmin obj1 = new BALAdmin();
                obj1.RPPaymentStatusUpdate(obj);
            }

            return RedirectToAction("RPAdminEmployeGrid");
            //BALAdmin obj1 = new BALAdmin();
            //obj1.RPPaymentStatusUpdate(obj.Employercode,obj.)
            //return View();
        }
    }
}