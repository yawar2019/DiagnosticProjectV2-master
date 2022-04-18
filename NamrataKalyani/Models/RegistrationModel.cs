using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace NamrataKalyani.Models
{
    public class RegistrationModel
    {
        //  public int rid { get; set; }
        [DisplayName("Name")]
        [Required(ErrorMessage = "Name is Required")]
        public string name { get; set; }
        [Required(ErrorMessage = "Email is Required")]
        [DisplayName("Email")]
        public string emalid { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        [DisplayName("Password")]
        public string password { get; set; }

        [Compare("password", ErrorMessage = "Password and Confirm Password Miss Match")]
        [DisplayName("Confirm Password")]
        public string confirmPassword { get; set; }

        [DisplayName("Select Role")]
        [Required(ErrorMessage = "No Role is Selected")]
        public int RoleId { get; set; }
        [DisplayName("Center")]
        public string CenterId { get; set; }
        public bool status { get; set; }

        [Required(ErrorMessage = "Qualification is Required")]
        [DisplayName("Qualification")]
        public string Qualification { get; set; }

        [Required(ErrorMessage = "Experience is Required")]
        [DisplayName("Experience")]
        public string Experience { get; set; }

        [Required(ErrorMessage = "MobileNo is Required")]
        [DisplayName("MobileNo")]
        public string MobileNo { get; set; }

        [Required(ErrorMessage = "UID is Required")]
        [DisplayName("UID")]
        public string UID { get; set; }

        [Required(ErrorMessage = "Collected By User is Required")]
        [DisplayName("Collected By Name")]
        public string CollectedByUser { get; set; }
    }
}