using Dapper;
using NamrataKalyani.CustomAttribute;
using NamrataKalyani.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace NamrataKalyani.Controllers
{
    [Authorize]
    [SessionTimeoutAttribute]
    public class BillingController : Controller
    {
        // GET: Billing

        int UserId;
        public BillingController()
        {
            if (System.Web.HttpContext.Current.Session["UserId"] != null)
            {
                UserId = Convert.ToInt32(System.Web.HttpContext.Current.Session["UserId"]);
            }
        }

        public ActionResult GetBillingInfo(int? BillId, string BeginDate, string Enddate)
        {

            var dlist = RetuningData.ReturnigList<PatientInfoModel>("usp_getReportsByBill", null);
            ViewBag.PatientList = new SelectList(dlist, "PId", "PName");
            var param = new DynamicParameters();
            if (Request.QueryString["id"] != null)
            {
                param.Add("@BillId", Request.QueryString["id"]);
            }
            else
            {
                param.Add("@BillId", BillId);
            }
            param.Add("@BeginDate", BeginDate);
            param.Add("@EndDate", Enddate);
            if (string.IsNullOrEmpty(System.Web.HttpContext.Current.Session["RoleId"].ToString()))
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                param.Add("@RoleId", System.Web.HttpContext.Current.Session["RoleId"]);

            }
            var Bill = RetuningData.ReturnigList<_BilIingInfoModel>("getReports_test", param);

            return View(Bill);
        }

        public ActionResult UpdateBillingInfo(int? id, string BeginDate, string Enddate)
        {
            var param2 = new DynamicParameters();
            param2.Add("@BillId", id);
            var BillDet = RetuningData.ReturnigList<_BilIingInfoModel>("sp_getBillReportByBillId", param2).SingleOrDefault(); ;

            var param = new DynamicParameters();
            param.Add("@BillId", BillDet.UniqueBillNo);
            param.Add("@BeginDate", BeginDate);
            param.Add("@EndDate", Enddate);
            var Bill = RetuningData.ReturnigList<_BilIingInfoModel>("getReports_test", param).SingleOrDefault();

            return View(Bill);
        }

        [HttpPost]
        public ActionResult UpdateBillingInfo(_BilIingInfoModel Bill)
        {
            int result = 0, roleId;
            if (!String.IsNullOrEmpty(System.Web.HttpContext.Current.Session["RoleId"].ToString()))
            {
                roleId = Convert.ToInt32(System.Web.HttpContext.Current.Session["RoleId"]);
            }
            else
            {
                return RedirectToAction("login", "login");
            }
            var param = new DynamicParameters();
            param.Add("@BillId", Bill.BillId);
            param.Add("@Amount", Bill.Amount);
            param.Add("@Paid", Bill.Paid);
            param.Add("@Discount", Bill.Discount);
            param.Add("@Expenses", Bill.Expenses);
            param.Add("@Due", Bill.Due);
            param.Add("@ReferalAmount", Bill.ReferalAmount);
            param.Add("@Status", Bill.Status);
            param.Add("@ReferalPercentage", Bill.ReferalPercentage);
            param.Add("@UpdatedBy", UserId);
            result = RetuningData.AddOrSave<int>("usp_UpdateBilling", param);

            if (result > 0 && roleId == 1)
            {
                return RedirectToAction("Dashboard", "Doc");
            }
            else
            {
                return RedirectToAction("GetBillingInfo", new { BillId = Bill.BillId });
            }
        }

        [HttpPost]
        public ActionResult UpdateBillingInfofromPatientScreen(_BilIingInfoModel Bill)
        {
            int result = 0, roleId;
            if (!String.IsNullOrEmpty(System.Web.HttpContext.Current.Session["RoleId"].ToString()))
            {
                roleId = Convert.ToInt32(System.Web.HttpContext.Current.Session["RoleId"]);
            }
            else
            {
                return RedirectToAction("login", "login");
            }
            var param = new DynamicParameters();
            param.Add("@BillId", Bill.BillId);
            param.Add("@Amount", Bill.Amount);
            param.Add("@Paid", Bill.Paid);
            param.Add("@Discount", Bill.Discount);
            param.Add("@Due", Bill.Due);
            param.Add("@UpdatedBy", UserId);
            result = RetuningData.AddOrSave<int>("usp_UpdateBillingFromPatientScreen", param);


            return RedirectToAction("getBillinfo", "Billing", new { id = Bill.BillId });



        }


        public ActionResult ShowCollectedByDetails()
        {
            var Clist = RetuningData.ReturnigList<CollectedByModel>("uspGetCollectedByList", null);
            return View(Clist.ToList());
        }
        public ActionResult UpdateCollectedByInfo(int? Id)
        {
            var param = new DynamicParameters();
            param.Add("@CollectedById", Id);
            var _CollectectedById = RetuningData.ReturnigList<CollectedByModel>("usp_getCollectedById", param).SingleOrDefault();
            return View(_CollectectedById);
        }

        [HttpGet]
        public ActionResult DeleteBillingInfo(int? Id)
        {
            int result = 0, roleId;
            if (!String.IsNullOrEmpty(System.Web.HttpContext.Current.Session["RoleId"].ToString()))
            {
                roleId = Convert.ToInt32(System.Web.HttpContext.Current.Session["RoleId"]);
            }
            else
            {
                return RedirectToAction("login", "login");
            }
            var param = new DynamicParameters();
            param.Add("@BillId", Id);

            result = RetuningData.AddOrSave<int>("usp_DeleteBilling", param);
            if (result > 0)
            {
                return RedirectToAction("GetBillingInfo");
            }
            else
            {
                return null;
            }
        }
        [HttpPost]
        public ActionResult UpdateCollectedByInfo(CollectedByModel col)
        {
            int result = 0, roleId;
            if (!String.IsNullOrEmpty(System.Web.HttpContext.Current.Session["RoleId"].ToString()))
            {
                roleId = Convert.ToInt32(System.Web.HttpContext.Current.Session["RoleId"]);
            }
            else
            {
                return RedirectToAction("login", "login");
            }
            var param = new DynamicParameters();
            param.Add("@CollectedById", col.CollectedById);
            param.Add("@CollectedByName", col.CollectedByName);
            param.Add("@UpdatedBy", UserId);
            result = RetuningData.AddOrSave<int>("usp_UpdateCollectedByName", param);
            if (result > 0)
            {
                return RedirectToAction("ShowCollectedByDetails");
            }
            else
            {
                return null;
            }

        }

        // uspInsertCollectedByName

        public ActionResult SaveCollectedByInfo()
        {

            return View();
        }
        [HttpPost]
        public ActionResult SaveCollectedByInfo(CollectedByModel col)
        {
            int result = 0, roleId;
            if (!String.IsNullOrEmpty(System.Web.HttpContext.Current.Session["RoleId"].ToString()))
            {
                roleId = Convert.ToInt32(System.Web.HttpContext.Current.Session["RoleId"]);
            }
            else
            {
                return RedirectToAction("login", "login");
            }
            var param = new DynamicParameters();
            param.Add("@CollectedByName", col.CollectedByName);
            param.Add("@CreatedBy", UserId);
            param.Add("@UpdatedBy", UserId);
            result = RetuningData.AddOrSave<int>("uspInsertCollectedByName", param);
            if (result > 0)
            {
                return RedirectToAction("ShowCollectedByDetails");
            }
            else
            {
                return null;
            }

        }
        public ActionResult GenerateBill(int? id)
        {
            var param2 = new DynamicParameters();
            param2.Add("@BillId", id);
            var BillDet = RetuningData.ReturnigList<_BilIingInfoModel>("sp_getBillReportByBillId", param2).SingleOrDefault(); ;
            var param = new DynamicParameters();
            param.Add("@BillId", BillDet.UniqueBillNo);
            param.Add("@BeginDate", null);
            param.Add("@EndDate", null);
            param.Add("@RoleId", 1);
            var Bill = RetuningData.ReturnigList<_BilIingInfoModel>("getReports_test", param).SingleOrDefault();
            return View(Bill);
        }

        public ActionResult getBillinfo(int? id)
        {
            var Reports = RetuningData.ReturnigList<ReportModel>("sp_getReports", null);
            ViewBag.ReportType = new SelectList(Reports, "Id", "ReportType");
            PatientInfoModel Doc = new PatientInfoModel();

            var dlist = RetuningData.ReturnigList<PatientInfoModel>("uspGetDoctotList", null);
            ViewBag.DoctorList = new SelectList(dlist, "docid", "DoctorName");

            var Billing = RetuningData.ReturnigList<ReportModel>("sp_getReports", null);
            ViewBag.ReportType = new SelectList(Reports, "Id", "ReportType");

            var Clist = RetuningData.ReturnigList<PatientInfoModel>("uspGetCollectedByList", null);
            ViewBag.CollectedByList = new SelectList(Clist, "CollectedById", "CollectedById");

            var param = new DynamicParameters();
            param.Add("@BillId", id);
            param.Add("@BeginDate", null);
            param.Add("@EndDate", null);
            param.Add("@RoleId", 1);
            var Bill = RetuningData.ReturnigList<_BilIingInfoModel>("getReports_test", param);

            ViewBag.Billinfo = Bill;
            return View("~/Views/Doc/PatientInfo.cshtml");
        }
        [HttpPost]
        public ActionResult GetBillingInfo(string BillId, DateTime? BeginDate, DateTime? Enddate)
        {
            var dlist = RetuningData.ReturnigList<PatientInfoModel>("usp_getReportsByBill", null);
            ViewBag.PatientList = new SelectList(dlist, "PId", "PName");
            var param = new DynamicParameters();
            if (Request.QueryString["id"] != null)
            {
                param.Add("@BillId", Request.QueryString["id"]);
            }
            else
            {
                param.Add("@BillId", BillId);
            }
            param.Add("@BeginDate", BeginDate);
            param.Add("@EndDate", Enddate);
            if (string.IsNullOrEmpty(System.Web.HttpContext.Current.Session["RoleId"].ToString()))
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                param.Add("@RoleId", System.Web.HttpContext.Current.Session["RoleId"]);

            }
            var Bill = RetuningData.ReturnigList<_BilIingInfoModel>("getReports_test", param);

            return View(Bill);
        }

        [HttpPost]
        public ActionResult deleteBillId(int[] customerIDs)
        {
            int result = 0, roleId;
            if (!String.IsNullOrEmpty(System.Web.HttpContext.Current.Session["RoleId"].ToString()))
            {
                roleId = Convert.ToInt32(System.Web.HttpContext.Current.Session["RoleId"]);
            }
            else
            {
                return RedirectToAction("login", "login");
            }

            foreach (var Id in customerIDs)
            {
                var param = new DynamicParameters();
                param.Add("@BillId", Id);

                result = RetuningData.AddOrSave<int>("usp_DeleteBilling", param);
            }
            return Json("All the Selected Bill deleted successfully!");
        }

        [HttpGet]
        public ActionResult GetClientBillsByCode()
        {
            var Bill=new List<_BilIingInfoModel>();
            if (Session["UserName"] != null) {
                string CodeName = Convert.ToString(Session["UserName"]);
                var param = new DynamicParameters();
                param.Add("@CodeName", CodeName);
                Bill = RetuningData.ReturnigList<_BilIingInfoModel>("sp_getClientBillsByCode", param).ToList();
            }
            return View(Bill);
        }
    }
}

