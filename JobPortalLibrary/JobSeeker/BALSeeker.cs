using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using JobPortalLibrary.JobSeeker;
using System.Runtime.Remoting.Messaging;

namespace JobPortalLibrary.JobSeeker
{
    public class BALSeeker
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-RU5490M;Initial Catalog=\"Job Portal\";Integrated Security=True");


        //--------------------------------------Saurabh Start--------------------------------//

        //-------------------------Personal Details-------------------------//
        public void PersonalDetails(SeekerUser objsekUser)
        {
            if(con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();
                
            }
            SqlCommand cmd = new SqlCommand("SPSeeker", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "PersonalDetails");
            cmd.Parameters.AddWithValue("@Seekercode", objsekUser.Seekercode);
            cmd.Parameters.AddWithValue("@SeekerName", objsekUser.SeekerName);
            cmd.Parameters.AddWithValue("@ContactNo", objsekUser.ContactNo);
            cmd.Parameters.AddWithValue("@DOB", objsekUser.DOB);
            cmd.Parameters.AddWithValue("@Gender", objsekUser.Gender);
            cmd.Parameters.AddWithValue("@CorrespondenceAddress", objsekUser.CorrespondenceAddress);
            cmd.Parameters.AddWithValue("@PermanentAddress", objsekUser.PermanantAddress);
            cmd.Parameters.AddWithValue("@Pincode", objsekUser.Pincode);
            cmd.Parameters.AddWithValue("@CityId", objsekUser.CityId);
            cmd.Parameters.AddWithValue("@LanguageID", objsekUser.LanguageId);
            cmd.Parameters.AddWithValue("@PhysicallyChallenged", objsekUser.PhysicallyChallenged);
            cmd.Parameters.AddWithValue("@CasteCategory", objsekUser.CasteCategory);
            cmd.Parameters.AddWithValue("@MaritalStatus", objsekUser.MaritalStatus);
            cmd.Parameters.AddWithValue("@ProfileSummary", objsekUser.ProfileSummary);
            cmd.ExecuteNonQuery();
            con.Close();

        }
        public void updateIMG(SeekerUser objsekUser)
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();

            }
            SqlCommand cmd = new SqlCommand("SPSeeker", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "updateIMG");
            cmd.Parameters.AddWithValue("@Seekercode", objsekUser.Seekercode);
            cmd.Parameters.AddWithValue("@ProfileIMG", objsekUser.ProfileIMG);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public SqlDataReader SeekerDetails(SeekerUser objsekUser)
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();

            }
            SqlCommand cmd = new SqlCommand("SPSeeker", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "SeekerDetails");
            cmd.Parameters.AddWithValue("@Seekercode", objsekUser.Seekercode);
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            return dr;
            con.Close();
        }
        public DataSet CityBind()
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();

            }
            SqlCommand cmd = new SqlCommand("SPSeeker", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "City");
            SqlDataAdapter adpt = new SqlDataAdapter();
            adpt.SelectCommand = cmd;
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            return ds;
            con.Close();
        }
        public DataSet Language()
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();

            }
            SqlCommand cmd = new SqlCommand("SPSeeker", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "Language");
            SqlDataAdapter adpt = new SqlDataAdapter();
            adpt.SelectCommand = cmd;
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            return ds;
            con.Close();
        }
        public DataTable LanguageShow(int languageid)
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();

            }
            SqlCommand cmd = new SqlCommand("SPSeeker", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "LanguageShow");
            cmd.Parameters.AddWithValue("@LanguageID", languageid);
            SqlDataAdapter adpt = new SqlDataAdapter();
            adpt.SelectCommand = cmd;
            DataTable ds = new DataTable();
            adpt.Fill(ds);
            return ds;
            con.Close();
        }
        //-----------------------------------------Education--------------------------//
        public DataSet Qualification()
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();

            }
            SqlCommand cmd = new SqlCommand("SPSeeker", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "Qualification");
            SqlDataAdapter adpt = new SqlDataAdapter();
            adpt.SelectCommand = cmd;
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            return ds;
            con.Close();
        }
        public DataSet Degree(int QualificationID)
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();

            }
            SqlCommand cmd = new SqlCommand("SPSeeker", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "Degree");
            cmd.Parameters.AddWithValue("@QualificationID", QualificationID);
            SqlDataAdapter adpt = new SqlDataAdapter();
            adpt.SelectCommand = cmd;
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            return ds;
            con.Close();
        }
        public DataSet Specialization(int DegreeId)
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();

            }
            SqlCommand cmd = new SqlCommand("SPSeeker", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "Specialization");
            cmd.Parameters.AddWithValue("@DegreeId", DegreeId);
            SqlDataAdapter adpt = new SqlDataAdapter();
            adpt.SelectCommand = cmd;
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            return ds;
            con.Close();
        }
        public SqlDataReader GetEducationDetails(SeekerUser objseeker)
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();

            }
            SqlCommand cmd = new SqlCommand("SPSeeker", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "ShowEducationDetails");
            cmd.Parameters.AddWithValue("@Seekercode", objseeker.Seekercode);
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            return dr;
            con.Close();
        }

        public void AddSSC(SeekerUser objseeker)
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();

            }
            SqlCommand cmd = new SqlCommand("SPSeeker", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "AddSSC");
            cmd.Parameters.AddWithValue("@Seekercode", objseeker.Seekercode);
            cmd.Parameters.AddWithValue("@QualificationID", objseeker.QualificationId);
            cmd.Parameters.AddWithValue("@DegreeId", objseeker. DegreeId);
            cmd.Parameters.AddWithValue("@PassingYear", objseeker.PassingYear);
            cmd.Parameters.AddWithValue("@marks", objseeker.Marks);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public void AddHSC(SeekerUser objseeker)
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();

            }
            SqlCommand cmd = new SqlCommand("SPSeeker", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "AddHSC");
            cmd.Parameters.AddWithValue("@Seekercode", objseeker.Seekercode);
            cmd.Parameters.AddWithValue("@QualificationID", objseeker.QualificationId);
            cmd.Parameters.AddWithValue("@DegreeId", objseeker.DegreeId);
            cmd.Parameters.AddWithValue("@PassingYear", objseeker.PassingYear);
            cmd.Parameters.AddWithValue("@marks", objseeker.Marks);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public void AddUG(SeekerUser objseeker)
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();

            }
            SqlCommand cmd = new SqlCommand("SPSeeker", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "AddUG");
            cmd.Parameters.AddWithValue("@Seekercode", objseeker.Seekercode);
            cmd.Parameters.AddWithValue("@QualificationID", objseeker.QualificationId);
            cmd.Parameters.AddWithValue("@DegreeId", objseeker.DegreeId);
            cmd.Parameters.AddWithValue("@SpecalizationId", objseeker.SpecializationId);
            cmd.Parameters.AddWithValue("@University", objseeker.University);
            cmd.Parameters.AddWithValue("@PassingYear", objseeker.PassingYear);
            cmd.Parameters.AddWithValue("@marks", objseeker.Marks);
            cmd.Parameters.AddWithValue("@CourseType", objseeker.CourseType);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public void AddGraduation(SeekerUser objseeker)
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();

            }
            SqlCommand cmd = new SqlCommand("SPSeeker", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "AddGraduation");
            cmd.Parameters.AddWithValue("@Seekercode", objseeker.Seekercode);
            cmd.Parameters.AddWithValue("@QualificationID", objseeker.QualificationId);
            cmd.Parameters.AddWithValue("@DegreeId", objseeker.DegreeId);
            cmd.Parameters.AddWithValue("@SpecalizationId", objseeker.SpecializationId);
            cmd.Parameters.AddWithValue("@University", objseeker.University);
            cmd.Parameters.AddWithValue("@PassingYear", objseeker.PassingYear);
            cmd.Parameters.AddWithValue("@marks", objseeker.Marks);
            cmd.Parameters.AddWithValue("@CourseType", objseeker.CourseType);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public void AddPG(SeekerUser objseeker)
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();

            }
            SqlCommand cmd = new SqlCommand("SPSeeker", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "AddPG");
            cmd.Parameters.AddWithValue("@Seekercode", objseeker.Seekercode);
            cmd.Parameters.AddWithValue("@QualificationID", objseeker.QualificationId);
            cmd.Parameters.AddWithValue("@DegreeId", objseeker.DegreeId);
            cmd.Parameters.AddWithValue("@SpecalizationId", objseeker.SpecializationId);
            cmd.Parameters.AddWithValue("@University", objseeker.University);
            cmd.Parameters.AddWithValue("@PassingYear", objseeker.PassingYear);
            cmd.Parameters.AddWithValue("@marks", objseeker.Marks);
            cmd.Parameters.AddWithValue("@CourseType", objseeker.CourseType);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        //---------------------------Employment details----------------------------//
        public SqlDataReader GetEmploymentDetails(SeekerUser objseeker)
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();

            }
            SqlCommand cmd = new SqlCommand("SPSeeker", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "GetEmploymentDetails");
            cmd.Parameters.AddWithValue("@Seekercode", objseeker.Seekercode);
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            return dr;
            con.Close();
        }
        public void UpdateEmploymentDetails(SeekerUser objseeker)
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();

            }
            SqlCommand cmd = new SqlCommand("SPSeeker", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "UpdateEmploymentDetails");
            cmd.Parameters.AddWithValue("@Seekercode", objseeker.Seekercode);
            cmd.Parameters.AddWithValue("@CurrentEmployement", objseeker.CurrentEmployement);
            cmd.Parameters.AddWithValue("@EmployementType", objseeker.EmployementType);
            cmd.Parameters.AddWithValue("@CompanyName", objseeker.CompanyName);
            cmd.Parameters.AddWithValue("@Designation", objseeker.Designation);
            cmd.Parameters.AddWithValue("@JoiningDate", objseeker.JoiningDate);
            cmd.Parameters.AddWithValue("@CurrentSalary", objseeker.CurrentSalary);
            cmd.Parameters.AddWithValue("@SkillName", objseeker.SkillId);
            cmd.Parameters.AddWithValue("@JobProfile", objseeker.JobProfile);
            cmd.Parameters.AddWithValue("@NoticePeriod", objseeker.NoticePeriod);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public DataSet Skill()
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();

            }
            SqlCommand cmd = new SqlCommand("SPSeeker", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "Skill");
            SqlDataAdapter adpt = new SqlDataAdapter();
            adpt.SelectCommand = cmd;
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            return ds;
            con.Close();
        }
        //---------------------------Project Details-----------------------//
        public SqlDataReader GetProjectDetails(SeekerUser objseeker)
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();

            }
            SqlCommand cmd = new SqlCommand("SPSeeker", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "GetProjectDetails");
            cmd.Parameters.AddWithValue("@Seekercode", objseeker.Seekercode);
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            return dr;
            con.Close();
        }
        public void UpdateProjectDetails(SeekerUser objseeker)
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();

            }
            SqlCommand cmd = new SqlCommand("SPSeeker", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "UpdateProjectDetails");
            cmd.Parameters.AddWithValue("@Seekercode", objseeker.Seekercode);
            cmd.Parameters.AddWithValue("@ProjectTitle", objseeker.ProjectTitle);
            cmd.Parameters.AddWithValue("@ClientName", objseeker.ClientName);
            cmd.Parameters.AddWithValue("@ProjectStatus", objseeker.ProjectStatus);
            cmd.Parameters.AddWithValue("@TotalExperience", objseeker.TotalExperience);
            cmd.Parameters.AddWithValue("@ProjectDetails", objseeker.ProjectDetails);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        //---------------------------Career Profile-----------------------//
        public SqlDataReader CareerProfile(SeekerUser objseeker)
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();

            }
            SqlCommand cmd = new SqlCommand("SPSeeker", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "CareerProfile");
            cmd.Parameters.AddWithValue("@Seekercode", objseeker.Seekercode);
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            return dr;
            con.Close();
        }
        public DataSet Industry()
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();

            }
            SqlCommand cmd = new SqlCommand("SPSeeker", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "Industry");
            SqlDataAdapter adpt = new SqlDataAdapter();
            adpt.SelectCommand = cmd;
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            return ds;
            con.Close();
        }
        public DataSet JobCategory(int IndustryId)
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();

            }
            SqlCommand cmd = new SqlCommand("SPSeeker", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "JobCategory");
            cmd.Parameters.AddWithValue("@IndustryId", IndustryId);
            SqlDataAdapter adpt = new SqlDataAdapter();
            adpt.SelectCommand = cmd;
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            return ds;
            con.Close();
        }
        public DataTable PreferredLocation(int CityId)
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();

            }
            SqlCommand cmd = new SqlCommand("SPSeeker", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "PreferredLocation");
            cmd.Parameters.AddWithValue("@CityId", CityId);
            SqlDataAdapter adpt = new SqlDataAdapter();
            adpt.SelectCommand = cmd;
            DataTable ds = new DataTable();
            adpt.Fill(ds);
            return ds;
            con.Close();
        }
        public void UpdateCareerProfile(SeekerUser objseeker)
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();

            }
            SqlCommand cmd = new SqlCommand("SPSeeker", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "UpdateCareerProfile");
            cmd.Parameters.AddWithValue("@Seekercode", objseeker.Seekercode);
            cmd.Parameters.AddWithValue("@IndustryId", objseeker.IndustryID);
            cmd.Parameters.AddWithValue("@TotalExperience", objseeker.TotalExperience);
            cmd.Parameters.AddWithValue("@Location", objseeker.Location);
            cmd.Parameters.AddWithValue("@JobcategoryId", objseeker.JobCategoryId);
            cmd.Parameters.AddWithValue("@EmailId", objseeker.EmailId);
            cmd.Parameters.AddWithValue("@ContactNo", objseeker.ContactNo);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        //---------------------------Complete Profile-----------------------//
        public SqlDataReader CompleteProfile(SeekerUser objseeker)
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();

            }
            SqlCommand cmd = new SqlCommand("SPSeeker", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "CompleteProfile");
            cmd.Parameters.AddWithValue("@Seekercode", objseeker.Seekercode);
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            return dr;
            con.Close();
        }
        //---------------------------Preferred Job-----------------------//
        public DataSet PreferredJob(SeekerUser objseeker)
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();

            }
            SqlCommand cmd = new SqlCommand("SPSeeker", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "PreferredJob");
            cmd.Parameters.AddWithValue("@Seekercode", objseeker.Seekercode);
            SqlDataAdapter adpt = new SqlDataAdapter();
            adpt.SelectCommand = cmd;
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            return ds;
            con.Close();
        }
        //---------------------------All Companys-----------------------//
        public DataSet AllCompanys()
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();

            }
            SqlCommand cmd = new SqlCommand("SPSeeker", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "AllCompanys");
            SqlDataAdapter adpt = new SqlDataAdapter();
            adpt.SelectCommand = cmd;
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            return ds;
            con.Close();
        }
        public SqlDataReader CompanysDetails(SeekerUser objseeker)
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();

            }
            SqlCommand cmd = new SqlCommand("SPSeeker", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "CompanysDetails");
            cmd.Parameters.AddWithValue("@CompanyId", objseeker.CompanyId);
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            return dr;
            con.Close();
        }
        //----------------------------company Follow & company Feedback----------------------//
        public SqlDataReader CompanyFeedback(SeekerUser objseeker)
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();

            }
            SqlCommand cmd = new SqlCommand("SPSeeker", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "CompanyFeedback");
            cmd.Parameters.AddWithValue("@CompanyId", objseeker.CompanyId);
            cmd.Parameters.AddWithValue("@Seekercode", objseeker.Seekercode);
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            return dr;
            con.Close();
        }
        public void SaveCompanyFeedback(SeekerUser objseeker)
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();

            }
            SqlCommand cmd = new SqlCommand("SPSeeker", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "CompanyFeedback");
            cmd.Parameters.AddWithValue("@CompanyId", objseeker.CompanyId);
            cmd.Parameters.AddWithValue("@EmployerCode", objseeker.Employercode);
            cmd.Parameters.AddWithValue("@Seekercode", objseeker.Seekercode);
            cmd.Parameters.AddWithValue("@Rating", objseeker.Rating);
            cmd.Parameters.AddWithValue("@Review", objseeker.Review);
            cmd.Parameters.AddWithValue("@Follow", objseeker.Follow);
            cmd.ExecuteNonQuery();
            con.Close();

            SqlDataReader dr1;
            dr1 = cmd.ExecuteReader();
            if (dr1.HasRows == true)
            {
                dr1.Close();

                SqlCommand cmd2 = new SqlCommand("SPTMClient", con);
                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.Parameters.AddWithValue("@Flag", "UpdateCompanyFeedback");
                cmd.Parameters.AddWithValue("@CompanyId", objseeker.CompanyId);
                cmd.Parameters.AddWithValue("@EmployerCode", objseeker.Employercode);
                cmd.Parameters.AddWithValue("@Seekercode", objseeker.Seekercode);
                cmd.Parameters.AddWithValue("@Rating", objseeker.Rating);
                cmd.Parameters.AddWithValue("@Review", objseeker.Review);
                cmd2.ExecuteNonQuery();
                con.Close();

            }

            else
            {

                dr1.Close();

                SqlCommand cmd1 = new SqlCommand("SPTMClient", con);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@Flag", "SaveCompanyFeedback");
                cmd.Parameters.AddWithValue("@CompanyId", objseeker.CompanyId);
                cmd.Parameters.AddWithValue("@EmployerCode", objseeker.Employercode);
                cmd.Parameters.AddWithValue("@Seekercode", objseeker.Seekercode);
                cmd.Parameters.AddWithValue("@Rating", objseeker.Rating);
                cmd.Parameters.AddWithValue("@Review", objseeker.Review);
                cmd1.ExecuteNonQuery();
                con.Close();
            }
        }
        public void FollowCompany(SeekerUser objseeker)
        {

            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();

            }
            SqlCommand cmd = new SqlCommand("SPSeeker", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "CompanyFeedback");
            cmd.Parameters.AddWithValue("@CompanyId", objseeker.CompanyId);
            cmd.Parameters.AddWithValue("@EmployerCode", objseeker.Employercode);
            cmd.Parameters.AddWithValue("@Seekercode", objseeker.Seekercode);
            cmd.Parameters.AddWithValue("@Rating", objseeker.Rating);
            cmd.Parameters.AddWithValue("@Review", objseeker.Review);
            cmd.Parameters.AddWithValue("@Follow", objseeker.Follow1);
            //   cmd.ExecuteNonQuery();
            //con.Close();

            SqlDataReader dr1;
            dr1 = cmd.ExecuteReader();
            if (dr1.HasRows)
            {
                dr1.Close();
                SqlCommand cmd2 = new SqlCommand("SPSeeker", con);
                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.Parameters.AddWithValue("@flag", "FollowCompany");
                cmd2.Parameters.AddWithValue("@CompanyId", objseeker.CompanyId);
                cmd2.Parameters.AddWithValue("@EmployerCode", objseeker.Employercode);
                cmd2.Parameters.AddWithValue("@Seekercode", objseeker.Seekercode);
                cmd2.Parameters.AddWithValue("@Follow", objseeker.Follow1);
                cmd2.ExecuteNonQuery();
                con.Close();
            }

            else
            {
                dr1.Close();
                SqlCommand cmd1 = new SqlCommand("SPSeeker", con);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@flag", "SaveCompanyFeedback");
                cmd1.Parameters.AddWithValue("@CompanyId", objseeker.CompanyId);
                cmd1.Parameters.AddWithValue("@EmployerCode", objseeker.Employercode);
                cmd1.Parameters.AddWithValue("@Seekercode", objseeker.Seekercode);
                cmd1.Parameters.AddWithValue("@Follow", objseeker.Follow1);
                cmd1.ExecuteNonQuery();
                con.Close();
            }
         
        }

        //--------------------------------------Saurabh End--------------------------------//
        //--------------------------------------Sanket Start------------------------------//

        public DataSet FindJobs(SeekerUser objsekUser /*string postjobcode*/)
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand("SPSeeker", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "FindJobs");
            //cmd.Parameters.AddWithValue("@PostJobCode", postjobcode);
            cmd.Parameters.AddWithValue("@PostJobCode", objsekUser.PostJobCode);
            cmd.Parameters.AddWithValue("@PostJobCode", objsekUser.PostJobCode);
            cmd.Parameters.AddWithValue("@PostJobCode", objsekUser.PostJobCode);
            SqlDataAdapter adpt = new SqlDataAdapter();
            adpt.SelectCommand = cmd;
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            return ds;
            con.Close();
        }

        //------------------Search Jobs-------------------------------------------------//
        public DataSet FindJobs1(SeekerUser objsekUser /*string jobtitle,string joblocation,string salary*/)
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand("SPSeeker", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "FindJobs1");

            cmd.Parameters.AddWithValue("@JobTitle", objsekUser.JobTitle);
            cmd.Parameters.AddWithValue("@JobLocation", objsekUser.JobLocation);
            cmd.Parameters.AddWithValue("@Salary", objsekUser.Salary);
            SqlDataAdapter adpt = new SqlDataAdapter();
            adpt.SelectCommand = cmd;
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            return ds;
            con.Close();
        }

        //--------ApplyButton Jobs Popup---------------------------------//
        public SqlDataReader ApplyJobView(SeekerUser objsekUser /*string PostJobCode*/)
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand("SPSeeker", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Flag", "ApplyJobView");
            cmd.Parameters.AddWithValue("@PostJobCode", objsekUser.PostJobCode);
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            return dr;
            con.Close();
        }


        //-----------------ResumePDF----------------------------------------------//

        public void SubmitPDF(SeekerUser objsekUser /*string seekercode, string postjobcode, int statusid, DateTime applieddate, string resumepdf*/)
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand("SPSeeker", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "SubmitPDF");
            cmd.Parameters.AddWithValue("@Seekercode", objsekUser.Seekercode);
            cmd.Parameters.AddWithValue("@PostJobCode", objsekUser.PostJobCode);
            cmd.Parameters.AddWithValue("@StatusID", objsekUser.StatusId);
            cmd.Parameters.AddWithValue("@AppliedDate", objsekUser.AppliedDate);
            cmd.Parameters.AddWithValue("@ResumePDF", objsekUser.ResumePDF);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        //----------ApplicationGriview--------------------------//
        public DataSet ApplicationGrid(SeekerUser objsekUser /*string seekercode*/)
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand("SPSeeker", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "ApplicationGrid");
            cmd.Parameters.AddWithValue("@Seekercode", objsekUser.Seekercode);
            SqlDataAdapter adpt = new SqlDataAdapter();
            adpt.SelectCommand = cmd;
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            return ds;
            con.Close();
        }

        //------------DeleteButtononGrid----------------//
        public void IsDelete(SeekerUser objsekUser /*int jobappliedid*/)
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand("SPSeeker", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "IsDelete");
            cmd.Parameters.AddWithValue("@AppliedJobId", objsekUser.AppliedJobID);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        //-----------StatusUpdateonJobSubmit-----------//

        public SqlDataReader SubmitApplication(SeekerUser objsekUser /*int  jobappliedid,int statusid*/)
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand("SPSeeker", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "SubmitApplication");
            cmd.Parameters.AddWithValue("@StatusID", objsekUser.StatusId);
            cmd.Parameters.AddWithValue("@AppliedJobId", objsekUser.AppliedJobID);
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            return dr;
            con.Close();
        }

        //------------------DownloadPDF------------------------//
        public SqlDataReader DownloadPDF(SeekerUser objsekUser  /*int seekerid*/)
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand("SPSeeker", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "DownloadPDF");
            cmd.Parameters.AddWithValue("@SeekerId", objsekUser.SeekerId);
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            return dr;
            con.Close();
        }

        //----------------------------Sanket End--------------------------------//
    }
}
