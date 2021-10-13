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


        public int? pid { get; set; }

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


    }

    public class PatientInfoOldModel
    {
        public string mobileNo { get; set; }
    }
}

