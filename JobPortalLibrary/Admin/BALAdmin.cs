using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace JobPortalLibrary.Admin
{
    public class BALAdmin
    {
        // SqlConnection con = new SqlConnection("Data Source=DESKTOP-B3UBKFN;Initial Catalog=\"Job Portal\";Integrated Security=True");
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
        //------------------Rita Start-----------------------------------------//

        //--------------------Admin Jov Application---------------------------------------------------------/

        public DataSet RPJobStatusApprovle()
        {
            ManageConnection();
            SqlCommand cmb = new SqlCommand("SPAdmin", con);
            cmb.CommandType = CommandType.StoredProcedure;
            cmb.Parameters.AddWithValue("@Flag", "RPJobStatusApprovle");
            SqlDataAdapter adpt = new SqlDataAdapter();
            adpt.SelectCommand = cmb;
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            return ds;
        }
        public void RPUpdateJobStatusApprovle(AdminUser objAdmin)
        {
            ManageConnection();
            SqlCommand cmd = new SqlCommand("SPAdmin", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Flag", "RPUpdateJobStatusApprovle");
            cmd.Parameters.AddWithValue("@PostJobCode", objAdmin.PostJobCode);
            cmd.Parameters.AddWithValue("@StatusId", objAdmin.StatusId);
            cmd.ExecuteNonQuery();

        }
        public void RPUpdatejobRejectionReason(AdminUser objAdmin)
        {
            ManageConnection();
            SqlCommand cmd = new SqlCommand("SPAdmin", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Flag", "RPUpdatejobRejectionReason");
            cmd.Parameters.AddWithValue("@PostJobCode", objAdmin.PostJobCode);
            cmd.Parameters.AddWithValue("@StatusId", objAdmin.StatusId);
            cmd.Parameters.AddWithValue("@RejectionReason", objAdmin.RejectionReason);
            cmd.ExecuteNonQuery();

        }

        public SqlDataReader RPJobDetails(AdminUser objAdmin)
        {
            ManageConnection();
            SqlCommand cmd = new SqlCommand("SPAdmin", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Flag", "RPJobDetails");
            cmd.Parameters.AddWithValue("@PostJobCode", objAdmin.PostJobCode);
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            return dr;
        }
        //------------------------Admin Company Reviews----------------------------------------------------/

        public DataSet RPCompanyGridview()
        {
            ManageConnection();
            SqlCommand cmb = new SqlCommand("SPAdmin", con);
            cmb.CommandType = CommandType.StoredProcedure;
            cmb.Parameters.AddWithValue("@Flag", "RPCompanyGridview");
            //cmb.Parameters.AddWithValue("@ReviewId", ReviewId);
            SqlDataAdapter adpt = new SqlDataAdapter();
            adpt.SelectCommand = cmb;
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            return ds;

        }
        public void RPCompanyIsDelete(AdminUser objAdmin)
        {
            ManageConnection();
            SqlCommand cmd = new SqlCommand("SPAdmin", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Flag", "RPCompanyIsDelete");
            //cmd.Parameters.AddWithValue("@isDelete", isDelete);
            cmd.Parameters.AddWithValue("@ReviewId", objAdmin.ReviewId);
            cmd.ExecuteNonQuery();

        }
        public void RPCompanyReviewStatusUpdate(AdminUser objAdmin)
        {
            ManageConnection();
            SqlCommand cmd = new SqlCommand("SPAdmin", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Flag", "RPCompanyReviewStatusUpdate");
            cmd.Parameters.AddWithValue("@StatusId", objAdmin.StatusId);
            cmd.Parameters.AddWithValue("@ReviewId", objAdmin.ReviewId);
            cmd.ExecuteNonQuery();

        }
        public SqlDataReader RPCompanyGridviewDetails(AdminUser objAdmin)
        {
            ManageConnection();
            SqlCommand cmd = new SqlCommand("SPAdmin", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Flag", "RPCompanyGridviewDetails");
            cmd.Parameters.AddWithValue("@ReviewId", objAdmin.ReviewId);
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            return dr;
        }

        // --------------------Admin--Employers-----------//
        public DataSet RPAdminEmployeGrid()
        {
            ManageConnection();
            SqlCommand cmb = new SqlCommand("SPAdmin", con);
            cmb.CommandType = CommandType.StoredProcedure;
            cmb.Parameters.AddWithValue("@Flag", "RPAdminEmployeGrid");
            SqlDataAdapter adpt = new SqlDataAdapter();
            adpt.SelectCommand = cmb;
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            return ds;

        }

        public void RPPaymentStatusUpdate(AdminUser objAdmin)
        {
            ManageConnection();
            SqlCommand cmd = new SqlCommand("SPAdmin", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Flag", "RPPaymentStatusUpdate");
            cmd.Parameters.AddWithValue("@StatusId", objAdmin.StatusId);
            cmd.Parameters.AddWithValue("@EmployeerCode", objAdmin.Employercode);
            cmd.ExecuteNonQuery();

        }



        //---------------------- Admin DashBord Count------------///
        public SqlDataReader RPTotalJobsPosted()
        {
            ManageConnection();
            SqlCommand cmd = new SqlCommand("SPAdmin", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Flag", "RPTotalJobsPosted");
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            return dr;
        }
        public SqlDataReader RPTotalSeekerRegister()
        {
            ManageConnection();
            SqlCommand cmd = new SqlCommand("SPAdmin", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Flag", "RPTotalSeekerRegister");
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            return dr;
        }
        public SqlDataReader RPTotalEmployerRegister()
        {
            ManageConnection();
            SqlCommand cmd = new SqlCommand("SPAdmin", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Flag", "RPTotalEmployerRegister");
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            return dr;
        }
        public SqlDataReader RPTotalApplicationAppvalAndReject()
        {
            ManageConnection();
            SqlCommand cmd = new SqlCommand("SPAdmin", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Flag", "RPTotalApplicationAppvalAndReject");
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            return dr;
        }
        public DataSet RPGetStatus()
        {
            ManageConnection();
            SqlCommand cmb = new SqlCommand("SPAdmin", con);
            cmb.CommandType = CommandType.StoredProcedure;
            cmb.Parameters.AddWithValue("@Flag", "RPGetStatus");
            SqlDataAdapter adpt = new SqlDataAdapter();
            adpt.SelectCommand = cmb;
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            return ds;

        }

        //----------Rita End------------------------------------------//

    }
}
