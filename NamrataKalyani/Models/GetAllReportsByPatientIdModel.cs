using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NamrataKalyani.Models
{
    public class GetAllReportsByPatientIdModel
    {
        public int Pid { get; set; }
        public string PatientName { get; set; }
        public string Name { get; set; }
        public int ReportTypeId { get; set; }
        public int ReportId { get; set; }
        public int BillId { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public string Printer_Name { get; set; }
        public string CreatedName { get; set; }
        public string UpdatedName { get; set; }
        public string MobileNo { get; set; }
        public string Qualification { get; set; }
        public string Department { get; set; }
        public string Signature { get; set; }
        public string ConsultantName { get; set; }
        public string ApprovalReason { get; set; }
        public string Status { get; set; }
        public int ApprovalFlag { get; set; }
        public string ApprovedBy { get; set; }
        public string BillBookNumber { get; set; }
    }
}