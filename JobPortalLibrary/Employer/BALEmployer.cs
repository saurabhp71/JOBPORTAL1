using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace JobPortalLibrary.Employer
{
    public class BALEmployer
    {

        //SqlConnection con = new SqlConnection("Data Source=DESKTOP-B3UBKFN;Initial Catalog=\"Job Portal\";Integrated Security=True");
        static string CS = ConfigurationManager.ConnectionStrings["RSJP"].ConnectionString;
        SqlConnection con = new SqlConnection(CS);
        public void ManageConnection()
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            else
            {
                con.Close();
            }
        }

        //---------------------------------Mahesh Start-----------------------------//
        public DataSet getjobgrid(EmployerUser obj)
        {
            ManageConnection();
            SqlCommand cmd = new SqlCommand("Employeer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "getjobgrid");
            cmd.Parameters.AddWithValue("@EmployeerCode", obj.Employercode);
            SqlDataAdapter adpt = new SqlDataAdapter();
            adpt.SelectCommand = cmd;
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            return ds;
            

        }
        public DataSet getapplyjobstud(EmployerUser obj)
        {
            ManageConnection();
            SqlCommand cmd = new SqlCommand("Employeer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "applyjobstud");
            cmd.Parameters.AddWithValue("@PostJobCode", obj.PostJobCode);
            SqlDataAdapter adpt = new SqlDataAdapter();
            adpt.SelectCommand = cmd;
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            return ds;
            


        }
        public void getaprove(EmployerUser obj)
        {
            ManageConnection();
            SqlCommand cmd = new SqlCommand("Employeer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "getstatusaprove");
            cmd.Parameters.AddWithValue("@StatusId", obj.StatusId);
            cmd.Parameters.AddWithValue("@AppliedJobId", obj.AppliedJobId);
            cmd.ExecuteNonQuery();
            
        }
        public DataSet getstatus(EmployerUser obj)
        {
            ManageConnection();
            SqlCommand cmd = new SqlCommand("Employeer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "getstatus");
            cmd.Parameters.AddWithValue("@PostJobCode", obj.PostJobCode);
            cmd.Parameters.AddWithValue("@AppliedJobId", obj.AppliedJobId);
            SqlDataAdapter adpt = new SqlDataAdapter();
            DataSet dt = new DataSet();
            adpt.SelectCommand = cmd;
            adpt.Fill(dt);
            return dt;
            
        }
        public DataSet getactivecount(EmployerUser obj)
        {
            ManageConnection();
            SqlCommand cmd = new SqlCommand("Employeer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "getactivecount");
            cmd.Parameters.AddWithValue("@PostJobCode", obj.PostJobCode);
            SqlDataAdapter adpt = new SqlDataAdapter();
            adpt.SelectCommand = cmd;
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            return ds;
            

        }
        public DataSet getawaitingreveiwcount(EmployerUser obj)
        {
            ManageConnection();
            SqlCommand cmd = new SqlCommand("Employeer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "getawaitingreviewcount");
            cmd.Parameters.AddWithValue("@PostJobCode", obj.PostJobCode);
            SqlDataAdapter adpt = new SqlDataAdapter();
            adpt.SelectCommand = cmd;
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            return ds;
            

        }
        public DataSet getreviewcount(EmployerUser obj)
        {
            ManageConnection();
            SqlCommand cmd = new SqlCommand("Employeer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "getreviewcount");
            cmd.Parameters.AddWithValue("@PostJobCode", obj.PostJobCode);
            SqlDataAdapter adpt = new SqlDataAdapter();
            adpt.SelectCommand = cmd;
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            return ds;
            

        }
        public DataSet getcontactingcount(EmployerUser obj)
        {
            ManageConnection();
            SqlCommand cmd = new SqlCommand("Employeer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "getcontactingcount");
            cmd.Parameters.AddWithValue("@PostJobCode", obj.PostJobCode);
            SqlDataAdapter adpt = new SqlDataAdapter();
            adpt.SelectCommand = cmd;
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            return ds;
            

        }
        public DataSet gethirecount(EmployerUser obj)
        {
            ManageConnection();
            SqlCommand cmd = new SqlCommand("Employeer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "gethirecount");
            cmd.Parameters.AddWithValue("@PostJobCode", obj.PostJobCode);
            SqlDataAdapter adpt = new SqlDataAdapter();
            adpt.SelectCommand = cmd;
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            return ds;
            

        }
        public DataSet getrejectedcount(EmployerUser obj)
        {
            ManageConnection();
            SqlCommand cmd = new SqlCommand("Employeer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "getrejectcount");
            cmd.Parameters.AddWithValue("@PostJobCode", obj.PostJobCode);
            SqlDataAdapter adpt = new SqlDataAdapter();
            adpt.SelectCommand = cmd;
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            return ds;
            

        }
        public SqlDataReader JobDetails(EmployerUser obj)
        {
            ManageConnection();
            SqlCommand cmd = new SqlCommand("Employeer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "JobDetails");
            cmd.Parameters.AddWithValue("@PostJobCode", obj.PostJobCode);
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            return dr;
            
        }
        public DataSet getphonedetails(EmployerUser obj)
        {
            ManageConnection();
            SqlCommand cmd = new SqlCommand("Employeer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "Getphonedetails");
            cmd.Parameters.AddWithValue("@AppliedJobId", obj.AppliedJobId);
            SqlDataAdapter adpt = new SqlDataAdapter();
            adpt.SelectCommand = cmd;
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            return ds;
            

        }
        public void deletecandidates(EmployerUser obj)
        {
            ManageConnection();
            SqlCommand cmd = new SqlCommand("Employeer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "Deletecandidate");
            cmd.Parameters.AddWithValue("@Seekercode", obj.Seekercode);
            cmd.ExecuteNonQuery();
            

        }
        public DataSet geteducation()
        {
            ManageConnection();
            SqlCommand cmd = new SqlCommand("Employeer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "GetEducation");
            SqlDataAdapter adpt = new SqlDataAdapter();
            adpt.SelectCommand = cmd;
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            return ds;
            

        }
        public DataSet getdegree(EmployerUser obj)
        {
            ManageConnection();
            SqlCommand cmd = new SqlCommand("Employeer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "GetDegree");
            cmd.Parameters.AddWithValue("@QualificationId", obj.QualificationId);
            SqlDataAdapter adpt = new SqlDataAdapter();
            adpt.SelectCommand = cmd;
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            return ds;
            

        }
        public DataSet getspecialization(EmployerUser obj)
        {
            ManageConnection();
            SqlCommand cmd = new SqlCommand("Employeer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "GetSpecialization");
            cmd.Parameters.AddWithValue("@DegreeId", obj.DegreeId);
            SqlDataAdapter adpt = new SqlDataAdapter();
            adpt.SelectCommand = cmd;
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            return ds;
           

        }
        public DataSet getseekercode()
        {
            ManageConnection();
            SqlCommand cmd = new SqlCommand("Employeer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "Getseekercode");
            SqlDataAdapter adpt = new SqlDataAdapter();
            adpt.SelectCommand = cmd;
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            return ds;
            

        }
        public void Editjob(EmployerUser obj)
        {

            ManageConnection();
            SqlCommand cmd = new SqlCommand("Employeer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "EditJob");
            cmd.Parameters.AddWithValue("@PostJobCode", obj.PostJobCode);
            //cmd.Parameters.AddWithValue("@CompanyId", obj.CompanyId);
            cmd.Parameters.AddWithValue("@ContactPerson", obj.ContactPerson);
            cmd.Parameters.AddWithValue("@OpportunityType", obj.OpportunityType);
            cmd.Parameters.AddWithValue("@JobTitle", obj.JobTitle);
            // cmd.Parameters.AddWithValue("@JobCategoryId", obj.JobCategory);
            cmd.Parameters.AddWithValue("@JobDescription", obj.JobDescription);
            cmd.Parameters.AddWithValue("@JobLocation", obj.City);
            cmd.Parameters.AddWithValue("@Address", obj.Address);
            cmd.Parameters.AddWithValue("@JobType", obj.JobType);
            cmd.Parameters.AddWithValue("@RequireQualificationId", obj.RequireQualification);
            cmd.Parameters.AddWithValue("@NoofOpenings", obj.NoOfOpenings);
            cmd.Parameters.AddWithValue("@WorkingShifts", obj.WorkingShifts);
            cmd.Parameters.AddWithValue("@SalaryType", obj.SalaryType);
            cmd.Parameters.AddWithValue("@SalaryRange", obj.SalaryRange);
            cmd.Parameters.AddWithValue("@Salary", obj.Salary);
            cmd.Parameters.AddWithValue("@JobBenefits", obj.JobBenifit);
            cmd.Parameters.AddWithValue("@TotalExperience", obj.TotalExperience);
            cmd.Parameters.AddWithValue("@ExpectedJoiningDate", obj.ExpectedJoiningDate);
            cmd.Parameters.AddWithValue("@ApplicationStartDate", obj.ApplicationStartDate);
            cmd.Parameters.AddWithValue("@ApplicationEndDate", obj.ApplicationEndDate);
            cmd.ExecuteNonQuery();
            
        }
        public void saveseekerdetail(EmployerUser obj, string seekercode, string Resume)
        {
            con.Close();
            ManageConnection();
            SqlCommand cmd = new SqlCommand("Employeer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "Saveseekerdetail");
            cmd.Parameters.AddWithValue("@Seekercode", seekercode);
            cmd.Parameters.AddWithValue("@SeekerName", obj.FullName);
            cmd.Parameters.AddWithValue("@EmailId", obj.EmailId);
            cmd.Parameters.AddWithValue("@Password", obj.Password);
            cmd.Parameters.AddWithValue("@DOB", obj.DOB);
            cmd.Parameters.AddWithValue("@Address", obj.Address);
            cmd.Parameters.AddWithValue("@ResumePDF", Resume); 
            cmd.ExecuteNonQuery();

         

        }
        public void applyjob(EmployerUser obj, string seekercode, string Resume)
        {
            con.Close();
            ManageConnection();
            SqlCommand cmd = new SqlCommand("Employeer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "ApplyJob");
            cmd.Parameters.AddWithValue("@Seekercode", seekercode);
            cmd.Parameters.AddWithValue("@PostJobCode", obj.PostJobCode);
            cmd.Parameters.AddWithValue("@StatusId", obj.StatusId);
            cmd.Parameters.AddWithValue("@AppliedDate", DateTime.Now);
            cmd.Parameters.AddWithValue("@ResumePDF", Resume);
             cmd.Parameters.AddWithValue("@isDelete", obj.isDelete);
            cmd.ExecuteNonQuery();
            

        }
        public void savesscdetail(EmployerUser obj, string seekercode)
        {
            con.Close();
            ManageConnection();
            SqlCommand cmd = new SqlCommand("Employeer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "SaveSSCdetail");
            cmd.Parameters.AddWithValue("@Seekercode", seekercode);
            cmd.Parameters.AddWithValue("@SSC", obj.SSC);
            cmd.Parameters.AddWithValue("@SSCBoard", obj.SSCBoard);
            cmd.ExecuteNonQuery();
           
        }
        public void savehscdetail(EmployerUser obj, string seekercode)
        {
            ManageConnection();
            SqlCommand cmd = new SqlCommand("Employeer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "SaveHSCdetail");
            cmd.Parameters.AddWithValue("@Seekercode", seekercode);
            cmd.Parameters.AddWithValue("@HSC", obj.HSC);
            cmd.Parameters.AddWithValue("@HSCBoard", obj.HSCBoard);
            cmd.ExecuteNonQuery();
           
        }
        public void saveundergraduation(EmployerUser obj, string seekercode)
        {
            con.Close();
            ManageConnection();
            SqlCommand cmd = new SqlCommand("Employeer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "SaveUGDetail");
            cmd.Parameters.AddWithValue("@Seekercode", seekercode);
            cmd.Parameters.AddWithValue("@UG", obj.QualificationId);
            cmd.Parameters.AddWithValue("@UGDegree", obj.DegreeId);
            cmd.Parameters.AddWithValue("@UGSpecialization", obj.SpecializationId);
            cmd.Parameters.AddWithValue("@UGUnivercity", obj.Univercity);
            cmd.ExecuteNonQuery();
            
        }
        public void savegraduationdetail(EmployerUser obj, string seekercode)
        {
            con.Close();
            ManageConnection();
            SqlCommand cmd = new SqlCommand("Employeer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "SaveGraduationDetail");
            cmd.Parameters.AddWithValue("@Seekercode", seekercode);
            cmd.Parameters.AddWithValue("@Graduation", obj.QualificationId);
            cmd.Parameters.AddWithValue("@GraduationDegree", obj.DegreeId);
            cmd.Parameters.AddWithValue("@GraduationSpecialization", obj.SpecializationId);
            cmd.Parameters.AddWithValue("@GraduationUnivercity", obj.Univercity);
            cmd.ExecuteNonQuery();
            
        }
        public void savepgdetail(EmployerUser obj, string seekercode)
        {
            con.Close();
            ManageConnection();
            SqlCommand cmd = new SqlCommand("Employeer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "SavePGDetail");
            cmd.Parameters.AddWithValue("@Seekercode", seekercode);
            cmd.Parameters.AddWithValue("@PG", obj.QualificationId);
            cmd.Parameters.AddWithValue("@PGDegree", obj.DegreeId);
            cmd.Parameters.AddWithValue("@PGSpecialization", obj.SpecializationId);
            cmd.Parameters.AddWithValue("@PGUnivercity", obj.Univercity);
            cmd.ExecuteNonQuery();
            
        }
        
        //---------------------------------Mahesh end-----------------------------//
        //----------------------------------Mitali Start-----------------------------------------//

        public void JobRegister(EmployerUser ObjEmployerUser)
        {
            ManageConnection();
            SqlCommand cmd = new SqlCommand("Employeer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "JobRegister");
            cmd.Parameters.AddWithValue("@PostJobCode", ObjEmployerUser.PostJobCode);
            cmd.Parameters.AddWithValue("@CompanyId", ObjEmployerUser.CompanyId);
            cmd.Parameters.AddWithValue("@ContactPerson", ObjEmployerUser.ContactPerson);
            cmd.Parameters.AddWithValue("@OpportunityType", ObjEmployerUser.OpportunityType);
            cmd.Parameters.AddWithValue("@JobTitle", ObjEmployerUser.JobTitle);
            cmd.Parameters.AddWithValue("@JobCategoryId", ObjEmployerUser.JobCategory);
            cmd.Parameters.AddWithValue("@JobDescription", ObjEmployerUser.JobDescription);
            cmd.Parameters.AddWithValue("@JobLocation", ObjEmployerUser.City);
            cmd.Parameters.AddWithValue("@Address", ObjEmployerUser.Address);
            cmd.Parameters.AddWithValue("@JobType", ObjEmployerUser.JobType);
            cmd.Parameters.AddWithValue("@RequireQualificationId", ObjEmployerUser.RequireQualification);
            cmd.Parameters.AddWithValue("@NoofOpenings", ObjEmployerUser.NoOfOpenings);
            cmd.Parameters.AddWithValue("@WorkingShifts", ObjEmployerUser.WorkingShifts);
            cmd.Parameters.AddWithValue("@SalaryType", ObjEmployerUser.SalaryType);
            cmd.Parameters.AddWithValue("@SalaryRange", ObjEmployerUser.SalaryRange);
            cmd.Parameters.AddWithValue("@Salary", ObjEmployerUser.Salary);
            cmd.Parameters.AddWithValue("@JobBenefits", ObjEmployerUser.JobBenifit);
            cmd.Parameters.AddWithValue("@TotalExperience", ObjEmployerUser.TotalExperience);
            cmd.Parameters.AddWithValue("@ExpectedJoiningDate", ObjEmployerUser.ExpectedJoiningDate);
            cmd.Parameters.AddWithValue("@ApplicationStartDate", ObjEmployerUser.ApplicationStartDate);
            cmd.Parameters.AddWithValue("@ApplicationEndDate", ObjEmployerUser.ApplicationEndDate);
            cmd.ExecuteNonQuery();
            

        }
        public SqlDataReader PostJobCode()
        {
            ManageConnection();
            SqlCommand cmd = new SqlCommand("Employeer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Flag", "PostJobCode");
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            return dr;
           

        }
        public DataSet GetCompany(EmployerUser obj)                                 // regular dropdown
        {
            ManageConnection();
            SqlCommand cmd = new SqlCommand("Employeer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "Company");
            cmd.Parameters.AddWithValue("@EmployeerCode", obj.Employercode);
            SqlDataAdapter adpt = new SqlDataAdapter();
            adpt.SelectCommand = cmd;
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            return ds;
        }
        public DataSet GetJobBenifits()                           // multiselect dropdown
        {
            ManageConnection();
            SqlCommand cmd = new SqlCommand("Employeer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "JobBenefits");
            SqlDataAdapter adpt = new SqlDataAdapter();
            adpt.SelectCommand = cmd;
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            return ds;
        }
        public DataSet GetJobCategory(int companyid)                                 // regular dropdown
        {
            ManageConnection();
            SqlCommand cmd = new SqlCommand("Employeer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "JobCategory");
            cmd.Parameters.AddWithValue("@CompanyId", companyid);
            SqlDataAdapter adpt = new SqlDataAdapter();
            adpt.SelectCommand = cmd;
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            return ds;
        }
        public DataSet GetLocation()
        {
            ManageConnection();
            SqlCommand cmd = new SqlCommand("Employeer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "JobLocation");
            SqlDataAdapter adpt = new SqlDataAdapter();
            adpt.SelectCommand = cmd;
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            return ds;
        }
        public DataSet QualificationRequired()                           // multiselect dropdown
        {
            ManageConnection();
            SqlCommand cmd = new SqlCommand("Employeer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "QualificationRequired");
            SqlDataAdapter adpt = new SqlDataAdapter();
            adpt.SelectCommand = cmd;
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            return ds;
        }

        //---------------------------------Mitali End----------------------------//
        //---------------------------------Kartik Start--------------------------//
        public void KTCompanyRegisteration(EmployerUser ObjEmployerUser)
        {
            ManageConnection();
            SqlCommand cmd = new SqlCommand("Employeer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Flag", "KTCompanyRegisteration");
            cmd.Parameters.AddWithValue("@EmployeerCode", ObjEmployerUser.Employercode);
            cmd.Parameters.AddWithValue("@CompanyName", ObjEmployerUser.CompanyName);
            cmd.Parameters.AddWithValue("@NoOfEmployees", ObjEmployerUser.NoOfEmployees);
            cmd.Parameters.AddWithValue("@ContactNo", ObjEmployerUser.ContactNo);
            cmd.Parameters.AddWithValue("@CompanyWebsite", ObjEmployerUser.CompanyWebsite);
            cmd.Parameters.AddWithValue("@CompanyEmail", ObjEmployerUser.CompanyEmail);
            cmd.Parameters.AddWithValue("@AboutCompany", ObjEmployerUser.AboutCompany);
            cmd.Parameters.AddWithValue("@IndustryId", ObjEmployerUser.IndustryId);
            cmd.Parameters.AddWithValue("@CityId", ObjEmployerUser.CityId);
            cmd.Parameters.AddWithValue("@PinCode", ObjEmployerUser.Pincode);
            cmd.Parameters.AddWithValue("@CompanyLogo", ObjEmployerUser.CompanyLogo);
            cmd.Parameters.AddWithValue("@PortFolio", ObjEmployerUser.Porfolio);
            cmd.Parameters.AddWithValue("@Slogan", ObjEmployerUser.Slogan);
            cmd.Parameters.AddWithValue("@Facebook", ObjEmployerUser.Facebook);
            cmd.Parameters.AddWithValue("@Twitter", ObjEmployerUser.Twitter);
            cmd.Parameters.AddWithValue("@LinkedIn", ObjEmployerUser.LinkedIn);
            cmd.Parameters.AddWithValue("@Instagram", ObjEmployerUser.Instagram);
            cmd.Parameters.AddWithValue("@Google", ObjEmployerUser.Google);
            cmd.Parameters.AddWithValue("@OwnerName", ObjEmployerUser.OwnerName);
            cmd.Parameters.AddWithValue("@HRName", ObjEmployerUser.HRName);
            cmd.Parameters.AddWithValue("@HRNumber", ObjEmployerUser.HRNumber);
            cmd.Parameters.AddWithValue("@HREmail", ObjEmployerUser.HREmail);
            cmd.Parameters.AddWithValue("@Source", ObjEmployerUser.Source);
            cmd.Parameters.AddWithValue("@RegistrationDate", ObjEmployerUser.RegistrationDate);
            cmd.ExecuteNonQuery();
            
        }


        public DataSet KTCompanyCategoryBind()
        {

            ManageConnection();
            SqlCommand cmd = new SqlCommand("Employeer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Flag", "KTCompanyCategoryBind");
            SqlDataAdapter adpt = new SqlDataAdapter();
            adpt.SelectCommand = cmd;
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            return ds;
            
        }

        public DataSet Locationbind()
        {
            ManageConnection();
            SqlCommand cmd = new SqlCommand("Employeer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Flag", "Locationbind");
            SqlDataAdapter adpt = new SqlDataAdapter();
            adpt.SelectCommand = cmd;
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            return ds;
            

        }


        //---------------------------------Kartik End----------------------------//
        //---------------------------------sachin start----------------------------//
        public DataSet GetJobCategory()
        {
            ManageConnection();
            SqlCommand cmb = new SqlCommand("Employeer", con);
            cmb.CommandType = CommandType.StoredProcedure;
            cmb.Parameters.AddWithValue("@flag", "GetJobCategory");
            SqlDataAdapter adpt = new SqlDataAdapter();
            adpt.SelectCommand = cmb;
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            return ds;
            
        }
            public DataSet JobCategory()
            {
            ManageConnection();
            SqlCommand cmd = new SqlCommand("Employeer", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@flag", "JobCategory");
                SqlDataAdapter adpt = new SqlDataAdapter();
                adpt.SelectCommand = cmd;
                DataSet ds = new DataSet();
                adpt.Fill(ds);
                return ds;

            }
            public DataSet GetJobTitle(int JobCategoryId)
        {
            ManageConnection();
            SqlCommand cmb = new SqlCommand("Employeer", con);
            cmb.CommandType = CommandType.StoredProcedure;
            cmb.Parameters.AddWithValue("@Flag", "GetJobTitle");
            cmb.Parameters.AddWithValue("@JobCatagoryId", JobCategoryId);
            SqlDataAdapter adpt = new SqlDataAdapter();
            adpt.SelectCommand = cmb;
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            return ds;
            
        }
        public DataSet GetJobLocation()
        {
            ManageConnection();
            SqlCommand cmb = new SqlCommand("Employeer", con);
            cmb.CommandType = CommandType.StoredProcedure;
            cmb.Parameters.AddWithValue("@Flag", "GetJobLocation");
            SqlDataAdapter adpt = new SqlDataAdapter();
            adpt.SelectCommand = cmb;
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            return ds;
            
        }
        public DataSet GetFindResume(int CityId, string JobTitle)
        {
            ManageConnection();
            SqlCommand cmd = new SqlCommand("Employeer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Flag", "GetFindResume");
            cmd.Parameters.AddWithValue("@CityId", CityId);
            cmd.Parameters.AddWithValue("@JobTitle", JobTitle);
            SqlDataAdapter adpt = new SqlDataAdapter();
            adpt.SelectCommand = cmd;
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            return ds;
            
        }

        //public SqlDataReader JobSeekerDetails(EmployerUser obj)
        //{
        //    ManageConnection();
        //    SqlCommand cmd = new SqlCommand("Employeer", con);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.AddWithValue("@flag", "JobSeekerDetails");
        //    cmd.Parameters.AddWithValue("@Seekercode", obj.Seekercode);
        //    SqlDataReader dr;
        //    dr = cmd.ExecuteReader();
        //    return dr;
            
        //}
        public DataSet JobSeekerDetails(EmployerUser obj)
        {
            ManageConnection();
            SqlCommand cmd = new SqlCommand("Employeer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Flag", "JobSeekerDetails");
            cmd.Parameters.AddWithValue("@Seekercode", obj.Seekercode);
            
            SqlDataAdapter adpt = new SqlDataAdapter();
            adpt.SelectCommand = cmd;
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            return ds;

        }
        public void Update(EmployerUser obj)
        {
            ManageConnection();
            SqlCommand cmd = new SqlCommand("Employeer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "Update");
            cmd.Parameters.AddWithValue("@StatusId", obj.StatusId);
            cmd.Parameters.AddWithValue("@Seekercode", obj.Seekercode);
            cmd.ExecuteNonQuery();
            
        }
        public DataSet ResumeAlert(EmployerUser objseeker)
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();

            }
            SqlCommand cmd = new SqlCommand("Employeer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "ResumeAlert");
            cmd.Parameters.AddWithValue("@EmployeerCode", objseeker.Employercode);
            SqlDataAdapter adpt = new SqlDataAdapter();
            adpt.SelectCommand = cmd;
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            return ds;
            con.Close();
        }
        //---------------------------------sachin End----------------------------//
        //---------------------------------Ashish start----------------------------//
        public DataSet AllRoundsGrid(EmployerUser objuser)
        {
            ManageConnection();
            SqlCommand cmd = new SqlCommand("Employeer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "AllRoundsGrid");
            cmd.Parameters.AddWithValue("@PostJobCode", objuser.PostJobCode);
            SqlDataAdapter adpt = new SqlDataAdapter();
            adpt.SelectCommand = cmd;
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            return ds;
            

        }
        public DataSet RoundsGrid(EmployerUser objuser)
        {
            ManageConnection();
            SqlCommand cmd = new SqlCommand("Employeer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "AORoundsGrid1");
            cmd.Parameters.AddWithValue("@PostJobCode", objuser.PostJobCode);
            SqlDataAdapter adpt = new SqlDataAdapter();
            adpt.SelectCommand = cmd;
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            return ds;

        }
        public void AddNewRounds(EmployerUser objuser)
        {
            ManageConnection();
            SqlCommand cmd = new SqlCommand("Employeer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "AOAddNewRounds");
            cmd.Parameters.AddWithValue("@EmployeerCode", objuser.Employercode);
            cmd.Parameters.AddWithValue("@RoundName", objuser.RoundName);
            cmd.ExecuteNonQuery();

        }

        public DataSet ShowRounds()
        {
            ManageConnection();
            SqlCommand cmd = new SqlCommand("Employeer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "AOShowRounds");
            SqlDataAdapter adpt = new SqlDataAdapter();
            adpt.SelectCommand = cmd;
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            return ds;

        }

        public void CreateRoundwithDetails(EmployerUser objuser)
        {
            con.Close();
            ManageConnection();
            SqlCommand cmd = new SqlCommand("Employeer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "CreateRoundwithDetails");
            cmd.Parameters.AddWithValue("@RoundName", objuser.RoundName);
            cmd.Parameters.AddWithValue("@applyjoblist", objuser.Applycodelist);
            cmd.Parameters.AddWithValue("@StartTime", objuser.StartTime);
            cmd.Parameters.AddWithValue("@EndTime", objuser.EndTime);
            cmd.Parameters.AddWithValue("@InterviewAddress", objuser.InterviewAddress);
            cmd.Parameters.AddWithValue("@Date", objuser.InterviewDate);
            cmd.Parameters.AddWithValue("@Comment", objuser.Comment);
            cmd.Parameters.AddWithValue("@InterviewType", objuser.InterviewType);
            cmd.Parameters.AddWithValue("@StatusId", objuser.StatusId);
            cmd.ExecuteNonQuery();

        }

        public void UpdateRound1(int AppliedJobId, EmployerUser objuser)
        {
            con.Close();
            ManageConnection();
            SqlCommand cmd = new SqlCommand("Employeer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "UpdateStatusR1Clear");
            cmd.Parameters.AddWithValue("@StatusId", objuser.StatusId);
            cmd.Parameters.AddWithValue("@AppliedJobId", AppliedJobId);

            cmd.ExecuteNonQuery();

        }
        public DataSet RoundsGridR2(EmployerUser objuser)
        {
            ManageConnection();
            SqlCommand cmd = new SqlCommand("Employeer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "AORoundsGridR2");
            cmd.Parameters.AddWithValue("@PostJobCode", objuser.PostJobCode);
            SqlDataAdapter adpt = new SqlDataAdapter();
            adpt.SelectCommand = cmd;
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            return ds;

        }
        public DataSet RoundsGridR3(EmployerUser objuser)
        {
            ManageConnection();
            SqlCommand cmd = new SqlCommand("Employeer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "AORoundsGridR3");
            cmd.Parameters.AddWithValue("@PostJobCode", objuser.PostJobCode);
            SqlDataAdapter adpt = new SqlDataAdapter();
            adpt.SelectCommand = cmd;
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            return ds;

        }
        public DataSet RoundsGridR4(EmployerUser objuser)
        {
            ManageConnection();
            SqlCommand cmd = new SqlCommand("Employeer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "AORoundsGridR4");
            cmd.Parameters.AddWithValue("@PostJobCode", objuser.PostJobCode);
            SqlDataAdapter adpt = new SqlDataAdapter();
            adpt.SelectCommand = cmd;
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            return ds;

        }
        public DataSet RoundsGridR5(EmployerUser objuser)
        {
            ManageConnection();
            SqlCommand cmd = new SqlCommand("Employeer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "AORoundsGridR5");
            cmd.Parameters.AddWithValue("@PostJobCode", objuser.PostJobCode);
            SqlDataAdapter adpt = new SqlDataAdapter();
            adpt.SelectCommand = cmd;
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            return ds;

        }
        //public SqlDataReader GetEmail(int AppliedJobId)
        //{
        //    ManageConnection();
        //    SqlCommand cmd = new SqlCommand("Employeer", con);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.AddWithValue("@flag", "GetEmail");
        //    cmd.Parameters.AddWithValue("@AppliedJobId", AppliedJobId);
        //    SqlDataReader dr;
        //    dr = cmd.ExecuteReader();
        //    return dr;

        //}
        public DataSet GetEmail(int AppliedJobId)
        {
            ManageConnection();
            SqlCommand cmd = new SqlCommand("Employeer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "GetEmail");
            cmd.Parameters.AddWithValue("@AppliedJobId", AppliedJobId);
            SqlDataAdapter adpt = new SqlDataAdapter();
            adpt.SelectCommand = cmd;
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            return ds;

        }
        public DataSet GetCompanyName(int AppliedJobId)
        {
            ManageConnection();
            SqlCommand cmd = new SqlCommand("Employeer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "GetCompanyName");
            cmd.Parameters.AddWithValue("@AppliedJobId", AppliedJobId);
            SqlDataAdapter adpt = new SqlDataAdapter();
            adpt.SelectCommand = cmd;
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            return ds;

        }
        //public SqlDataReader GetCompanyName(int AppliedJobId)
        //{
        //    ManageConnection();
        //    SqlCommand cmd = new SqlCommand("Employeer", con);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.AddWithValue("@flag", "GetCompanyName");
        //    cmd.Parameters.AddWithValue("@AppliedJobId", AppliedJobId);
        //    SqlDataReader dr;
        //    dr = cmd.ExecuteReader();
        //    return dr;

        //}
        //---------------------------------Ashish End----------------------------//
        //---------------------------------Muskan Start----------------------------//
        public DataSet Fetchemp(string employercode)
        {
            ManageConnection();
            SqlCommand cmd = new SqlCommand("Employeer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "fetchEmp");
            cmd.Parameters.AddWithValue("@EmployeerCode", employercode);
            SqlDataAdapter adpt = new SqlDataAdapter();
            adpt.SelectCommand = cmd;
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            return ds;

        }
        //public SqlDataReader Fetchemp(string employercode)
        //{
        //    con.Open();
        //    SqlCommand cmd = new SqlCommand("Employeer", con);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.AddWithValue("@flag", "fetchEmp");
        //    cmd.Parameters.AddWithValue("@EmployeerCode", employercode);
        //    SqlDataReader dr;
        //    dr = cmd.ExecuteReader();
        //    return dr;
        //    con.Close();
        //}
        //--------------------EmployerUpdate-------------------------------------------------
        public void Updateemployer(string Employercode, string EmailId, Int64 ContactNo, string Password)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Employeer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "UpdateEmployer");
            cmd.Parameters.AddWithValue("@EmployeerCode", Employercode);
            cmd.Parameters.AddWithValue("@EmailId", EmailId);
            cmd.Parameters.AddWithValue("@ContactNo", ContactNo);
            cmd.Parameters.AddWithValue("@Password", Password);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        //--------------------Isdeleteemployer-------------------------------------------------
        public void IsDeleteEmployer(string Employercode)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Employeer", con);
            cmd.Parameters.AddWithValue("@flag", "DeleteEmp");
            //cmd.Parameters.AddWithValue("@isDelete", isDelete);
            cmd.Parameters.AddWithValue("@EmployeerCode", Employercode);
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}





