using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NamrataKalyani.Models
{
    public class ReferalDoctorModel
    {
        public int DocId { get; set; }

        [Required(ErrorMessage ="Enter Doctor Name")]
        [Display(Name ="Doctor Name")]
        public string  DoctorName { get; set; }

        public string Mobile { get; set; }

        public string Email { get; set; }

        public string CreatedName { get; set; }
        public string UpdatedName { get; set; }

        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int UpdatedBy { get; set; }


        public DateTime UpdatedOn { get; set; }
    }
}