using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NamrataKalyani.Models
{
    public class ReportByPidModel    {        public int Pid { get; set; }        public int ReportTypeId { get; set; }        public int ReportId { get; set; }        public int BillId { get; set; }        [AllowHtml]        public string Description { get; set; }        public string Pname { get; set; }        [DisplayName("Doctor Name")]        public string DoctorName { get; set; }        public int? Srno { get; set; }        public int Age { get; set; }        public string Gender { get; set; }        public string RefByDoc { get; set; }        public string BillingID { get; set; }        public int CollectedBy { get; set; }
    }

}