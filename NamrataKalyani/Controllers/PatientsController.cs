using Dapper;
using NamrataKalyani.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NamrataKalyani.Controllers
{
    public class PatientsController : Controller
    {
        // GET: Patients
        int UserId;
        public PatientsController()
        {
            if (System.Web.HttpContext.Current.Session["UserId"] != null)
            {
                UserId = Convert.ToInt32(System.Web.HttpContext.Current.Session["UserId"]);
            }
        }
        public ActionResult GetAllPatientDetails()
        {
            var patList = RetuningData.ReturnigList<PatientInfoModel>("sp_GetPatients", null);
            return View(patList);//sp_GetPatients
        }
        [HttpGet]
        public ActionResult Edit(int? id)
        {
           

            var param = new DynamicParameters();
            param.Add("@Pid", id);
            var patList = RetuningData.ReturnigList<PatientInfoModel>("sp_GetPatientsById", param).SingleOrDefault();
            
            var dlist = RetuningData.ReturnigList<PatientInfoModel>("uspGetDoctotList", null);
            ViewBag.DoctorList = new SelectList(dlist, "docid", "DoctorName", patList.RefByDoc);
            var Reports = RetuningData.ReturnigList<ReportModel>("sp_getReports", null);

            var paramptAndReport = new DynamicParameters();
            param.Add("@Pid", id);
            var rltf = RetuningData.ReturnigList<GetAllReportsByPatientIdModel>("usp_getAllReportsByPatientId", param);

            foreach (var item in rltf)
            {
                foreach (var report in Reports)
                {
                    if(item.ReportId==Convert.ToInt32(report.Id))
                    {
                        report.selectedReport = "true";
                        break;
                    }
                    
                  
                }
            }

            SelectPatientandReports selectPatientandReports = new SelectPatientandReports();
            selectPatientandReports.patientInfoModel = patList;
            selectPatientandReports.ReportsByPid = rltf.Select(x=>x.ReportId).ToList();
            ViewBag.ReportType = Reports.OrderByDescending(x=>x.selectedReport);

            return View(patList);
        }


       [HttpPost]
        public ActionResult Edit(EditPatientInfoModel patientInfo,string[] newReports)
        {
            var dlist = RetuningData.ReturnigList<PatientInfoModel>("uspGetDoctotList", null);
            ViewBag.DoctorList = new SelectList(dlist, "docid", "DoctorName");

            var paramptAndReport = new DynamicParameters();
            paramptAndReport.Add("@Pid", patientInfo.pid);

            var rltf = RetuningData.ReturnigList<GetAllReportsByPatientIdModel>("usp_getAllReportsByPatientId", paramptAndReport);
            int[] tblexistreportid = rltf.Select(x => x.ReportId).ToArray();


            var RefDoc = (from doc in dlist
                            where doc.DoctorName == patientInfo.DoctorName.Trim()
                            select doc.docid).SingleOrDefault();


            int billId;
            var param = new DynamicParameters();
            param.Add("@Pid", patientInfo.pid);
            param.Add("@Pname", patientInfo.pname);
            param.Add("@SurName", patientInfo.surname);
            param.Add("@Age", patientInfo.age);
            param.Add("@YMD", patientInfo.ymd);
            param.Add("@Gender", patientInfo.gender);
            param.Add("@RefDocId", RefDoc);
            param.Add("@MobileNo", patientInfo.mobileNo);
             


            int Pid = RetuningData.AddOrSave<int>("sp_UpdatePatientById", param);
 

            var paramDelete = new DynamicParameters();

                paramDelete.Add("@BillBookNumber", patientInfo.BillBookNumber);
             

                billId = RetuningData.ReturnSingleValue<int>("deleteBillingTransaction", paramDelete);
             

            ////new and updated Reports 
            var paramNew = new DynamicParameters();

            foreach (var item in newReports)
            {
                paramNew.Add("@BillBookNumber", patientInfo.BillBookNumber);
                paramNew.Add("@ReportId", item);
                paramNew.Add("@CreatedBy", UserId);
                paramNew.Add("@UpdatedBy", UserId);
                billId = RetuningData.ReturnSingleValue<int>("UpdateBillingTransaction", paramNew);
            }

            if (Pid > 0)
            {
                return RedirectToAction("GetAllPatientDetails");
            }
            else
            {
                return View();
            }

        }

        public ActionResult GetPatientsById(int? id)
        {
            var param = new DynamicParameters();
            param.Add("@Pid", id);
            var patList = RetuningData.ReturnigList<PatientInfoModel>("sp_GetPatientDetailById", param).SingleOrDefault();
            return View(patList);
        
        }
    }
}