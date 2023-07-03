using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using NamrataKalyani.Models;

namespace NamrataKalyani.Models
{


    public class PatientInfoModel
    {
        public enum Gender
        {
            Male,
            Female
        }

        public string  Designation { get; set; }
        public int? pid { get; set; }
        public string Password { get; set; }

        public int? docid { get; set; }

        //public SelectList DoctorList { get; set; }

        [DisplayName("Doctor")]
        [Required(ErrorMessage ="Select Doctor")]
        public string DoctorName { get; set; }
        public string SelectedDoctor { get; set; }

        [DisplayName("Patient Name")]
        [Required(ErrorMessage ="Enter Patient")]
        public string pname { get; set; }
        [DisplayName("Age")]
        [Required(ErrorMessage = "Enter Age")]
        public int? age { get; set; }
        [DisplayName("Ref. By Doctor")]
        public string RefByDoc { get; set; }
        [DisplayName("Gender")]
        [Required(ErrorMessage ="Select Gender")]
        public string gender { get; set; }
        [DisplayName("Mobile No.")]
        [Required(ErrorMessage ="Enter Mobile Number")]
        public string mobileNo { get; set; }
        public string Name_Mobile { get; set; }

        [DisplayName("Created Date")]
        public DateTime? CreatedOn { get; set; }

        [DisplayName("Updated Date")]
        public DateTime? UpdatedOn { get; set; }

        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public string CreatedName { get; set; }
        public  string UpdatedName { get; set; }
        [Display(Name="Reports")]
        //[Required(ErrorMessage ="Select Report")]
        public int ReportTypeId { get; set; }
        public int ReportId { get; set; }

        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
         
        public string RptId { get; set; }

        [Required(ErrorMessage ="Doctor is Required")]
        public string DoctorList { get; set; }
        public string CollectedByList { get; set; }
              [DisplayName("CollectedBy")]        [Required(ErrorMessage =("Select Collected By"))]        public int CollectedById { get; set; }

        [DisplayName("CollectedByName")]        [Required(ErrorMessage = ("Select Collected By"))]

        public string CollectedByName { get; set; }


        [DisplayName("Bill Book Number")]        [Required(ErrorMessage = ("Bill Id Cannot Be Empty"))]        public int BillBookNumber { get; set; }

                [Required(ErrorMessage = ("Cannot be Empty"))]        public string ymd { get; set; }

        [Required(ErrorMessage = ("Surname Cannot be Empty"))]        public string surname { get; set; }

        [DisplayName("Total")]        [Required(ErrorMessage = ("Paid Ammount Cannout be Empty "))]        public decimal Total { get; set; }
        [DisplayName("Paid")]        [Required(ErrorMessage = ("Paid Ammount Cannout be Empty "))]        public decimal PaidAmmount { get; set; }

        [DisplayName("Due")]        [Required(ErrorMessage = ("Due Ammount Cannot be Empty"))]        public decimal Due { get; set; }
        [DisplayName("Select to Print")]
        public bool chkprint { get; set; }
        public string MobileBelongsTo { get; set; }
        public string Address { get; set; }
        public int BillId { get; set; }
        
    }

    public class PatientInfoOldModel
    {
        public string mobileNo { get; set; }
    }

    public class EditPatientInfoModel
    {
        


        public int? pid { get; set; }


        public string ymd { get; set; }

        public string surname { get; set; }

        [DisplayName("Doctor")]
        [Required(ErrorMessage = "Select Doctor")]
        public string DoctorName { get; set; }
        public string SelectedDoctor { get; set; }

        [Required(ErrorMessage = "Doctor is Required")]
        public string DoctorList { get; set; }

        [DisplayName("Patient Name")]
        [Required(ErrorMessage = "Enter Patient")]
        public string pname { get; set; }
        [DisplayName("Age")]
        [Required(ErrorMessage = "Enter Age")]
        public int? age { get; set; }
        [DisplayName("Ref. By Doctor")]
        public string RefByDoc { get; set; }
        [DisplayName("Gender")]
        [Required(ErrorMessage = "Select Gender")]
        public string gender { get; set; }
        [DisplayName("Mobile No.")]
        [Required(ErrorMessage = "Enter Mobile Number")]
        public string mobileNo { get; set; }


        public int BillBookNumber { get; set; }

        public int BillId { get; set; }


    }

    public class SelectPatientandReports
    {

      public PatientInfoModel patientInfoModel { get; set; }
      public List<int> ReportsByPid { get; set; }

    }
}

