using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Xml.Linq;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.IO;


namespace JobPortalLibrary.JobSeeker
{
    public class SeekerUser
    {
        public int SeekerId { get; set; }

        public string Seekercode { get; set; }
        [Required(ErrorMessage = "Please enter name")]
        public string SeekerName { get; set; }

        public string EmailId { get; set; }

        public string Password { get; set; }

        public Int64 ContactNo { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Date Of Birth")]
        [Required]
        public DateTime DOB { get; set; }
        [Required(ErrorMessage = "Select Gender")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "Enter Address")]
        public string CorrespondenceAddress { get; set; }
        [Required(ErrorMessage = "Enter Address")]
        public string PermanantAddress { get; set; }
        [Required]
        public int Pincode { get; set; }
        [Required(ErrorMessage = "Select Location")]
        public int CityId { get; set; }
        [Required(ErrorMessage = "Select Language")]
        public string LanguageId { get; set; }
        [Required]
        public int PhysicallyChallenged { get; set; }
        [Required(ErrorMessage = "Select CasteCategory")]
        public string CasteCategory { get; set; }
        [Required(ErrorMessage = "Select MaritalStatus")]
        public string MaritalStatus { get; set; }

        public DateTime DateOfRegistration { get; set; }
        [Required(ErrorMessage = "Please enter ProfileSummary")]
        public string ProfileSummary { get; set; }

        public int StatusId { get; set; }

        public string ResumePDF { get; set; }

        public string ProfileIMG { get; set; }

        public int isDelete { get; set; }

        public int EmployementId { get; set; }
        [Required(ErrorMessage = "Select Current Employement")]
        public int CurrentEmployement { get; set; }
        [Required(ErrorMessage = "Select EmployementType")]
        public string EmployementType { get; set; }
        [Required(ErrorMessage = "Please enter Company Name")]
        public string CompanyName { get; set; }
        [Required(ErrorMessage = "Please enter Designation")]
        public string Designation { get; set; }

        public string JoiningDate { get; set; }
        [Required(ErrorMessage = "Please enter CurrentSalary")]
        public string CurrentSalary { get; set; }

        public string SkillId { get; set; }
        [Required(ErrorMessage = "Please enter JobProfile")]
        public string JobProfile { get; set; }
        [Required(ErrorMessage = "Select NoticePeriod")]
        public string NoticePeriod { get; set; }

        public int ProjectId { get; set; }
        [Required(ErrorMessage = "Please enter Project Title")]
        public string ProjectTitle { get; set; }
        [Required(ErrorMessage = "Please enter Client Name")]
        public string ClientName { get; set; }

        public string ProjectStatus { get; set; }
        [Required(ErrorMessage = "Select Experience")]
        public string TotalExperience { get; set; }
        [Required(ErrorMessage = "Please enter Project Details")]
        public string ProjectDetails { get; set; }

        [Required(ErrorMessage = "Select Qualification")]
        public int QualificationId { get; set; }

        public string Qualification { get; set; }
        [Required(ErrorMessage = "Select DegreeId")]
        public int DegreeId { get; set; }

        public string Degree { get; set; }
        [Required(ErrorMessage = "Select DegreeId")]
        public int SpecializationId { get; set; }

        public string Specialization { get; set; }

        public string Language { get; set; }

        public string Status { get; set; }
       
        public string SkillName { get; set; }
        [Required(ErrorMessage = "Select Year")]
        public int PassingYear { get; set; }
        [Required(ErrorMessage = "Please enter Marks")]
        public int Marks { get; set; }
        [Required(ErrorMessage = "Please enter University")]
        public string University { get; set; }

        public int JobAlertId { get; set; }

        public string AlertKeyword { get; set; }

        public int ReviewId { get; set; }

        public int CompanyId { get; set; }

        public string Employercode { get; set; }

        public string Rating { get; set; }

        public string Review { get; set; }

        public int Follow { get; set; }

        public int CountryId { get; set; }

        public string Country { get; set; }

        public int StateId { get; set; }

        public string State { get; set; }

        public string City { get; set; }

        //Add new Property//
        
        public string[] LanguageList { get; set; }
        public string[] LocationList { get; set; }
        public string[] SkillList { get; set; }
        [Required(ErrorMessage = "Select Course Type")]
        public string CourseType { get; set; }
        public string Year { get; set; }
        public int Experience { get; set; }
        public string Month { get; set; }
        public string Location { get; set; }
        public string Physically { get; set; }
        public string EmploymentCurrent { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        public DateTime EdnDate { get; set; }
       
        public string ExYear { get; set; }
        public string ExMonth { get; set; }

        public string SSC { get; set; }

        public string SSCBoard { get; set; }
        public string SSCBoard1 { get; set; }

        public string SSCPassingYear { get; set; }

        public string SSCmarks { get; set; }
        public string HSC { get; set; }

        public string HSCBoard { get; set; }
        public string HSCBoard1 { get; set; }

        public string HSCPassingYear { get; set; }

        public string HSCmarks { get; set; }
        public string UG { get; set; }

        public string UGDegree { get; set; }

        public string UGSpecialization { get; set; }
        public string UG1 { get; set; }

        public string UGDegree1 { get; set; }

        public string UGSpecialization1 { get; set; }

        public string UGUniversity { get; set; }

        public string UGPassingYear { get; set; }
        public string UGCourseType { get; set; }
        public string UGmarks { get; set; }
        public string Graduation { get; set; }

        public string GraduationDegree { get; set; }

        public string GraduationSpecialization { get; set; }
        public string G1 { get; set; }

        public string GDegree1 { get; set; }

        public string GSpecialization1 { get; set; }

        public string GraduationUniversity { get; set; }

        public string GraduationPassingYear { get; set; }
        public string GCourseType { get; set; }
        public string Graduationmarks { get; set; }
        public string PG { get; set; }

        public string PGDegree { get; set; }

        public string PGSpecialization { get; set; }

        public string PGUniversity { get; set; }

        public string PGPassingYear { get; set; }

        public string PGmarks { get; set; }
        public string PGCourseType { get; set; }
        public string PG1 { get; set; }

        public string PGDegree1 { get; set; }

        public string PGSpecialization1 { get; set; }
        [Required(ErrorMessage = "Please enter ContactNo")]
        public string AlternateContactNo { get;set; }
        public string ContactNo1 { get; set; }
        public string files { get; set; }
        public int JobCategoryId { get; set; }

        public string JobCategory { get; set; }
        public string Industry { get; set; }
        public int IndustryID { get; set; }
        public string JobTitle { get; set; }

        public List<SeekerUser> lstuser {  get; set; }
        public string PostJobCode { get; set; }
        public string JobType { get; set; }

        public string DOB1 { get; set; }
        [Required(ErrorMessage = "Please enter Pincode")]
        public string Pincode1 { get; set; }
        public string CityId1 { get; set; }
        public string Slogan { get; set; }
        public string CompanyLogo { get; set; }
        public string AboutCompany { get; set; }
        public string NoOfEmployees { get; set; }
        public string CompanyWebsite { get; set; }
        public bool Follow1 { get; set; }
        public string Follow2 { get; set; }
        //--------Sanket Side---------//

        public string JobLocation { get; set; }
        public string Salary { get; set; }

        public string EndDate { get; set; }

        public string JobDescription { get; set; }
        public DateTime AppliedDate { get; set; }
        public int AppliedJobID { get; set; }

        public string UploadFile { get; set; }

        public List<SeekerUser> user { get; set; }


    }
}
