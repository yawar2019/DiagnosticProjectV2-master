using Dapper;
using NamrataKalyani.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace NamrataKalyani.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
      
        public ActionResult Login()
        {
            return View("~/Views/Doc/Login.cshtml");
        }

        [HttpPost]
    
        public ActionResult Login(LoginModel login)
        {
            var param = new DynamicParameters();
            param.Add("@Email", login.email);
            param.Add("@Passward", login.Passward);

            LoginModel _login = RetuningData.ReturnigList<LoginModel>("sp_getLogin", param).SingleOrDefault();
            if (_login != null)
            {
                if (_login.Name != null)
                {
                    Session["UserName"] = _login.Name;
                }

                Session["UserId"] = _login.id;
                Session["RoleId"] = _login.RoleId;
                FormsAuthentication.SetAuthCookie(_login.Name, false);

                return RedirectToAction("Dashboard","Doc");
            }
            else
            {
                return View();
            }
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
        public ActionResult Registration()
        {
            var dlist = RetuningData.ReturnigList<CenterModel>("usp_getCenter", null);
            ViewBag.Center = new SelectList(dlist, "CenterId", "CenterName");
            return View();
        }
        [HttpPost]
        public ActionResult Registration(RegistrationModel reg)
        {
            var param = new DynamicParameters();
            param.Add("@Name", reg.name);
            param.Add("@Email", reg.emalid);
            param.Add("@Passward", reg.password);
            param.Add("@RoleId", reg.RoleId);
            param.Add("@Status", reg.status);
            param.Add("@CenterId", 1);

            int i = RetuningData.AddOrSave<int>("usp_getUserLogin", param);
            if (i > 0)
            {
                return RedirectToAction("Login");
            }
            else
            {
                return View();
            }
        }
    }
}