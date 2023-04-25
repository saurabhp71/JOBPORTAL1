using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace JobPortalLibrary.Admin
{
    public class AdminUser
    {
        public int AdminId { get; set; }
        public string AdminName { get; set; }

        public string EmailId { get; set; }

        public Int64 ContactNo { get; set; }

        public string ProfilePicture { get; set; }

        public string Password { get; set; }
        public int BenefitId { get; set; }
        public string Benefit { get; set; }

        public int PlanId { get; set; }

        public string Plans { get; set; }

        public string PlanPrice { get; set; }

        public string PlanDuration { get; set; }

        public DateTime PlanRegistrationDate{ get; set; }

        public string Planfor { get; set; }

        public int PaymentId { get; set; }

        public string Seekercode { get; set; }

        public string Employercode { get; set; }

        public string PaymentMode { get; set; }
        public DateTime SubscriptionDate { get; set; }

        public string TransactionId { get; set; }

        public int StatusId { get; set; }

        public string Status { get; set; }





    }
}
