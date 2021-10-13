using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NamrataKalyani.Models;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using PagedList;
using PagedList.Mvc;
using NamrataKalyani.CustomAttribute;

namespace NamrataKalyani.Controllers
{
    [Authorize]
    [SessionTimeoutAttribute]
    public class ReportsController : Controller
    {
        int UserId;
        public ReportsController()
        {
            if (System.Web.HttpContext.Current.Session["UserId"] != null)
            {
                UserId = Convert.ToInt32(System.Web.HttpContext.Current.Session["UserId"]);
            }
        }

        // GET: Reports
        public ActionResult Index(int? page)        {            var rm = RetuningData.ReturnigList<ReportModel>("sp_getReports", null).ToPagedList(page ?? 1, 10);
            return View(rm);        }        
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ReportModel rem)
        {
            var param = new DynamicParameters();
            param.Add("@RName", rem.ReportType);
            param.Add("@Description", rem.Description);
            param.Add("@ShortName", rem.ShortName);
            param.Add("@CreatedBy", UserId);
            param.Add("@UpdatedBy", UserId);

            int i = RetuningData.AddOrSave<int>("sp_SaveReport", param);
            if (i > 0)
            {

                return RedirectToAction("index");
            }

            return View();
        }

        public ActionResult Edit(int? id)
        {

            var param = new DynamicParameters();
            param.Add("@Rid", id);
            var rm = RetuningData.ReturnigList<ReportModel>("sp_ReportById", param).SingleOrDefault();


            return View(rm);
        }

        [HttpPost]        public ActionResult Edit(ReportModel rm)        {            var param = new DynamicParameters();            param.Add("@Rid", rm.Id);            param.Add("@RName", rm.ReportType);            param.Add("@Description", rm.Description);            param.Add("@ShortName", rm.ShortName);            param.Add("@UpdatedBy", UserId );            param.Add("@UpdatedOn", DateTime.Now);            int i = RetuningData.AddOrSave<int>("usp_UpdateReportById", param);            if (i > 0)            {                return RedirectToAction("Index");            }            return View();        }


        public ActionResult Delete(int? id)
        {

            var param = new DynamicParameters();
            param.Add("@Rid", id);
            var rm = RetuningData.ReturnigList<ReportModel>("sp_ReportById", param).SingleOrDefault();
            return View(rm);
        }

        [HttpPost]
        public ActionResult Delete(ReportModel rm)
        {

            var param = new DynamicParameters();

            param.Add("@Rid", rm.Id);
            var num = RetuningData.ReturnigList<ReportModel>("sp_DeleteReportById", param).SingleOrDefault();


            if (num != null)
            {
                ViewBag.msg = "Record Deleted";
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        //public ActionResult Search()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult Search(ReportModel rm)
        //{
        //    var param = new DynamicParameters();
        //    // param.Add("@Pid", Session["Pid"]);
        //    //param.Add("@Date", cbrm.date);
        //    param.Add("@Rname", rm.ReportType);
        //    //int i = RetuningData.AddOrSave<int>("Dashborad", param);
        //    var i = RetuningData.ReturnigList<ReportModel>("uspGetReportByName", param);
        //    if (i != null)
        //    {

        //                        return View(i);
        //    }
        //    else
        //    {
        //        return View();
        //    }

        //}

    }

}
