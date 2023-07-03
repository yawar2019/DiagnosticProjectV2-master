using Dapper;
using NamrataKalyani.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NamrataKalyani.Controllers
{

    public class HomeController : Controller
    {
        int UserId;
        public HomeController()
        {
            if (System.Web.HttpContext.Current.Session["UserId"] != null)
            {
                UserId = Convert.ToInt32(System.Web.HttpContext.Current.Session["UserId"]);
            }
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Settings()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        public List<_BilIingInfoModel> GetPatientInfo(string id, DateTime? BeginDate, DateTime? EndDate)
        {

            var param = new DynamicParameters();
            param.Add("@BillId", id);

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
            param.Add("@RoleId", 1);
            var Bill = RetuningData.ReturnigList<_BilIingInfoModel>("getReports_test", param).ToList();
           
            return Bill;
        }

        public ActionResult ViewPendingRequest()
        {
            var Reports = RetuningData.ReturnigList<ReportModel>("sp_getReports", null);
            ViewBag.ReportType = new SelectList(Reports, "Id", "ReportType");
            
            var param = new DynamicParameters();
            
            param.Add("@ApprovalFlag", 1);
            var rltf = RetuningData.ReturnigList<GetAllReportsByPatientIdModel>("usp_getAllReportsByPatientIdByApprovalFlag", param);
            rltf = rltf.Where(x => x.CreatedOn.ToShortDateString() == DateTime.Now.ToShortDateString());
            ViewBag.AllReportsByPid = rltf;
            var Patientinfo = new List<_BilIingInfoModel>();
            Patientinfo = GetPatientInfo(null, DateTime.Now, DateTime.Now.AddHours(48));
            var param2 = new DynamicParameters();
            var notificationHeader = RetuningData.ReturnigList<NotificationModel>("sp_GetRecentNotification", param2).SingleOrDefault();
            ViewBag.NotifcationHeader = notificationHeader.Name;
            return View(rltf);

              
        }


        public ActionResult ViewApprovalRequest()
        {
            var Reports = RetuningData.ReturnigList<ReportModel>("sp_getReports", null);
            ViewBag.ReportType = new SelectList(Reports, "Id", "ReportType");

            var param = new DynamicParameters();

            param.Add("@ApprovalFlag", 2);
            var rltf = RetuningData.ReturnigList<GetAllReportsByPatientIdModel>("usp_getAllReportsByPatientIdByApprovalFlag", param);
            rltf = rltf.Where(x => x.CreatedOn.ToShortDateString() == DateTime.Now.ToShortDateString());
            ViewBag.AllReportsByPid = rltf;
            var Patientinfo = new List<_BilIingInfoModel>();
            Patientinfo = GetPatientInfo(null, DateTime.Now, DateTime.Now.AddHours(48));
            var param2 = new DynamicParameters();
            var notificationHeader = RetuningData.ReturnigList<NotificationModel>("sp_GetRecentNotification", param2).SingleOrDefault();
            ViewBag.NotifcationHeader = notificationHeader.Name;
            return View(rltf);

            
        }

        public ActionResult ViewRejectRequest()
        {
            var Reports = RetuningData.ReturnigList<ReportModel>("sp_getReports", null);
            ViewBag.ReportType = new SelectList(Reports, "Id", "ReportType");

            var param = new DynamicParameters();

            param.Add("@ApprovalFlag", 3);
            var rltf = RetuningData.ReturnigList<GetAllReportsByPatientIdModel>("usp_getAllReportsByPatientIdByApprovalFlag", param);
            rltf = rltf.Where(x => x.CreatedOn.ToShortDateString() == DateTime.Now.ToShortDateString());
            ViewBag.AllReportsByPid = rltf;
            var Patientinfo = new List<_BilIingInfoModel>();
            Patientinfo = GetPatientInfo(null, DateTime.Now, DateTime.Now.AddHours(48));
            var param2 = new DynamicParameters();
            var notificationHeader = RetuningData.ReturnigList<NotificationModel>("sp_GetRecentNotification", param2).SingleOrDefault();
            ViewBag.NotifcationHeader = notificationHeader.Name;
            return View(rltf);

            
        }


        public ActionResult ShowReportApproval(ReportByPidModel rpt)
        {
            ReportByPidModel obj = new ReportByPidModel();

            obj.Pid = rpt.Pid;
            obj.BillId = Convert.ToInt32(Request.QueryString["BillId"]);
            obj.ReportId = Convert.ToInt32(Request.QueryString["ReportId"]);

            
            return RedirectToAction("UpdateApprovalRequest", "Doc", obj);
             

        }

        [HttpPost]
        public ActionResult UpdateApprovalStatus(int ApprovalRequest,string Reason,int ReportId, int BillId)
        {
      

            var param = new DynamicParameters();
            
            param.Add("@ApprovalFlag", ApprovalRequest);
            param.Add("@ApprovalReason", Reason);
            param.Add("@BillID", BillId);
            param.Add("@RptID", ReportId);
           param.Add("@UpdatedBy", UserId);
            int i = RetuningData.AddOrSave<int>("usp_UpdateApprovalStatus", param);
            if (i > 0)
            {
                return RedirectToAction("ViewPendingRequest");
            }

            return View();
        
        }

        [HttpPost]
        public JsonResult AutocompleteDoctorinfo(string Prefix)
        {
            var dlist = RetuningData.ReturnigList<PatientInfoModel>("uspGetDoctotList", null);
            //ViewBag.DoctorList = new SelectList(dlist, "docid", "DoctorName");
            var Name = (from N in dlist
                        where N.DoctorName.ToLower().Contains(Prefix.ToLower())
                        select new { N.DoctorName });
            return Json(Name, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SearchPendingRequestRecord(string BeginDate, string EndDate,int? ApprovalFlag)
        {
            DateTime start=new DateTime(), end= new DateTime();

            if (!string.IsNullOrEmpty(BeginDate))
            {
                start = Convert.ToDateTime(BeginDate);
            }
            else
            {
                start = Convert.ToDateTime(DateTime.Now);

            }

            if (!string.IsNullOrEmpty(EndDate))
            {
                end = Convert.ToDateTime(EndDate);
            }
            else
            {
                end = Convert.ToDateTime(DateTime.Now);

            }

            var Reports = RetuningData.ReturnigList<ReportModel>("sp_getReports", null);
            ViewBag.ReportType = new SelectList(Reports, "Id", "ReportType");

            var param = new DynamicParameters();

            param.Add("@ApprovalFlag", ApprovalFlag);
            var rltf = RetuningData.ReturnigList<GetAllReportsByPatientIdModel>("usp_getAllReportsByPatientIdByApprovalFlag", param);
            rltf = rltf.Where(x =>Convert.ToDateTime(x.CreatedOn.ToShortDateString())>= Convert.ToDateTime(start.ToShortDateString()) && Convert.ToDateTime(x.CreatedOn.ToShortDateString()) <= Convert.ToDateTime(end.ToShortDateString()));
            ViewBag.AllReportsByPid = rltf;
            var Patientinfo = new List<_BilIingInfoModel>();
            Patientinfo = GetPatientInfo(null, DateTime.Now, DateTime.Now.AddHours(48));
            var param2 = new DynamicParameters();
            var notificationHeader = RetuningData.ReturnigList<NotificationModel>("sp_GetRecentNotification", param2).SingleOrDefault();
            ViewBag.NotifcationHeader = notificationHeader.Name;
            if (ApprovalFlag == 1)
            {
                return View("ViewPendingRequest", rltf);
            }
            else if (ApprovalFlag == 2)
            { 
                return View("ViewApprovalRequest", rltf);

            }
            else 
            {
                return View("ViewRejectRequest", rltf);

            }
             
        }
    }
}