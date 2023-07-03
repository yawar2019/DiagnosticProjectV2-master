using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NamrataKalyani.Models
{
    public class _BilIingInfoModel
    {
        public int Id { get; set; }        public int BillId { get; set; }        public int PId { get; set; }        public string PatientName { get; set; }        public int ReportId { get; set; }        public string ReportName { get; set; }        public string DoctorName { get; set; }        public string CodeName { get; set; }        public decimal Amount { get; set; }
        public decimal Paid { get; set; }        public decimal Investment { get; set; }        public decimal Discount { get; set; }        public decimal Expenses { get; set; }        public decimal Due { get; set; }        public decimal ReferalAmount { get; set; }        public bool Status { get; set; }        public decimal Total_Amount { get; set; }
        public string Investigation { get; set; }
        //[DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime Date { get; set; }
        public decimal ReferalPercentage { get; set; }
        public int BillNumber { get; set; }
        public string UniqueBillNo { get; set; }
        public string YMD { get; set; }
        public string Surname { get; set; }
        public int? Age { get; set; }
        public string gender { get; set; }
        public string showdate { get; set; }
        public string MobileNo { get; set; }
        public string CollectedByName { get; set; }
    }
}