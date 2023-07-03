using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NamrataKalyani.Models
{
    public class DashBoardHeaderModel
    {
        public string Registered { get; set; }
        public string Collected { get; set; }
        public string Rejected { get; set; }
        public string ResultDone { get; set; }
        public string Verified { get; set; }
        public string Approved { get; set; }
        public string Dispatch { get; set; }
    }
}