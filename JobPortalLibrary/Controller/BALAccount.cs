using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using JobPortalLibrary.Controller;


namespace JobPortalLibrary.Controller
{
    public class BALAccount
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-RU5490M;Initial Catalog=\"Job Portal\";Integrated Security=True");

        //--------------------------------------Saurabh Start--------------------------------//
        public void SeekerRegister(AccountUser objaccount)
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();

            }
            SqlCommand cmd = new SqlCommand("SPSeeker", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "SeekerRegister");
            cmd.Parameters.AddWithValue("@Seekercode", objaccount.Seekercode);
            cmd.Parameters.AddWithValue("@SeekerName", objaccount.SeekerName);
            cmd.Parameters.AddWithValue("@EmailId", objaccount.EmailId);
            cmd.Parameters.AddWithValue("@Password", objaccount.Password);
            cmd.Parameters.AddWithValue("@ContactNo", objaccount.ContactNo);
         //   cmd.Parameters.AddWithValue("@ResumePDF", objaccount.ResumePDF);
            cmd.Parameters.AddWithValue("@DateOfRegistration", objaccount.DateOfRegistration);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public void EmployeerRegister(AccountUser objaccount)
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();

            }
            SqlCommand cmd = new SqlCommand("SPSeeker", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "EmployeerRegister");
            cmd.Parameters.AddWithValue("@EmployerCode", objaccount.Employercode);
            cmd.Parameters.AddWithValue("@EmployerName", objaccount.SeekerName);
            cmd.Parameters.AddWithValue("@EmailId", objaccount.EmailId);
            cmd.Parameters.AddWithValue("@Password", objaccount.Password);
            cmd.Parameters.AddWithValue("@ContactNo", objaccount.ContactNo);
           // cmd.Parameters.AddWithValue("@ContactNo", Dateofregistration);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public SqlDataReader SeekerCode()
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();

            }
            SqlCommand cmd = new SqlCommand("SPSeeker", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "SeekerCode");
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            return dr;
            con.Close();
        }
        public SqlDataReader EmployeerCode()
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();

            }
            SqlCommand cmd = new SqlCommand("SPSeeker", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "EmployeerCode");
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            return dr;
            con.Close();
        }
        public void EducationDetails(AccountUser objaccount)
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();

            }
            SqlCommand cmd = new SqlCommand("SPSeeker", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "EducationDetails");
            cmd.Parameters.AddWithValue("@Seekercode", objaccount.Code);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public void EmploymentDetails(AccountUser objaccount)
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();

            }
            SqlCommand cmd = new SqlCommand("SPSeeker", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "EmploymentDetails");
            cmd.Parameters.AddWithValue("@Seekercode", objaccount.Code);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public void ProjectDetails(AccountUser objaccount)
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();

            }
            SqlCommand cmd = new SqlCommand("SPSeeker", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "ProjectDetails");
            cmd.Parameters.AddWithValue("@Seekercode", objaccount.Code);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public void JobAlertSave(AccountUser objaccount)
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();

            }
            SqlCommand cmd = new SqlCommand("SPSeeker", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "JobAlertSave");
            cmd.Parameters.AddWithValue("@Seekercode", objaccount.Code);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public SqlDataReader Adminemail(AccountUser objaccount)
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();

            }
            SqlCommand cmd = new SqlCommand("SPAdmin", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "Adminemail");
            cmd.Parameters.AddWithValue("@EmailId", objaccount.EmailId);
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            return dr;
            con.Close();
        }
        //--------------------------------------Saurabh End--------------------------------//


        //--------------------------------------Muskan Start--------------------------------//


        //--------------------seekerlogin-------------------------------------------------------
        public SqlDataReader Login(AccountUser objaccount)
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();

            }
            SqlCommand cmd = new SqlCommand("SPSeeker", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "LoginSeeker");
            cmd.Parameters.AddWithValue("@EmailId", objaccount.EmailId);
            cmd.Parameters.AddWithValue("@Password", objaccount.Password);

            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            return dr;
            con.Close();
        }

        //--------------------employerlogin---------------------------------------------------
        public SqlDataReader LoginEmp(AccountUser objaccount)
        {
            con.Close();
            con.Open();
            SqlCommand cmd = new SqlCommand("Employeer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "LoginEmployer");
            cmd.Parameters.AddWithValue("@EmailId", objaccount.EmailId);
            cmd.Parameters.AddWithValue("@Password", objaccount.Password);
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            return dr;
            con.Close();
        }
        //--------------------Adminlogin---------------------------------------------------
        public SqlDataReader LoginAdmin(AccountUser objaccount)
        {
            con.Close();
            con.Open();
            SqlCommand cmd = new SqlCommand("SPAdmin", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "Loginadmin");
            cmd.Parameters.AddWithValue("@EmailId", objaccount.EmailId);
            cmd.Parameters.AddWithValue("@Password", objaccount.Password);
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            return dr;
            con.Close();
        }
        //--------------------forgetpasswordseeker-------------------------------------------------
        public SqlDataReader forgetpasswords(AccountUser objaccount)
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();

            }
            SqlCommand cmd = new SqlCommand("SPSeeker", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "ForgetPasswordseek");
            cmd.Parameters.AddWithValue("@EmailId", objaccount.EmailId);
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            return dr;
            con.Close();
        }
        //--------------------forgetpasswordemployer-------------------------------------------------
        public SqlDataReader forgetpasswordE(AccountUser objaccount)
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();

            }
            SqlCommand cmd = new SqlCommand("Employeer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "ForgetPasswordemp");
            cmd.Parameters.AddWithValue("@EmailId", objaccount.EmailId);
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            return dr;
            con.Close();
        }

        //--------------------SignInWithGoogle(Externalloginseeker)------------------------------

        public SqlDataReader ExternalLogin(string EmailId)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SPSeeker", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "CheckExternalLogin");
            cmd.Parameters.AddWithValue("@EmailId", EmailId);
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            return dr;
            con.Close();
        }
        //--------------------signinwithgoogle(ExternalRegisterSeeker)-----------------------------
        public void ExternalRegister(string Seekercode, string name, string email, DateTime Dateofregistration)
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand cmd = new SqlCommand("SPSeeker", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "RegisterExternalLogin");
            cmd.Parameters.AddWithValue("@Seekercode", Seekercode);
            cmd.Parameters.AddWithValue("@SeekerName", name);
            cmd.Parameters.AddWithValue("@EmailId", email);
            cmd.Parameters.AddWithValue("@DateOfRegistration", Dateofregistration);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        //--------------------SignInWithGoogle(ExternalloginEmployer)----------------------------------------
        public SqlDataReader ExternalLogine(string EmailId)
        {
            con.Close();
            con.Open();
            SqlCommand cmd = new SqlCommand("Employeer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "CheckExternalLoginE");
            cmd.Parameters.AddWithValue("@EmailId", EmailId);
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            return dr;
            con.Close();
        }

        //--------------------externalRegisterEmployer---------------------------------------------------
        public void ExternalRegisterEmp(string Employercode, string EmployerName, string EmailId, string Password, Int64 ContactNo)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Employeer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flag", "RegisterExternalLoginEmp");
            cmd.Parameters.AddWithValue("@EmployeerCode", Employercode);
            cmd.Parameters.AddWithValue("@EmployerName", EmployerName);
            cmd.Parameters.AddWithValue("@EmailId", EmailId);
            cmd.Parameters.AddWithValue("@Password", Password);
            cmd.Parameters.AddWithValue("@ContactNo", ContactNo);

            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}
