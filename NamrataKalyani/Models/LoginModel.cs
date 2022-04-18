using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace NamrataKalyani.Models
{
    public class LoginModel
    {
        public int id { get; set; }
        public string Name { get; set; }
        [DisplayName("Email")]
        [Required(ErrorMessage ="Email is Required")]
        public string email { get; set; }
        [Required(ErrorMessage = "Password is Required")]
        [DisplayName("Password")]
        public string Passward { get; set; }
        public string Name_Mobile { get; set; }
        public int RoleId { get; set; }
    }
}