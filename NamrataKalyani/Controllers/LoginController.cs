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
        int UserId;
        public LoginController()
        {
            if (System.Web.HttpContext.Current.Session["UserId"] != null)
            {
                UserId = Convert.ToInt32(System.Web.HttpContext.Current.Session["UserId"]);
            }
        }
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
                if (_login.Name == null && _login.Passward == null)
                {
                    ViewBag.Result = "Invalid UserName and Password";
                    return View("~/Views/Doc/Login.cshtml");
                }
                if (_login.Status == false)
                {
                    ViewBag.Result = "Your Account is Locked Please Contact Admin";
                    return View("~/Views/Doc/Login.cshtml");
                }

                if (_login.Name != null)
                {
                    Session["UserName"] = _login.Name;
                }

                Session["UserId"] = _login.id;
                Session["RoleId"] = _login.RoleId;
                FormsAuthentication.SetAuthCookie(_login.Name, false);

                return RedirectToAction("Dashboard", "Doc");
            }
            else
            {
                ViewBag.Result = "Invalid UserName and Password";
                return View("~/Views/Doc/Login.cshtml");
            }
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        public ActionResult Employees()
        {
            var employees = RetuningData.ReturnigList<RegistrationModel>("usp_GetEmployees", null);

            return View(employees);
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
            param.Add("@Experience", reg.Experience);
            param.Add("@Qualification", reg.Qualification);
            param.Add("@UID", reg.UID);
            param.Add("@MobileNo", reg.MobileNo);
            param.Add("@DOJ", reg.DOJ);
            param.Add("@Address", reg.Address);
            param.Add("@CollectedByUser", reg.CollectedByUser);
            param.Add("@CenterId", reg.CenterId);

            int i = RetuningData.AddOrSave<int>("usp_getUserLogin", param);
            if (i > 0)
            {
                return RedirectToAction("Employees");
            }
            else
            {
                return View();
            }
        }

        public ActionResult EditRegistration(int? id)
        {
            var param = new DynamicParameters();
            param.Add("@id", id);

            var Emp = RetuningData.ReturnigList<RegistrationModel>("sp_getLoginbyId", param).SingleOrDefault();

            return View(Emp);
        }

        [HttpPost]
        public ActionResult EditRegistration(RegistrationModel reg)
        {
            var param = new DynamicParameters();
            param.Add("@id", reg.EmpId);
            param.Add("@Name", reg.name);
            param.Add("@Email", reg.emalid);
            param.Add("@Passward", reg.password);
            param.Add("@RoleId", reg.RoleId);
            param.Add("@Status", reg.status);
            param.Add("@Experience", reg.Experience);
            param.Add("@Qualification", reg.Qualification);
            param.Add("@UID", reg.UID);
            param.Add("@MobileNo", reg.MobileNo);
            param.Add("@DOJ", reg.DOJ);
            param.Add("@Address", reg.Address);
            param.Add("@CollectedByUser", reg.CollectedByUser);
            param.Add("@Center", reg.CenterId);
            param.Add("@UpdatedBy", 1);
            param.Add("@UpdatedOn", DateTime.Now);

            int i = RetuningData.AddOrSave<int>("usp_UpdateUserLogin", param);
            if (i > 0)
            {
                return RedirectToAction("Employees");
            }
            else
            {
                return View();
            }
        }


        //[HttpPost]
        //public ActionResult Registration(RegistrationModel reg)
        //{
        //    var param = new DynamicParameters();
        //    param.Add("@Name", reg.name);
        //    param.Add("@Email", reg.emalid);
        //    param.Add("@Passward", reg.password);
        //    param.Add("@RoleId", reg.RoleId);
        //    param.Add("@Status", reg.status);
        //    param.Add("@Experience", reg.Experience);
        //    param.Add("@Qualification", reg.Qualification);
        //    param.Add("@UID", reg.UID);
        //    param.Add("@MobileNo", reg.MobileNo);
        //    param.Add("@DOJ", reg.DOJ);
        //    param.Add("@Address", reg.Address);
        //    param.Add("@CollectedByUser", reg.CollectedByUser);
        //    param.Add("@CenterId", reg.CenterId);

        //    int i = RetuningData.AddOrSave<int>("usp_getUserLogin", param);
        //    if (i > 0)
        //    {
        //        return RedirectToAction("Employees");
        //    }
        //    else
        //    {
        //        return View();
        //    }
        //}

        public ActionResult DeleteRegistration(int? id)
        {
            var param = new DynamicParameters();
            param.Add("@id", id);

            var Emp = RetuningData.ReturnigList<RegistrationModel>("sp_getLoginbyId", param).SingleOrDefault();

            return View(Emp);
        }

        [HttpPost]
        [ActionName("DeleteRegistration")]
        public ActionResult DeleteConfirm(int? id)
        {
            var param = new DynamicParameters();
            param.Add("@id", id);

            int i = RetuningData.AddOrSave<int>("usp_DeleteEmployee", param);
            if (i > 0)
            {
                return RedirectToAction("Employees");
            }
            else
            {
                return View();
            }
        }


        public ActionResult DetailsRegistration(int? id)
        {
            var param = new DynamicParameters();
            param.Add("@id", id);

            var Emp = RetuningData.ReturnigList<RegistrationModel>("sp_getLoginbyId", param).SingleOrDefault();

            return View(Emp);
        }


        public ActionResult CustomerLogin()
        {
            return View();
        }

        [HttpPost]

        public ActionResult CustomerLogin(ReferalDoctorModel login)
        {
            var param = new DynamicParameters();
            param.Add("@CodeName", login.CodeName);
            param.Add("@Password", login.Password);

            ReferalDoctorModel _login = RetuningData.ReturnigList<ReferalDoctorModel>("sp_getLoginMedico", param).SingleOrDefault();
            if (_login != null)
            {
                if (_login.CodeName == null && _login.Password == null)
                {
                    ViewBag.Result = "Invalid UserName and Password";
                    return View("~/Views/Login/CustomerLogin.cshtml");
                }
                

                if (_login.CodeName != null)
                {
                    Session["UserName"] = _login.CodeName;
                }

                Session["UserId"] = _login.DocId;
                Session["RoleId"] = 3;
                FormsAuthentication.SetAuthCookie(_login.DoctorName, false);

                return RedirectToAction("MedicoDashboard", "Medico",new {CodeName=_login.CodeName});
            }
            else
            {
                ViewBag.Result = "Invalid UserName and Password";
                return View("~/Views/Login/CustomerLogin.cshtml");
            }
        }

        public ActionResult Welcome()
        {
            return View();
        }

    }
}