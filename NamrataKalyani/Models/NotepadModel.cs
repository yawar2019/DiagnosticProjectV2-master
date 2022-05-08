using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NamrataKalyani.Models
{
    public class NotepadModel
    {
        public int Nid { get; set; }
        public string FileName { get; set; }

        [Display(Name ="Short Discription")]
       
        public string ShortDescription { get; set; }
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string Notepad { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}