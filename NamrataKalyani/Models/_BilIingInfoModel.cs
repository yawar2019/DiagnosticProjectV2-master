using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NamrataKalyani.Models
{
    public class _BilIingInfoModel
    {
        public int Id { get; set; }        public int BillId { get; set; }        public int PId { get; set; }        public string PatientName { get; set; }        public int ReportId { get; set; }        public string ReportName { get; set; }        public string DoctorName { get; set; }        public decimal Amount { get; set; }        public decimal Investment { get; set; }        public decimal Discount { get; set; }        public decimal Expenses { get; set; }        public decimal Due { get; set; }        public decimal ReferalAmount { get; set; }        public bool Status { get; set; }        public decimal Total_Amount { get; set; }
        public string Investigation { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime Date { get; set; }
        public decimal ReferalPercentage { get; set; }
    }
}