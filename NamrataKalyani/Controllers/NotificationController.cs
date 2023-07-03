using Dapper;
using NamrataKalyani.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NamrataKalyani.Controllers
{
    public class NotificationController : Controller
    {
        // GET: Notification

        int UserId;
        public NotificationController()
        {
            if (System.Web.HttpContext.Current.Session["UserId"] != null)
            {
                UserId = Convert.ToInt32(System.Web.HttpContext.Current.Session["UserId"]);
            }
        }
        public ActionResult Index()
        {
            var param = new DynamicParameters();
            var pat = RetuningData.ReturnigList<NotificationModel>("sp_ALL_Notification", param);
            return View(pat);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(string Name)
        {
            var param = new DynamicParameters();
            
            param.Add("@Name", Name);
            

            int i = RetuningData.AddOrSave<int>("sp_CreateNotification", param);
            if (i > 0)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        public ActionResult Edit(int? id)
        {
            var param = new DynamicParameters();
            param.Add("@id", id);

            var pat = RetuningData.ReturnigList<NotificationModel>("sp_Get_Notification_ById", param).SingleOrDefault();
            
            return View(pat);
        }

        [HttpPost]
        public ActionResult Edit(NotificationModel notificationModel)
        {
            var param = new DynamicParameters();
      
            param.Add("@Name", notificationModel.Name);
            param.Add("@id", notificationModel.Id);

            int i = RetuningData.AddOrSave<int>("sp_UpdateNotification", param);
            if (i > 0)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
    }
}