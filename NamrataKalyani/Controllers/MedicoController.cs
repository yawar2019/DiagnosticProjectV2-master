using Dapper;
using NamrataKalyani.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NamrataKalyani.Controllers
{
    public class MedicoController : Controller
    {
        // GET: Medico
        public ActionResult MedicoDashboard(string CodeName)
        {
            var Patientinfo = new List<_BilIingInfoModel>();
            Patientinfo = GetPatientInfo(null, null, null, CodeName);
            return View(Patientinfo);
            
        }

        [HttpGet]
        public List<_BilIingInfoModel> GetPatientInfo(string id, DateTime? BeginDate, DateTime? EndDate,string CodeName)
        {

            var param = new DynamicParameters();
            param.Add("@BillId", id);
            param.Add("@CodeName", CodeName);
            if (string.IsNullOrEmpty(id))
            {
                if (BeginDate != null)
                {
                    param.Add("@BeginDate", BeginDate.Value.ToString("yyyy-MM-dd"));
                }
                else
                {
                    param.Add("@BeginDate", DateTime.Now.ToShortDateString());
                }


                if (EndDate != null)
                {
                    param.Add("@EndDate", EndDate.Value.ToString("yyyy-MM-dd"));
                }
                else
                {
                    param.Add("@EndDate", DateTime.Now.ToShortDateString());
                }

 
            }

            var Bill = RetuningData.ReturnigList<_BilIingInfoModel>("getClientReports", param).ToList();
            
            return Bill;
        }


        public ActionResult GetClientAllReportsByPatientId(int? id,string CodeName)
        {
            var Reports = RetuningData.ReturnigList<ReportModel>("sp_getReports", null);
            ViewBag.ReportType = new SelectList(Reports, "Id", "ReportType");
            int? Pid = id;
            if (id == null)
            {
                Pid = Convert.ToInt32(Request.QueryString["Pid"]);
            }
            var param = new DynamicParameters();
            param.Add("@Pid", Pid);
            var rltf = RetuningData.ReturnigList<GetAllReportsByPatientIdModel>("usp_getAllReportsByPatientId", param);

            ViewBag.ReportsInfo = rltf;
            var Patientinfo = new List<_BilIingInfoModel>();
            Patientinfo = GetPatientInfo(null, null, null,CodeName);
            return View("MedicoDashboard", Patientinfo);
        }

    }
}