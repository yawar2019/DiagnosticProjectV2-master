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
            var param = new DynamicParameters();
            param.Add("@BillId", id);
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
                return RedirectToAction("login","login");
            }
            var param = new DynamicParameters();
            param.Add("@BillId", Bill.BillId);
            param.Add("@Amount", Bill.Amount);
            param.Add("@Discount", Bill.Discount);
            param.Add("@Expenses", Bill.Expenses);
            param.Add("@Due", Bill.Due);
            param.Add("@ReferalAmount", Bill.ReferalAmount);
            param.Add("@Status", Bill.Status);
            param.Add("@ReferalPercentage", Bill.ReferalPercentage);
            param.Add("@UpdatedBy", UserId);
            result = RetuningData.AddOrSave<int>("usp_UpdateBilling", param);

            if (result > 0 && roleId==1)
            {
                return RedirectToAction("GetBillingInfo");
            }
            else
            {
                return RedirectToAction("GetBillingInfo",new {BillId= Bill.BillId });
            }
        }
    }
}
