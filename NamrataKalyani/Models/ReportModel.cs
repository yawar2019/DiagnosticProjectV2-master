using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NamrataKalyani.Models
{
    public class ReportModel
    {
        public int Id { get; set; }
        [Display(Name="Report Name")]
        [Required(ErrorMessage = "Report Name Cannot be Empty")]
        public string ReportType { get; set; }
        public int ReportTypeId { get; set; }
        public string ReportId { get; set; }
        [AllowHtml]
        [Required(ErrorMessage ="Report Cannot be Empty")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Report Short Name Cannot be Empty")]
        public string ShortName { get; set; }
        public string CreatedName { get; set; }
        public string UpdatedName { get; set; }
        
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
    }
}