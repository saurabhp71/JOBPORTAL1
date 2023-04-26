using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace JobPortalLibrary.Employer
{
    public class BALEmployer
    {
       // SqlConnection con = new SqlConnection("Data Source=DESKTOP-RU5490M;Initial Catalog=\"Job Portal\";Integrated Security=True");
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-UIFFRJ2;Initial Catalog=\"Job Portal\";Integrated Security=True");

        //---------------------------------Mahesh Start-----------------------------//
        public DataSet getjobgrid()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Employeer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "getjobgrid");
            SqlDataAdapter adpt = new SqlDataAdapter();
            adpt.SelectCommand = cmd;
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            return ds;
            con.Close();

        }
        public DataSet getapplyjobstud(EmployerUser obj)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Employeer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "applyjobstud");
            cmd.Parameters.AddWithValue("@PostJobCode", obj.PostJobCode);
            SqlDataAdapter adpt = new SqlDataAdapter();
            adpt.SelectCommand = cmd;
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            return ds;
            con.Close();


        }
        public void getaprove(EmployerUser obj)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Employeer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "getstatusaprove");
            cmd.Parameters.AddWithValue("@StatusId", obj.StatusId);
            cmd.Parameters.AddWithValue("@AppliedJobId", obj.AppliedJobId);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public DataSet getstatus(EmployerUser obj)
        {
            con.Open();
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
            con.Close();
        }
        public DataSet getactivecount(EmployerUser obj)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Employeer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "getactivecount");
            cmd.Parameters.AddWithValue("@PostJobCode", obj.PostJobCode);
            SqlDataAdapter adpt = new SqlDataAdapter();
            adpt.SelectCommand = cmd;
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            return ds;
            con.Close();

        }
        public DataSet getawaitingreveiwcount(EmployerUser obj)
        {
            con.Close();
            con.Open();
            SqlCommand cmd = new SqlCommand("Employeer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "getawaitingreviewcount");
            cmd.Parameters.AddWithValue("@PostJobCode", obj.PostJobCode);
            SqlDataAdapter adpt = new SqlDataAdapter();
            adpt.SelectCommand = cmd;
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            return ds;
            con.Close();

        }
        public DataSet getreviewcount(EmployerUser obj)
        {
            con.Close();
            con.Open();
            SqlCommand cmd = new SqlCommand("Employeer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "getreviewcount");
            cmd.Parameters.AddWithValue("@PostJobCode", obj.PostJobCode);
            SqlDataAdapter adpt = new SqlDataAdapter();
            adpt.SelectCommand = cmd;
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            return ds;
            con.Close();

        }
        public DataSet getcontactingcount(EmployerUser obj)
        {
            con.Close();
            con.Open();
            SqlCommand cmd = new SqlCommand("Employeer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "getcontactingcount");
            cmd.Parameters.AddWithValue("@PostJobCode", obj.PostJobCode);
            SqlDataAdapter adpt = new SqlDataAdapter();
            adpt.SelectCommand = cmd;
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            return ds;
            con.Close();

        }
        public DataSet gethirecount(EmployerUser obj)
        {
            con.Close();
            con.Open();
            SqlCommand cmd = new SqlCommand("Employeer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "gethirecount");
            cmd.Parameters.AddWithValue("@PostJobCode", obj.PostJobCode);
            SqlDataAdapter adpt = new SqlDataAdapter();
            adpt.SelectCommand = cmd;
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            return ds;
            con.Close();

        }
        public DataSet getrejectedcount(EmployerUser obj)
        {
            con.Close();
            con.Open();
            SqlCommand cmd = new SqlCommand("Employeer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "getrejectcount");
            cmd.Parameters.AddWithValue("@PostJobCode", obj.PostJobCode);
            SqlDataAdapter adpt = new SqlDataAdapter();
            adpt.SelectCommand = cmd;
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            return ds;
            con.Close();

        }
        public SqlDataReader JobDetails(EmployerUser obj)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Employeer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "JobDetails");
            cmd.Parameters.AddWithValue("@PostJobCode", obj.PostJobCode);
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            return dr;
            con.Close();
        }
        public DataSet getphonedetails(EmployerUser obj)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Employeer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "Getphonedetails");
            cmd.Parameters.AddWithValue("@AppliedJobId", obj.AppliedJobId);
            SqlDataAdapter adpt = new SqlDataAdapter();
            adpt.SelectCommand = cmd;
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            return ds;
            con.Close();

        }
        public void deletecandidates(EmployerUser obj)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Employeer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "Deletecandidate");
            cmd.Parameters.AddWithValue("@Seekercode", obj.Seekercode);
            cmd.ExecuteNonQuery();
            con.Close();

        }
        public DataSet geteducation()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Employeer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "GetEducation");
            SqlDataAdapter adpt = new SqlDataAdapter();
            adpt.SelectCommand = cmd;
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            return ds;
            con.Close();

        }
        public DataSet getdegree(EmployerUser obj)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Employeer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "GetDegree");
            cmd.Parameters.AddWithValue("@QualificationId", obj.QualificationId);
            SqlDataAdapter adpt = new SqlDataAdapter();
            adpt.SelectCommand = cmd;
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            return ds;
            con.Close();

        }
        public DataSet getspecialization(EmployerUser obj)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Employeer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "GetSpecialization");
            cmd.Parameters.AddWithValue("@DegreeId", obj.DegreeId);
            SqlDataAdapter adpt = new SqlDataAdapter();
            adpt.SelectCommand = cmd;
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            return ds;
            con.Close();

        }
        public DataSet getseekercode()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Employeer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "Getseekercode");
            SqlDataAdapter adpt = new SqlDataAdapter();
            adpt.SelectCommand = cmd;
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            return ds;
            con.Close();

        }
        public SqlDataReader Editjob(EmployerUser obj)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Employeer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "EditJob");
            cmd.Parameters.AddWithValue("@PostJobCode", obj.PostJobCode);
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            return dr;
            con.Close();
        }
        public void saveseekerdetail(EmployerUser obj, string seekercode, string Resume)
        {
            con.Close();
            con.Open();
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
            con.Close();

        }
        public void applyjob(EmployerUser obj, string seekercode, int statusid, string Resume)
        {
            con.Close();
            con.Open();
            SqlCommand cmd = new SqlCommand("Employeer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "ApplyJob");
            cmd.Parameters.AddWithValue("@Seekercode", seekercode);
            cmd.Parameters.AddWithValue("@PostJobCode", obj.PostJobCode);
            cmd.Parameters.AddWithValue("@StatusId", statusid);
            cmd.Parameters.AddWithValue("@AppliedDate", DateTime.Now);
            cmd.Parameters.AddWithValue("@ResumePDF", Resume);
            // cmd.Parameters.AddWithValue("@isDelete", obj.isDelete);
            cmd.ExecuteNonQuery();
            con.Close();

        }
        public void savesscdetail(EmployerUser obj, string seekercode)
        {
            con.Close();
            con.Open();
            SqlCommand cmd = new SqlCommand("Employeer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "SaveSSCdetail");
            cmd.Parameters.AddWithValue("@Seekercode", seekercode);
            cmd.Parameters.AddWithValue("@SSC", obj.SSC);
            cmd.Parameters.AddWithValue("@SSCBoard", obj.SSCBoard);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public void savehscdetail(EmployerUser obj, string seekercode)
        {
            con.Close();
            con.Open();
            SqlCommand cmd = new SqlCommand("Employeer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "SaveHSCdetail");
            cmd.Parameters.AddWithValue("@Seekercode", seekercode);
            cmd.Parameters.AddWithValue("@HSC", obj.HSC);
            cmd.Parameters.AddWithValue("@HSCBoard", obj.HSCBoard);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public void saveundergraduation(EmployerUser obj, string seekercode)
        {
            con.Close();
            con.Open();
            SqlCommand cmd = new SqlCommand("Employeer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "SaveUGDetail");
            cmd.Parameters.AddWithValue("@Seekercode", seekercode);
            cmd.Parameters.AddWithValue("@UG", obj.QualificationId);
            cmd.Parameters.AddWithValue("@UGDegree", obj.DegreeId);
            cmd.Parameters.AddWithValue("@UGSpecialization", obj.SpecializationId);
            cmd.Parameters.AddWithValue("@UGUnivercity", obj.Univercity);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public void savegraduationdetail(EmployerUser obj, string seekercode)
        {
            con.Close();
            con.Open();
            SqlCommand cmd = new SqlCommand("Employeer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "SaveGraduationDetail");
            cmd.Parameters.AddWithValue("@Seekercode", seekercode);
            cmd.Parameters.AddWithValue("@Graduation", obj.QualificationId);
            cmd.Parameters.AddWithValue("@GraduationDegree", obj.DegreeId);
            cmd.Parameters.AddWithValue("@GraduationSpecialization", obj.SpecializationId);
            cmd.Parameters.AddWithValue("@GraduationUnivercity", obj.Univercity);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public void savepgdetail(EmployerUser obj, string seekercode)
        {
            con.Close();
            con.Open();
            SqlCommand cmd = new SqlCommand("Employeer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "SavePGDetail");
            cmd.Parameters.AddWithValue("@Seekercode", seekercode);
            cmd.Parameters.AddWithValue("@PG", obj.QualificationId);
            cmd.Parameters.AddWithValue("@PGDegree", obj.DegreeId);
            cmd.Parameters.AddWithValue("@PGSpecialization", obj.SpecializationId);
            cmd.Parameters.AddWithValue("@PGUnivercity", obj.Univercity);
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}


//---------------------------------Mahesh end-----------------------------//


