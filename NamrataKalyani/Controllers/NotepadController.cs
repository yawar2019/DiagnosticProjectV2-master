using NamrataKalyani.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dapper;
using System.Data;
using System.Net.Mail;
using System.IO;
using System.Net;

namespace NamrataKalyani.Controllers
{
    public class NotepadController : Controller
    {
        // GET: Notepad
        public ActionResult Index()
        {
            var dlist = RetuningData.ReturnigList<NotepadModel>("usp_getNotepad", null).ToList();
            return View(dlist);
        }
        public ActionResult Create()
        {
            return View();
        }
       

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(string FileName, string ShortDescription, string Notepad)
        {
            var param = new DynamicParameters();

            param.Add("@FileName", FileName);
            param.Add("@ShortDescription", ShortDescription);
            param.Add("@Notepad",  Notepad);
            param.Add("@CreatedBy", 1);
            param.Add("@UpdatedBy", 1);

            int i = RetuningData.AddOrSave<int>("sp_CreateNotepad", param);
            if (i > 0)
            {
                return RedirectToAction("Index", "Notepad");
            }
            else
            {
                return View();
            }
        }

        public ActionResult Edit(int? id)
        {
            var param = new DynamicParameters();
            param.Add("@Nid", id);
            var dlist = RetuningData.ReturnigList<NotepadModel>("usp_getNotepad_ById", param: param).SingleOrDefault();

            return View(dlist);
        }


        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(int Nid,string FileName, string ShortDescription, string Notepad)
        {
            var param = new DynamicParameters();

            param.Add("@Nid", Nid);
            param.Add("@FileName", FileName);
            param.Add("@ShortDescription", ShortDescription);
            param.Add("@Notepad",  Notepad);
            param.Add("@UpdatedBy", 1);

            int i = RetuningData.AddOrSave<int>("sp_UpdatedNotepad", param);
            if (i > 0)
            {
                return RedirectToAction("Index", "Notepad");
            }
            else
            {
                return View();
            }
        }

        public ActionResult Details(int? id)
        {
            var param = new DynamicParameters();
            param.Add("@Nid", id); 
            var dlist = RetuningData.ReturnigList<NotepadModel>("usp_getNotepad_ById",param: param).SingleOrDefault();

            return View(dlist);
        }

        public ActionResult Delete(int? id)
        {
            var param = new DynamicParameters();
            param.Add("@Nid", id);
            var dlist = RetuningData.ReturnigList<NotepadModel>("usp_getNotepad_ById", param: param).SingleOrDefault();

            return View(dlist);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int? id)
        {
            var param = new DynamicParameters();
            param.Add("@Nid", id);
            int i = RetuningData.AddOrSave<int>("sp_DeleteNotepad", param);
            if (i > 0)
            {
                return RedirectToAction("Index", "Notepad");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SendMail(MailModel mailModel, HttpPostedFileBase[] attachments)
        {

            using (MailMessage mail = new MailMessage())
            {
                mailModel.emailFromAddress="medicoweblab@gmail.com";
                mail.From = new MailAddress(mailModel.emailFromAddress);
                mail.To.Add(mailModel.emailToAddress);
                mail.Subject = mailModel.subject;
                mail.Body = mailModel.Notepad;
                mail.IsBodyHtml = true;
                // mail.Attachments.Add(new Attachment("D:\\TestFile.txt"));//--Uncomment this to send any attachment  
                List<string> fileNames = null;

                if (attachments != null && attachments.Length > 0)
                {
                    fileNames = new List<string>();
                    if (attachments != null)
                    {
                        foreach (HttpPostedFileBase attachment in attachments)
                        {
                            if (attachment != null)
                            {
                                if (attachment.FileName != null)
                                {
                                    string _FileName = Path.GetFileName(attachment.FileName);
                                    string _path = Path.Combine(Server.MapPath("~/uploads"), attachment.FileName);
                                    attachment.SaveAs(_path);

                                    using (var stream = new FileStream(_path, FileMode.Create))
                                    {
                                        new Attachment(stream, attachment.FileName);
                                    }
                                    fileNames.Add(_path);
                                }
                            }
                        }
                    }
                }

                if (attachments != null)
                {
                    foreach (var attachment in fileNames)
                    {
                        mail.Attachments.Add(new Attachment(attachment));
                    }
                }



                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 25))
                {
                    smtp.Credentials = new NetworkCredential("medicoweblab@gmail.com", "ismail2688@2005");
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
            }

            TempData["msg"] = "Mail Send Successfully";
            return Redirect("~/Notepad/Details/"+ mailModel.id);
        }



    }
}