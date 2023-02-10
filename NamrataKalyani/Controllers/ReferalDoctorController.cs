using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using PagedList;
using PagedList.Mvc;
using NamrataKalyani.Models;
using NamrataKalyani.CustomAttribute;

namespace NamrataKalyani.Controllers
{
    [Authorize]
    [SessionTimeoutAttribute]
    public class ReferalDoctorController : Controller
    {
        // GET: ReferalDoctor
        // GET: ReferDoc
        int UserId;
        public ReferalDoctorController()
        {
            if (System.Web.HttpContext.Current.Session["UserId"] != null)
            {
                UserId = Convert.ToInt32(System.Web.HttpContext.Current.Session["UserId"]);
            }
        }
        public ActionResult ReferDocIndex()
        {
            var rd = RetuningData.ReturnigList<ReferalDoctorModel>("usp_getDoctorsDetail", null);

            return View(rd);
        }


        public ActionResult CreateRecord()
        {
            var rd = RetuningData.ReturnigList<ReferalDoctorModel>("usp_getListDoctors", null);

            ViewBag.Doctor = new SelectList(rd, "docid", "doctorName");
            return View();
        }

        [HttpPost]
        public ActionResult CreateRecord(ReferalDoctorModel rdm)
        {
            var param = new DynamicParameters();
    
            param.Add("@DoctorName", rdm.DoctorName);
            param.Add("@CodeName", rdm.CodeName);
            param.Add("@Specilization", rdm.Specilization);
            param.Add("@Signature", rdm.Signature);
            param.Add("@Qualification", rdm.Qualification);
            param.Add("@ContactNumber", rdm.ContactNumber);
            param.Add("@EmailId", rdm.EmailId);
            param.Add("@Address1", rdm.Address1);
            param.Add("@Address2", rdm.Address2);
            param.Add("@Address3", rdm.Address3);
            param.Add("@MobileAdd1", rdm.MobileAdd1);
            param.Add("@MobileAdd2", rdm.MobileAdd2);
            param.Add("@MobileAdd3", rdm.MobileAdd3);
            param.Add("@DayAndTime1", rdm.DayAndTime1);
            param.Add("@DayAndTime2", rdm.DayAndTime2);
            param.Add("@DayAndTime3", rdm.DayAndTime3);
            
            param.Add("@CreatedBy", UserId);
            param.Add("@UpdatedBy", UserId);

            int i = RetuningData.AddOrSave<int>("usp_DoctorsInfo", param);
            if (i > 0)
            {

                return RedirectToAction("ReferDocIndex");
            }

            return View();
        }
        public ActionResult EditRecord(int? id)
        {
            var param = new DynamicParameters();
            param.Add("@Docid", id);
            var rdm = RetuningData.ReturnigList<ReferalDoctorModel>("usp_getDoctorsDetailById", param).SingleOrDefault();
            return View(rdm);

        }


        [HttpPost]
        public ActionResult EditRecord(ReferalDoctorModel rdm)
        {
            var param = new DynamicParameters();
            param.Add("@Specilization", rdm.Specilization);
            param.Add("@Signature", rdm.Signature);
            param.Add("@Qualification", rdm.Qualification);
            param.Add("@ContactNumber", rdm.ContactNumber);
            param.Add("@EmailId", rdm.EmailId);
            param.Add("@Address1", rdm.Address1);
            param.Add("@Address2", rdm.Address2);
            param.Add("@Address3", rdm.Address3);
            param.Add("@MobileAdd1", rdm.MobileAdd1);
            param.Add("@MobileAdd2", rdm.MobileAdd2);
            param.Add("@MobileAdd3", rdm.MobileAdd3);
            param.Add("@DayAndTime1", rdm.DayAndTime1);
            param.Add("@DayAndTime2", rdm.DayAndTime2);
            param.Add("@DayAndTime3", rdm.DayAndTime3);
            param.Add("@DocId", rdm.DocId);
            param.Add("@DoctorName", rdm.DoctorName);
            param.Add("@CodeName", rdm.CodeName);
            param.Add("@CreatedBy", UserId);
            param.Add("@UpdatedBy", UserId);

            int i = RetuningData.AddOrSave<int>("usp_DoctorsInfo", param);
            
            if (i > 0)
            {
                return RedirectToAction("ReferDocIndex");
            }
            return View();
        }


        public ActionResult DeleteRecord(int? id)
        {

            var param = new DynamicParameters();
            param.Add("@Rid", id);
            var rm = RetuningData.ReturnigList<ReferalDoctorModel>("sp_ReferDoctorById", param).SingleOrDefault();
            return View(rm);
        }

        public ActionResult Details(int? id) {
            var param = new DynamicParameters();
            param.Add("@Docid", id);
            var rdm = RetuningData.ReturnigList<ReferalDoctorModel>("usp_getDoctorsDetailById", param).SingleOrDefault();
            return View(rdm);
        }

        [HttpPost]
        public ActionResult DeleteConfirm(int? id)
        {

            var param = new DynamicParameters();

            param.Add("@DocId", id);
            var num = RetuningData.ReturnigList<int>("[uspDeleteDoctor]", param).SingleOrDefault();


            if (num>0)
            {
                ViewBag.msg = "Record Deleted";
                return RedirectToAction("ReferDocIndex");
            }

            return RedirectToAction("ReferDocIndex");
        }
    }
}