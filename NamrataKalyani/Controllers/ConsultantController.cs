using Dapper;
using NamrataKalyani.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NamrataKalyani.Controllers
{
    public class ConsultantController : Controller
    {
        // GET: Consultant
        int UserId;
        public ConsultantController()
        {
            if (System.Web.HttpContext.Current.Session["UserId"] != null)
            {
                UserId = Convert.ToInt32(System.Web.HttpContext.Current.Session["UserId"]);
            }
        }
        public ActionResult Index()
        {
            var dlist = RetuningData.ReturnigList<Consultant>("sp_getConsultant", null).ToList();
            return View(dlist);
        }
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(string Name,   string Qualification,string Department, HttpPostedFileBase SigNature)
        {
            if (SigNature.ContentLength > 0)
            {
                string _FileName = Path.GetFileName(SigNature.FileName);
                string _path = Path.Combine(Server.MapPath("~/uploads"), _FileName);
                SigNature.SaveAs(_path);


                var param = new DynamicParameters();

                param.Add("@Name", Name);
                param.Add("@Signature", "../../uploads/"+_FileName);
                param.Add("@Qualification", Qualification);
                param.Add("@Department", Department);

                int i = RetuningData.AddOrSave<int>("sp_SaveConsultant", param);
                if (i > 0)
                {
                    return RedirectToAction("Index", "Consultant");
                }
                else
                {
                    return View();
                }
            }
            return View();
        }

        public ActionResult Edit(int? id)
        {
            var param = new DynamicParameters();
            param.Add("@ConsultantId", id);
            var dlist = RetuningData.ReturnigList<Consultant>("sp_getConsultantById", param: param).SingleOrDefault();

            return View(dlist);
        }


        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(int ConsultantId,string Name, HttpPostedFileBase SigNature, string Qualification, string Department)
        {
            if (SigNature.ContentLength > 0)
            {

                string _FileName = Path.GetFileName(SigNature.FileName);
                string _path = Path.Combine(Server.MapPath("~/uploads"), _FileName);
                SigNature.SaveAs(_path);

                var param = new DynamicParameters();

                param.Add("@ConsultantId", ConsultantId);
                param.Add("@Name", Name);
                param.Add("@Signature", "../../uploads/" + _FileName);
                param.Add("@Qualification", Qualification);
                param.Add("@Department", Department);

                int i = RetuningData.AddOrSave<int>("sp_UpdateConsultant", param);
                if (i > 0)
                {
                    return RedirectToAction("Index", "Consultant");
                }
                else
                {
                    return View();
                }
            }

            return View();

        }

        public ActionResult Details(int? id)
        {
            var param = new DynamicParameters();
            param.Add("@ConsultantId", id);
            var dlist = RetuningData.ReturnigList<Consultant>("sp_getConsultantById", param: param).SingleOrDefault();

            return View(dlist);
        }

        public ActionResult Delete(int? id)
        {
            var param = new DynamicParameters();
            param.Add("@ConsultantId", id);
            var dlist = RetuningData.ReturnigList<Consultant>("sp_getConsultantById", param: param).SingleOrDefault();

            return View(dlist);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int? id)
        {
            var param = new DynamicParameters();
            param.Add("@ConsultantId", id);
            int i = RetuningData.AddOrSave<int>("sp_DeleteConsultant", param);
            if (i > 0)
            {
                return RedirectToAction("Index", "Notepad");
            }
            else
            {
                return View();
            }
        }
    }
}